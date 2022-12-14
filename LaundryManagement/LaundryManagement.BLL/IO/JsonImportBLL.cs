using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LaundryManagement.BLL.IO
{
    public class JsonImportBLL
    {
        public T Deserialize<T>(string json)
        {
            try
            {
                var deserialized = JsonSerializer.Deserialize(json, typeof(T));

                return (T)deserialized;
            }
            catch (Exception)
            {
                throw new ValidationException("Error converting file", ValidationType.Error);
            }
        }
    }
}
