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
            this.emailService = new EmailService();
        }

        public List<RoadmapDTO> GetAll()
        {
            var locations = this.GetLocations();
            var list = this.dal.GetAll().AsEnumerable();

            list = list.Where(x => locations.Select(l => l.Id).Contains(x.Destination.Id));
            list = list.Where(x => x.Origin.Equals(Session.Instance.User.Location));

            return list
                .Select(x => mapper.MapToDTO(x))
                .ToList();
        }

        public List<RoadmapViewDTO> GetAllForView()
        {
            return this.GetAll().Select(x => mapper.MapToViewDTO(x)).ToList();
        }

        public void Send(RoadmapDTO roadmap) 
        { 
            //Roadmap
            int id = dal.Save(mapper.MapToEntity(roadmap));
            roadmap.Id = id;

            //Item Status
            dal.UpdateItems(roadmap.Origin.Id, (int)ItemStatus.ItemStatusByRoadmapStatus[roadmap.Status], id);

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
                  roadmap.Id, Session.Translations[statusName], DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss"), Session.Instance.User.FullName);

                emailService.SendMail(
                    shipping.Responsible.Email,
                    $"{Session.Translations["Shipping"]} {shipping.Id} - { Session.Translations[statusName] }",
                    message);
                }
            );
        }
    }
}
