using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Responses;
using LaundryManagement.Services;

namespace LaundryManagement.BLL
{
    public class LoginBLL
    {
        private string mockEmail = "a@a.com";
        private string mockPassword = Encryptor.Hash("1234");
        private UserBLL userBLL;

        public LoginBLL()
        {
            userBLL = new UserBLL();
        }

        public LoginResponse Login(LoginDTO dto)
        {
            if (dto.Email == mockEmail && Encryptor.Hash(dto.Password) == mockPassword)
            {
                var userDTO = userBLL.GetAll().Where(x => x.Email == dto.Email).First();
                return new LoginResponse(true, null, userDTO);
            }

            return new LoginResponse(false, "Los datos ingresados son incorrectos");
        }
    }
}