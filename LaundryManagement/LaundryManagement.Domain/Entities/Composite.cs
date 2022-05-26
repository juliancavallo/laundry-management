using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Entities
{
    public class Composite : Component
    {
        private IList<Component> _children;

        public Composite()
        {
            _children = new List<Component>();
        }
        public override IList<Component> Children
        {
            get { return _children; }
        }

        public override void AddChildren(Component component) => _children.Add(component);
        
        public override void RemoveChildren(Component component) => _children.Remove(component);
    }
}
