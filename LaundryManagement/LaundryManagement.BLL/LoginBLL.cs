using LaundryManagement.Domain;
using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Domain.Exceptions;
using LaundryManagement.Domain.Filters;
using LaundryManagement.Services;
using System;
using System.Linq;

namespace LaundryManagement.BLL
{
    public class LoginBLL
    {
        private Configuration configuration;
        private UserBLL userBLL;
        private int maxLoginAttempts = 1;
        private SeedService seedService;

        public LoginBLL()
        {
            configuration = new Configuration();
            userBLL = new UserBLL();
            seedService = new SeedService(configuration.GetValue<string>("connectionString"));

            maxLoginAttempts = configuration.GetValue<int>("maxLoginAttempts");
        }

        public void Login(LoginDTO dto)
        {
            if (Session.Instance != null)
                throw new ValidationException("A session is already open", ValidationType.Error);

            var filter = new UserFilter(email: dto.Email);
            var userDTO = userBLL.GetByFilter(filter).FirstOrDefault();
            if (userDTO == null)
                throw new ValidationException("User does not exists", ValidationType.Error);

            if(Encryptor.Hash(dto.Password) != userDTO.Password)
            {
                RegisterAttempt(dto.Email);

                if (Session.LoginAttempts[dto.Email] == maxLoginAttempts)
                {
                    this.ResetPassword(userDTO);
                    throw new ValidationException(@"Ha superado el limite máximo de intentos de " +
                       "inicio de sesión para el usuario " + dto.Email + ". " +
                       "Le enviaremos a su correo su nueva contraseña," +
                       "la cual recomendamos cambiar la proxima vez que ingrese.", ValidationType.Warning);
                }
                
                throw new ValidationException("The password is incorrect", ValidationType.Error);
            }

            Session.Login(userDTO);

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

        public void SeedData() => seedService.SeedData();
    }
}