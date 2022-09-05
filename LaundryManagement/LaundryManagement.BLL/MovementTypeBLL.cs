using LaundryManagement.DAL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.BLL
{
    public class MovementTypeBLL
    {
        private readonly MovementTypeDAL movementTypeDAL;
        public MovementTypeBLL()
        {
            movementTypeDAL = new MovementTypeDAL();
        }

        public List<EnumTypeDTO> GetAll() =>
            this.movementTypeDAL
            .GetAll()
            .Select(x => new EnumTypeDTO()
            {
                Id = x.Id,
                Name = x.Name,
            })
            .ToList();
    }
}
