using LaundryManagement.Interfaces.Domain.DTOs;

namespace LaundryManagement.Services
{
    public class Session
    {

        private static object _lock = new Object();
        private static Session _session;
        private static Dictionary<string, int> _loginAttempts = new Dictionary<string, int>();

        public IUserDTO User { get; set; }
        public DateTime FechaInicio { get; set; }
        public static Dictionary<string, int> LoginAttempts 
        { 
            get { return _loginAttempts; }
            set { _loginAttempts = value; }        
        }

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
                    LoginAttempts.Clear();
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
                    LoginAttempts.Clear();
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