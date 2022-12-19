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

        public List<ReceptionDetailViewDTO> GetDetailForView(int receptionId)
        {
            var reception = dal.GetAll().First(x => x.Id == receptionId);

            var detail = this.GetDetailByRoadmaps(reception.Roadmaps.Select(x => x.Id));
            detail.ForEach(x =>
            {
                x.Quantity += reception.ReceptionDetail
                    .Where(d => d.Item.Article.Id == x.ArticleId)
                    .Count();
            });

            foreach(var item in reception.ReceptionDetail
                .GroupBy(x => x.Item.Article.Id)
                .Select(x => new 
                {
                    Key = x.Key,
                    Items = x.AsEnumerable()
                }))
            {
                if (!detail.Any(x => x.ArticleId == item.Key))
                {
                    detail.Add(new ReceptionDetailViewDTO()
                    {
                        ArticleId = item.Key,
                        Color = item.Items.First().Item.Article.Color.Name,
                        ExpectedQuantity = 0,
                        Size = item.Items.First().Item.Article.Size.Name,
                        ItemType = item.Items.First().Item.Article.Type.Name,
                        Quantity = item.Items.Count()
                    });
                }
            }



            return detail;
        }

        public List<ReceptionDetailViewDTO> GetDetailByRoadmaps(IEnumerable<int> roadmapIds)
         {
            var roadmaps = roadmapBLL.GetAll().Where(x => roadmapIds.Contains(x.Id));

            var items = roadmaps
                .SelectMany(x => x.Shippings
                    .SelectMany(s => s.ShippingDetail))
                .Select(x => x.Item);

            var groupedItems = items
                .GroupBy(x => x.Article.Id)
                .Select(x => new
                {
                    Article = items.First(i => i.Article.Id == x.Key).Article,
                    Quantity = x.Count()
                });

            return groupedItems.Select(x => new ReceptionDetailViewDTO()
            {
                ArticleId = x.Article.Id,
                Color = x.Article.Color.Name,
                ExpectedQuantity = x.Quantity,
                Size = x.Article.Size.Name,
                ItemType = x.Article.Type.Name
            }).ToList();
        }

        public void Save(ReceptionDTO dto) 
        { 
            //Reception
            int id = dal.Save(mapper.MapToEntity(dto));
            dto.Id = id;

            //Item Status
            dal.UpdateItems((int)ItemStatusEnum.OnLocation, dto.Destination.Id, id);

            //Shippings
            foreach(var roadmap in dto.Roadmaps)
            {
                shippingBLL.Receive(roadmap.Shippings);
            }
            roadmapBLL.Receive(dto.Roadmaps.Select(r => r.Id));

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
