using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using ResilITApp.Model;

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

        public Login () {
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
            }
        }

        public void DoLogout()
        {
            IsLoggedIn = false;
        }

        public async void DoLogin(SignInModel user)
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
                // TODO: send feedback to user that login is wrong.
                return;
            }

            var response = await request.Content.ReadAsStringAsync();

            IsLoggedIn = true;
        }
    }
}
