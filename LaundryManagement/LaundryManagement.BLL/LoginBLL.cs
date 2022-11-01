﻿using LaundryManagement.Domain;
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
        private UserBLL userBLL;
        private TranslatorBLL translatorBLL;
        private LogBLL logBLL;

        private EmailService emailService;

        private int maxLoginAttempts;

        public LoginBLL()
        {
            userBLL = new UserBLL();
            translatorBLL = new TranslatorBLL();
            logBLL = new LogBLL();

            emailService = new EmailService();

            maxLoginAttempts = Session.Settings.PasswordPolicy.MaxLoginAttempts;
        }

        public UserDTO Login(LoginDTO dto)
        {
            if (Session.Instance != null)
                throw new ValidationException(Session.Translations[Tags.SessionAlreadyOpen], ValidationType.Error);

            var filter = new UserFilter(email: dto.Email);
            var userDTO = userBLL.GetByFilter(filter).FirstOrDefault();
            if (userDTO == null)
                throw new ValidationException(Session.Translations[Tags.NonexistentUser], ValidationType.Error);

            if(Encryptor.HashToString(dto.Password) != userDTO.Password)
            {
                RegisterAttempt(dto.Email);

                if (Session.LoginAttempts[dto.Email] == maxLoginAttempts)
                {
                    this.ResetPassword(userDTO);
                    throw new LoginAttemptsException(Session.Translations[Tags.PasswordLimitMessage]);
                }
                
                throw new ValidationException(Session.Translations[Tags.IncorrectPassword], ValidationType.Error);
            }

            Session.Login(userDTO);
            logBLL.LogInfo(MovementTypeEnum.Login, $"The user {userDTO.FullName} logged in successfully");

            return userDTO;
        }

        public void Logout() 
        { 
            if(Session.Instance != null)
            {
                logBLL.LogInfo(MovementTypeEnum.Logout, $"The user {Session.Instance.User.FullName} logged out successfully");
                Session.Logout(translatorBLL.GetDefaultLanguage());
            }
        }

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

            var newPassword = Encryptor.GenerateRandom();
            dto.Password = Encryptor.HashToString(newPassword);
            userBLL.Save(dto);

            string message = string.Format(Session.Translations[Tags.PasswordResetEmailBody], newPassword);

            emailService.SendMail(dto.Email, Session.Translations[Tags.PasswordResetEmailSubject], message);

            logBLL.LogInfo(MovementTypeEnum.ResetPassword, $"The password for the user {dto.FullName} has been reset");
        }
    }
}