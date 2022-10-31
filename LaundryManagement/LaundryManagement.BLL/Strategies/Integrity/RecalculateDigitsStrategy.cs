using LaundryManagement.DAL;
using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.BLL.Strategies
{
    public class RecalculateDigitsStrategy : IIntegrityActionStrategy
    {
        private readonly CheckDigitDAL _checkDigitDAL;
        private CheckDigitBLL _checkDigitBLL;

        public RecalculateDigitsStrategy()
        {
            _checkDigitBLL = new CheckDigitBLL();
            _checkDigitDAL = new CheckDigitDAL();
        }

        public void RestoreIntegrity()
        {
            var corruptedEntities = _checkDigitBLL.GetHorizontalCorruptedEntities();

            foreach(var entity in corruptedEntities)
            {
                var newCheckDigit = _checkDigitBLL.GenerateHorizontalCheckDigit(entity);

                if (!newCheckDigit.SequenceEqual(entity.CheckDigit))
                    _checkDigitDAL.UpdateHorizontalCheckDigit(entity, newCheckDigit);
            }

            var verticalCorruptedEntities = _checkDigitBLL.GetVerticalCorruptedEntities();
            foreach (var type in verticalCorruptedEntities)
            {
                var newCheckDigit = _checkDigitBLL.GenerateVerticalCheckDigit(type);

                if (newCheckDigit == null)
                    _checkDigitDAL.DeleteVerticalCheckDigit(type);
                else
                    _checkDigitDAL.SaveVerticalCheckDigit(type, newCheckDigit);
            }
        }
    }
}
