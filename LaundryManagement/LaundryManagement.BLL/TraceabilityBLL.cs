using LaundryManagement.BLL.Mappers;
using LaundryManagement.DAL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
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
        
        public List<TraceabilityViewDTO> GetForView(string code)
        {
            return traceabilityDAL.Get(code).Select(x => traceabilityMapper.MapToViewDTO(x)).ToList();
        }
        
    }
}
