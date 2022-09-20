using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Enums
{
    public enum MovementTypeEnum
    {
        LaundryShipping = 1,
        ClinicShipping = 2,
        InternalShipping = 3,
        RoadMap = 4,
        LaundryReception = 5,
        ClinicReception = 6,
        Login = 7,
        ResetPassword = 8,
        Logout = 9,
        ManualPasswordReset = 10,
        UserCreate = 11,
        UserDelete = 12,
        UserHistory = 13
    }
}
