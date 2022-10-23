using LaundryManagement.BLL.Strategies;
using LaundryManagement.DAL;
using LaundryManagement.Domain.DataAnnotations;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Interfaces.Domain.Entities;
using LaundryManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LaundryManagement.BLL
{
    public class CheckDigitBLL
    {
        private readonly LogBLL _logBLL;
        private readonly CheckDigitDAL _checkDigitDAL;
        private readonly Dictionary<string, Type> _restoreIntegrityStrategies;

        public CheckDigitBLL()
        {
            _logBLL = new LogBLL();
            _checkDigitDAL = new CheckDigitDAL();
            _restoreIntegrityStrategies = new Dictionary<string, Type>()
            {
                { Tags.IntegrityRecalculate, typeof(RecalculateDigitsStrategy) },
                { Tags.IntegrityLastBackup, typeof(RestoreBackupStrategy) },
            };
        }

        public byte[] GenerateHorizontalCheckDigit(ICheckDigitEntity entity)
        {
            var seed = "";
            var type = entity.GetType();
            foreach (var property in type.GetProperties())
            {
                if (Attribute.IsDefined(property, typeof(IntegrityProperty)))
                    seed += property.Name + property.GetValue(entity)?.ToString() ?? "null";
            }

            return Encryptor.HashToByteArray(seed);

        }

        public byte[] GenerateVerticalCheckDigit(Type entityType)
        {
            var checkDigits = new List<byte[]>();
            var list = _checkDigitDAL.GetAllRows(entityType, new string[] { "CheckDigit", "Id" });

            checkDigits.AddRange(list.Select(x => x.CheckDigit));

            return Encryptor.HashToByteArrayFromList(checkDigits);
        }

        public IEnumerable<ICheckDigitEntity> GetHorizontalCorruptedEntities()
        {
            var corruptedEntities = new List<ICheckDigitEntity>();
            var checkeableType = typeof(ICheckDigitEntity);
            var typesToCheck = typeof(Domain.Entities.Article).Assembly
                .GetTypes()
                .Where(entityType => checkeableType.IsAssignableFrom(entityType));

            foreach (var type in typesToCheck)
            {
                var list = _checkDigitDAL.GetAllRows(type, new string[] { "CheckDigit", "Id" });

                foreach (var item in list)
                {
                    var newCheckDigit = this.GenerateHorizontalCheckDigit(item);
                    if (!newCheckDigit.SequenceEqual(item.CheckDigit))
                        corruptedEntities.Add(item);
                }
            }

            if(corruptedEntities.Count > 0)
                _logBLL.LogError(MovementTypeEnum.CorruptedEntities, $"There are {corruptedEntities.Count} corrupted entities");

            return corruptedEntities;
        }

        public IEnumerable<Type> GetVerticalCorruptedEntities()
        {
            var corruptedEntities = new List<Type>();
            var verticalCheckDigits = _checkDigitDAL.GetAllVerticalCheckDigits();

            var checkeableType = typeof(ICheckDigitEntity);
            var typesToCheck = typeof(Domain.Entities.Article).Assembly
                .GetTypes()
                .Where(entityType => checkeableType.IsAssignableFrom(entityType));

            foreach (var type in typesToCheck)
            {
                var generatedCheckDigit = this.GenerateVerticalCheckDigit(type);
                var currentCheckDigit = verticalCheckDigits.FirstOrDefault(x => x.TableName == type.Name)?.CheckDigit;
                
                if (currentCheckDigit == null || !generatedCheckDigit.SequenceEqual(currentCheckDigit))
                    corruptedEntities.Add(type);
            }

            if (corruptedEntities.Count > 0)
                _logBLL.LogError(MovementTypeEnum.CorruptedEntities, $"There are {corruptedEntities.Count} corrupted entities");

            return corruptedEntities;
        }

        public void SaveVerticalCheckDigit(Type type) =>
            this._checkDigitDAL.SaveVerticalCheckDigit(type, this.GenerateVerticalCheckDigit(type));

        public void ValidateAdminCredentials(string user, string password)
        {
            if (user != Session.Settings.IntegrityAdminUser)
                throw new ValidationException(Session.Translations[Tags.NonexistentUser], ValidationType.Error);

            if (Encryptor.HashToString(password) != Session.Settings.IntegrityAdminPassword)
                throw new ValidationException(Session.Translations[Tags.IncorrectPassword], ValidationType.Error);
        }

        public void RestoreIntegrity(string selectedAction)
        {
            var strategyType = this._restoreIntegrityStrategies[selectedAction];
            var strategy = Activator.CreateInstance(strategyType) as IIntegrityActionStrategy;

            _logBLL.LogWarning(
                MovementTypeEnum.IntegrityRestore, 
                $"The integrity of the databaes has been established with {strategyType.Name} strategy");

            strategy.RestoreIntegrity();
        }
    }
}
