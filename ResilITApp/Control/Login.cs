﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
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

        public async Task<bool> GetUser(bool forceRefresh = false)
        {
            // TODO: auto refresh every x time. 
            if(User != null && !forceRefresh)
            {
                return true;
            }

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

        public async Task<UserModel> GiveUser(bool forceRefresh = false)
        {
            await GetUser(forceRefresh);
            return User;
        }

        public void AddUserObserver(IUserObserver observer)
        {
            if(!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        public async Task<HttpMessage> DoPost(string url)
        {
            if(!url.StartsWith("/", StringComparison.Ordinal))
            {
                url = $"/{url}";
            }

            HttpMessage result = new HttpMessage();
            var request = await _client.PostAsync(URL + url, new StringContent(""));

            result.Success = request.IsSuccessStatusCode;
            if (request.RequestMessage.RequestUri.AbsolutePath == "/login" && !url.Equals("/login"))
            {
                // we're not logged in. The user should log in again.
                result.Message = "Not logged in.";
                result.Success = false;
            }
            result.Response = request;
            return result;
        }
    }
}
