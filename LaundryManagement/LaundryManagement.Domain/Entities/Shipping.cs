using LaundryManagement.Domain.DataAnnotations;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Entities
{
    public class Shipping : Process, ICheckDigitEntity
    {
        public int Id { get; set; }
        public ShippingStatus Status { get; set; }
        public List<ShippingDetail> ShippingDetail { get; set; }
        public ShippingType Type { get; set; }
        public User Responsible { get; set; }
        public byte[] CheckDigit { get; set; }
        [IntegrityProperty]
        public int IdShippingStatus { get; set; }
        [IntegrityProperty]
        public int IdLocationOrigin { get; set; }
        [IntegrityProperty]
        public int IdLocationDestination { get; set; }
        [IntegrityProperty]
        public int IdShippingType { get; set; }
        [IntegrityProperty]
        public int IdCreatedUser { get; set; }
    }
}
