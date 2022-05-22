using LaundryManagement.Interfaces.Domain.DTOs;
using LaundryManagement.Interfaces.Domain.Responses;

namespace LaundryManagement.Domain.Responses
{
    public class LoginResponse : IGenericResponse
    {
        public LoginResponse(bool success, string message = "")
        {
            Success = success;  
            Message = message;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
