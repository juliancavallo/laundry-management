using System;
using System.ComponentModel.DataAnnotations;

namespace LaundryManagement.Domain.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class IntegrityProperty : ValidationAttribute
    {
    }
}
