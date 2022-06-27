using LaundryManagement.BLL.Mappers;
using LaundryManagement.DAL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Domain.Extensions;
using LaundryManagement.Domain.Filters;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LaundryManagement.BLL
{
    public class TraceabilityBLL
    {
        private TraceabilityMapper traceabilityMapper;
        private TraceabilityDAL traceabilityDAL;
        public TraceabilityBLL()
        {
            this.traceabilityDAL = new TraceabilityDAL();
            this.traceabilityMapper  = new TraceabilityMapper();
        }

        
        public void Save(List<TraceabilityDTO> list) 
        {
            var entities = list.Select(x => traceabilityMapper.MapToEntity(x)).ToList();

            traceabilityDAL.Save(entities);
        }

        
    }
}
