using System;
using System.Collections.Generic;

namespace LaundryManagement.Domain.DTOs
{
    public class ReceptionDTO
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public UserDTO CreationUser { get; set; }
        public LocationDTO Origin { get; set; }
        public LocationDTO Destination { get; set; }
        public IEnumerable<RoadmapDTO> Roadmaps { get; set; }
        public IEnumerable<ReceptionDetailDTO> ReceptionDetail { get; set; }
    }
}
