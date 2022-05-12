﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Interfaces.Domain.Entities
{
    public interface ICrud<T>
    {
        T GetById(int id);
        IList<T> GetAll();
        void Save(T entity);
        void Delete(T entity);
    }
}
