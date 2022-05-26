﻿using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Entities
{
    public class User : IEntity
    {
        public User()
        {
            _permissions = new List<Component>();
        }
        private List<Component> _permissions { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public IList<Component> Permissions { get { return _permissions; } }
    }
}
