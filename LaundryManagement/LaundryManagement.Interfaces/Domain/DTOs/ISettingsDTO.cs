using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Interfaces.Domain.DTOs
{
    public interface ISettingsDTO
    {
        public string ImagePath { get; set; }
        public string ConnectionString { get; set; }
        public IPasswordPolicyDTO PasswordPolicy { get; set; }
        public IEmailSettingsDTO EmailSettings { get; set; }
        public string BackupPath { get; set; }
        public int LogLevel { get; set; }
    }

    public interface IPasswordPolicyDTO
    {
        public int MaxLoginAttempts { get; set; }
        public int PasswordMinLength { get; set; }
        public int PasswordMinSpecialCharacters { get; set; }
        public int PasswordMinUppercase { get; set; }
        public int PasswordMinLowercase { get; set; }
        public int PasswordMinNumbers { get; set; }
    }

    public interface IEmailSettingsDTO
    {
        public string Address { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string ApplicationPassword { get; set; }
    }
}
