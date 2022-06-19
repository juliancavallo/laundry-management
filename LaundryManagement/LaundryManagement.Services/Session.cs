using LaundryManagement.Interfaces.Domain.DTOs;
using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;

namespace LaundryManagement.Services
{
    public class Session
    {

        private static object _lock = new Object();
        private static Session _session;
        private static Dictionary<string, int> _loginAttempts = new Dictionary<string, int>();
        private static IList<ILanguageObserver> _observers = new List<ILanguageObserver>();
        private static IDictionary<string, ITranslation> _translations = new Dictionary<string, ITranslation>();

        public IUserDTO User { get; set; }
        public static Dictionary<string, int> LoginAttempts 
        { 
            get { return _loginAttempts; }
            set { _loginAttempts = value; }        
        }
        public static IDictionary<string, ITranslation> Translations
        {
            get { return _translations; }
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
                    LoginAttempts.Clear();
                }
                else
                {
                    throw new Exception("Sesión ya iniciada");
                }
            }
        }

        public static void Logout(ILanguage defaultLanguage)
        {
            lock (_lock)
            {
                if (_session != null)
                {
                    _session = null;
                    LoginAttempts.Clear();
                    Notify(defaultLanguage);
                }
                else
                {
                    throw new Exception("Sesión no iniciada");
                }
            }
        }

        public static void SubscribeObserver(ILanguageObserver observer) => _observers.Add(observer);
        public static void UnsubscribeObserver(ILanguageObserver observer) => _observers.Remove(observer);
        private static void Notify(ILanguage language)
        {
            foreach (var observer in _observers)
            {
                observer.UpdateLanguage(language);
            }
        }
        public static void ChangeLanguage(ILanguage language)
        {
            if(_session != null)
            {
                _session.User.Language = language;
                Notify(language);
            }
        }

        public static void SetTranslations(IDictionary<string, ITranslation> translations) => _translations = translations;

        private Session()
        {
            
        }
    }
}