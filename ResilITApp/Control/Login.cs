using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using ResilITApp.Control;
using ResilITApp.Model;
using Xamarin.Forms;

namespace ResilITApp
{
    public class Login
    {
        private static Login instance = null;
        private static readonly object padlock = new object();

        public static Login Instance
        {
            get
            {
                lock(padlock)
                {
                    if(instance == null)
                    {
                        instance = new Login();
                    }
                    return instance;
                }
            }
        }

        private const string URL = "http://10.0.2.2";
        private HttpClient _client;

        private List<IUserObserver> _observers;

        public Login () {
            _observers = new List<IUserObserver>();
            _client = new HttpClient();
		}

        private bool isLoggedIn;
        public bool IsLoggedIn
        {
            get
            {
                return isLoggedIn;
            }
            set
            {
                isLoggedIn = value;
                if(isLoggedIn)
                {
                    GetUser();
                }
            }
        }

        private UserModel user;
        public UserModel User
        {
            get
            {
                return user;
            }
            set
            {
                // TODO: add caching with time here.
                bool lost = user != null && value == null;
                if (lost)
                {
                    foreach(IUserObserver observer in _observers)
                    {
                        observer.OnUserLost();
                    }
                }

                user = value;

                if(!lost)
                {
                    foreach (IUserObserver observer in _observers)
                    {
                        observer.OnUserReceived(user);
                    }
                }
            }
        }

        public void DoLogout()
        {
            IsLoggedIn = false;
            User = null;
        }

        public async System.Threading.Tasks.Task<bool> DoLoginAsync(SignInModel user)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("email", user.email),
                new KeyValuePair<string, string>("password", user.password),
            });

            var request = await _client.PostAsync(URL + "/login", formContent);
            request.EnsureSuccessStatusCode();
            if (request.RequestMessage.RequestUri.AbsolutePath == "/login")
            {
                // We failed.
                IsLoggedIn = false;
                return false;
            }

            IsLoggedIn = true;
            return true;
        }

        public async System.Threading.Tasks.Task<bool> GetUser()
        {
            var request = await _client.GetAsync(URL + "/api/user");
            if(!request.IsSuccessStatusCode)
            {
                //TODO: try again?
                return false;
            }

            string json = await request.Content.ReadAsStringAsync();

            User = JsonConvert.DeserializeObject<UserModel>(json);

            return true;
        }

        public void AddUserObserver(IUserObserver observer)
        {
            if(!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }
    }
}
