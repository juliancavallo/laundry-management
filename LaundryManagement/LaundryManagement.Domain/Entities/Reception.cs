using System.Collections.Generic;

namespace LaundryManagement.Domain.Entities
{
    public class Reception : Process
    {
        public int Id { get; set; }
        public IEnumerable<ReceptionDetail> ReceptionDetail { get; set; }
        public IEnumerable<Roadmap> Roadmaps { get; set; }
    }
}
