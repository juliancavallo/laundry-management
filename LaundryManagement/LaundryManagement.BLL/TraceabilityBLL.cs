using LaundryManagement.BLL.Mappers;
using LaundryManagement.DAL;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Filters;
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
        
        public List<TraceabilityViewDTO> GetForView(TraceabilityFilter filter)
        {
            var list = traceabilityDAL.Get(filter.Code).AsEnumerable();

            if (filter.MovementType.HasValue && filter.MovementType.Value != 0)
                list = list.Where(x => x.MovementType.Id == (int)filter.MovementType);

            if (filter.ItemStatus.HasValue && filter.ItemStatus.Value != 0)
                list = list.Where(x => x.ItemStatus.Id == (int)filter.ItemStatus);

            return list.Select(x => traceabilityMapper.MapToViewDTO(x)).ToList();
        }
        
    }
}
