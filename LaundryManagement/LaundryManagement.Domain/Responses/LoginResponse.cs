using LaundryManagement.Interfaces.Domain.DTOs;
using LaundryManagement.Interfaces.Domain.Responses;

namespace LaundryManagement.Domain.Responses
{
    public class LoginResponse : IGenericResponse
    {
        public LoginResponse(bool success, string message = "", IUserDTO user = null)
        {
            Success = success;  
            Message = message;
            User = user;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public IUserDTO User { get; set; }
    }
}
