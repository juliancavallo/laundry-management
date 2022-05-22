using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Responses;
using LaundryManagement.Services;

namespace LaundryManagement.BLL
{
    public class LoginBLL
    {
        private UserBLL userBLL;

        public LoginBLL()
        {
            userBLL = new UserBLL();
        }

        public LoginResponse Login(LoginDTO dto)
        {
            if (Session.Instance != null)
                throw new Exception("Ya hay una sesión iniciada");

            var userDTO = userBLL.GetAll().Where(x => x.Email == dto.Email).FirstOrDefault();
            if (userDTO == null)
                return new LoginResponse(false, "El susuario no existe");

            if(Encryptor.Hash(dto.Password) != userDTO.Password)
            {
                RegisterAttempt(dto.Email);

                if (Session.LoginAttempts[dto.Email] == 3)
                {
                    this.ResetPassword(userDTO);
                    return new LoginResponse(false, @"Ha superado el limite máximo de intentos de " +
                       "inicio de sesión para el usuario " + dto.Email + ". " +
                       "Le enviaremos a su correo su nueva contraseña," +
                       "la cual recomendamos cambiar la proxima vez que ingrese.");
                }
                
                return new LoginResponse(false, "La contraseña es incorrecta");
            }

            Session.Login(userDTO);
            return new LoginResponse(true);

        }

        public void Logout() => Session.Logout();

        public bool IsLogged() => Session.Instance != null;

        private void RegisterAttempt(string email) 
        { 
            if(!Session.LoginAttempts.ContainsKey(email))
                Session.LoginAttempts.Add(email, 0);

            Session.LoginAttempts[email] += 1;
        }

        private void ResetPassword(UserDTO dto)
        {
            Session.LoginAttempts.Remove(dto.Email);

            var password = userBLL.ResetPassword(dto);

            string message = string.Format(
                    @"<p>Su nueva contraseña es <h1>{0}</h1> </p> " +
                    "<p>Recuerde cambiarla cuando inicie sesión nuevamente</p>" +
                    "<p>Muchas gracias</p>",
                    password);

            EmailService emailService = new EmailService();
            //emailService.SendMail(dto.Email, "Password Reset", message);
        }
    }
}