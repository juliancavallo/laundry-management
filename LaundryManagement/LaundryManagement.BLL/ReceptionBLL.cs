using LaundryManagement.BLL.Mappers;
using LaundryManagement.BLL.Validators;
using LaundryManagement.DAL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Extensions;
using LaundryManagement.Domain.Filters;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LaundryManagement.BLL
{
    public class ReceptionBLL
    {
        private ReceptionDAL dal;
        private ReceptionMapper mapper;
        private ShippingBLL shippingBLL;
        private LogBLL logBLL;
        private TraceabilityBLL traceabilityBLL;
        private RoadmapBLL roadmapBLL;
        private ItemValidator itemValidator;

        public ReceptionBLL()
        {
            this.dal = new ReceptionDAL();
            this.shippingBLL = new ShippingBLL();
            this.logBLL = new LogBLL();
            this.traceabilityBLL = new TraceabilityBLL();
            this.roadmapBLL = new RoadmapBLL();
            this.mapper = new ReceptionMapper();
            this.itemValidator = new ItemValidator();
        }

        public List<ReceptionDTO> GetAll()
        {
            var list = this.dal.GetAll().AsEnumerable();
            list = list.Where(x => x.Destination.Equals(Session.Instance.User.Location));

            return list
                .Select(x => mapper.MapToDTO(x))
                .ToList();
        }

        public List<ReceptionViewDTO> GetAllForView(ReceptionFilter filter)
        {
            var list = this.GetAll().AsEnumerable();

            if (filter.DateFrom != DateTime.MinValue)
                list = list.Where(x => x.CreationDate >= filter.DateFrom.Date);

            if (filter.DateTo != DateTime.MinValue)
                list = list.Where(x => x.CreationDate <= filter.DateTo.Date.AddDays(1).AddTicks(-1));

            list = list.Where(x => x.Destination.Id == Session.Instance.User.Location.Id);

            return list.Select(x => mapper.MapToViewDTO(x)).ToList();
        }

        public List<ReceptionDetailViewDTO> GetDetailByRoadmaps(IEnumerable<int> roadmapIds)
        {
            var roadmaps = roadmapBLL.GetAll().Where(x => roadmapIds.Contains(x.Id));

            var items = roadmaps
                .SelectMany(x => x.Shippings
                    .SelectMany(s => s.ShippingDetail))
                .Select(x => x.Item)
                .GroupBy(x => x.Article)
                .Select(x => new 
                { 
                    x.Key, 
                    Quantity = x.Count() 
                }).ToList();

            return items.Select(x => new ReceptionDetailViewDTO()
            {
                ArticleId = x.Key.Id,
                Color = x.Key.Color.Name,
                ExpectedQuantity = x.Quantity,
                Size = x.Key.Size.Name,
                ItemType = x.Key.Type.Name
            }).ToList();
        }

        public IEnumerable<ReceptionDetailViewDTO> MapToView(IEnumerable<ReceptionDetailDTO> receptionDetailDTO)
        {
            var result = new List<ReceptionDetailViewDTO>();
            foreach (var item in receptionDetailDTO)
            {
                result.AddOrUpdate(mapper.MapToViewDTO(item));
            }
            return result;
        }

        public void Save(ReceptionDTO dto) 
        { 
            //Roadmap
            int id = dal.Save(mapper.MapToEntity(dto));
            dto.Id = id;

            //Item Status
            dal.UpdateItems(dto.Destination.Id, (int)ItemStatusEnum.OnLocation, id);

            //Shippings
            foreach(var roadmap in dto.Roadmaps)
            {
                shippingBLL.Receive(roadmap.Shippings);
            }

            //Traceability
            var traceabilityList = dto.ReceptionDetail.Select(x => new TraceabilityDTO()
            {
                Date = DateTime.Now,
                Destination = dto.Destination,
                Origin = dto.Origin,
                Item = x.Item,
                User = dto.CreationUser,
                ItemStatus = ItemStatusEnum.OnLocation,
                MovementType = MovementTypeEnum.Reception
            }).ToList();

            traceabilityBLL.Save(traceabilityList);

            logBLL.LogInfo(MovementTypeEnum.RoadMap, $"The reception {dto.Id} has been created");
        }

        public ValidationResponseDTO ApplyValidationForReception(ItemDTO item, LocationDTO originLocation)
        {
            var result = new ValidationResponseDTO();

            itemValidator.StatusValidation(item.ItemStatus, ItemStatusEnum.Sent);
            itemValidator.LocationValidation(item, originLocation);

            return result;
        }
    }
}
