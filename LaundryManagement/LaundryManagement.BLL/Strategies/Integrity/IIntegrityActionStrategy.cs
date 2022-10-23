using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.BLL.Strategies
{
    public interface IIntegrityActionStrategy
    {
        public void RestoreIntegrity();
    }
}
