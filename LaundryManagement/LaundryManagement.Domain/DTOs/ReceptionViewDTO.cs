namespace LaundryManagement.Domain.DTOs
{
    public class ReceptionViewDTO
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string CreationDate { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
    }
}
