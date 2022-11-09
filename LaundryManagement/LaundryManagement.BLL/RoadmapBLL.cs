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
    public class RoadmapBLL
    {
        private RoadmapMapper mapper;
        private ItemMapper itemMapper;
        private RoadmapDAL dal;
        private ShippingBLL shippingBLL;
        private LocationBLL locationBLL;
        private LogBLL logBLL;
        private ItemBLL itemBLL;
        private TraceabilityBLL traceabilityBLL;
        private EmailService emailService;

        public RoadmapBLL()
        {
            this.dal = new RoadmapDAL();
            this.itemBLL = new ItemBLL();
            this.shippingBLL = new ShippingBLL();
            this.locationBLL = new LocationBLL();
            this.logBLL = new LogBLL();
            this.traceabilityBLL = new TraceabilityBLL();
            this.mapper = new RoadmapMapper();
            this.itemMapper = new ItemMapper();
            this.emailService = new EmailService();
        }

        public List<RoadmapDTO> GetAll()
        {
            //var locations = this.GetLocations();
            var list = this.dal.GetAll().AsEnumerable();

            //list = list.Where(x => locations.Select(l => l.Id).Contains(x.Destination.Id));
            //list = list.Where(x => x.Origin.Equals(Session.Instance.User.Location));

            return list
                .Select(x => mapper.MapToDTO(x))
                .ToList();
        }

        public RoadmapDTO GetById(int id)
        {
            var roadmap = this.dal.GetAll().First(x => x.Id == id);

            return mapper.MapToDTO(roadmap);
        }

        public IEnumerable<RoadmapDTO> GetByIds(IEnumerable<int> ids)
        {
            var roadmaps = this.dal.GetAll().Where(x => ids.Contains(x.Id));

            return roadmaps.Select(x => mapper.MapToDTO(x));
        }

        public List<RoadmapViewDTO> GetAllForView(RoadmapFilter filter)
        {
            var list = this.GetAll().AsEnumerable();

            if (filter.DateFrom != DateTime.MinValue)
                list = list.Where(x => x.CreatedDate >= filter.DateFrom.Date);

            if (filter.DateTo != DateTime.MinValue)
                list = list.Where(x => x.CreatedDate <= filter.DateTo.Date.AddDays(1).AddTicks(-1));

            if (filter.IdLocationOrigin.HasValue)
                list = list.Where(x => x.Origin.Id == filter.IdLocationOrigin);

            if (filter.IdLocationDestination.HasValue)
                list = list.Where(x => x.Destination.Id == filter.IdLocationDestination);

            if (filter.Status.HasValue)
                list = list.Where(x => x.Status == filter.Status);

            return list.Select(x => mapper.MapToViewDTO(x)).ToList();
        }

        public List<ReceptionRoadmapViewDTO> GetForCreateReception(RoadmapFilter filter)
        {
            var roadmaps = this.GetAllForView(filter);

            return roadmaps.Select(x => new ReceptionRoadmapViewDTO()
            {
                CreatedDate = x.CreatedDate,
                Id = x.Id,
                Number = x.Number,
                Selected = false
            }).ToList();
        }

        public List<ProcessDetailViewDTO> GetDetailForView(int roadmapId)
        {
            var detail = dal.GetDetailByRoadmapId(roadmapId);
            var listView = detail.Select(x => itemMapper.MapToViewDTO(x)).ToList();

            var result = new List<ProcessDetailViewDTO>();
            foreach (var item in listView)
            {
                result.AddOrUpdate(item);
            }
            return result;
        }

        public void Send(RoadmapDTO roadmap) 
        { 
            //Roadmap
            int id = dal.Save(mapper.MapToEntity(roadmap));
            roadmap.Id = id;

            //Item Status
            dal.UpdateItems((int)ItemStatus.ItemStatusByRoadmapStatus[roadmap.Status], roadmap.Origin.Id, id);

            //ShippingStatus
            shippingBLL.Send(roadmap.Shippings);

            //Traceability
            var traceabilityList = roadmap.Shippings.SelectMany(x => x.ShippingDetail).Select(x => new TraceabilityDTO()
            {
                Date = DateTime.Now,
                Destination = roadmap.Destination,
                Origin = roadmap.Origin,
                Item = x.Item,
                User = roadmap.CreationUser,
                ItemStatus = ItemStatus.ItemStatusByRoadmapStatus[roadmap.Status],
                MovementType = MovementTypeEnum.RoadMap
            }).ToList();

            traceabilityBLL.Save(traceabilityList);

            //Send email
            SendEmail(roadmap);

            logBLL.LogInfo(MovementTypeEnum.RoadMap, $"The roadmap {roadmap.Id} has been created");
        }

        public void Receive(IEnumerable<int> ids)
        {
            dal.Receive(ids);

            logBLL.LogInfo(MovementTypeEnum.RoadMap, $"The roadmaps {string.Join(',', ids)} has been received");
        }

        public List<LocationDTO> GetLocations()
        {
            var userLocation = Session.Instance.User.Location;

            return locationBLL.GetAll()
                .Where(x => 
                    !x.IsInternal 
                    && x.LocationType != userLocation.LocationType
                    && !x.Equals(userLocation))
                .ToList();
        }

        private void SendEmail(RoadmapDTO roadmap)
        {
            roadmap.Shippings.ForEach(shipping => 
            {
                var statusName = shippingBLL.GetStatusName((int)shipping.Status);

                var message = string.Format(Session.Translations[Tags.ShippingEmailBody],
                  shipping.Id, Session.Translations[statusName], DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss"), Session.Instance.User.FullName);

                emailService.SendMail(
                    shipping.Responsible.Email,
                    $"{Session.Translations["Shipping"]} {shipping.Id} - { Session.Translations[statusName] }",
                    message);
                }
            );
        }
    }
}
