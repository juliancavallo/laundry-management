using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Entities
{
    public class Leaf : Component
    {
        public override IList<Component> Children
        {
            get { throw new NotImplementedException(); }
        }
        public override void AddChildren(Component component) => throw new NotImplementedException();
        public override void RemoveChildren(Component component) => throw new NotImplementedException();
    }
}
