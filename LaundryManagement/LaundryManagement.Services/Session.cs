using LaundryManagement.Interfaces.Domain.DTOs;

namespace LaundryManagement.Services
{
    public class Session
    {

        private static object _lock = new Object();
        private static Session _session;

        public IUserDTO User { get; set; }
        public DateTime FechaInicio { get; set; }

        public static Session Instance
        {
            get
            {
                return _session;
            }
        }

        public static void Login(IUserDTO user)
        {

            lock (_lock)
            {
                if (_session == null)
                {
                    _session = new Session();
                    _session.User = user;
                    _session.FechaInicio = DateTime.Now;
                }
                else
                {
                    throw new Exception("Sesión ya iniciada");
                }
            }
        }

        public static void Logout()
        {
            lock (_lock)
            {
                if (_session != null)
                {
                    _session = null;
                }
                else
                {
                    throw new Exception("Sesión no iniciada");
                }
            }


        }

        private Session()
        {

        }
    }
}