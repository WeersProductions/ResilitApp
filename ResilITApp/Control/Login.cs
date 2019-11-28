using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
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

        public const string URL = "https://www.resilit.snic.nl/";
        private HttpClient _client;

        private List<IUserObserver> _observers;

        private List<CancellationTokenSource> _cancellationTokenSources;

        public Login () {
            _observers = new List<IUserObserver>();
            _cancellationTokenSources = new List<CancellationTokenSource>();
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.AllowAutoRedirect = true;
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            _client = new HttpClient(httpClientHandler);
            _client.BaseAddress = new Uri(URL);
            
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
                     _ = GetUser();
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

        public async Task<HttpMessage> DoLoginAsync(SignInModel user)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("email", user.email),
                new KeyValuePair<string, string>("password", user.password),
            });
            HttpMessage result = new HttpMessage();
            try
            {
                var request = await _client.PostAsync("login", formContent);
                string json = await request.Content.ReadAsStringAsync();
                if (json.Contains("/login"))
                {
                    // We failed.
                    IsLoggedIn = false;
                    result.Success = false;
                    result.Message = "Incorrect username or password.";
                }
                else
                {
                    IsLoggedIn = true;
                    result.Success = true;
                }
            }
            catch (HttpRequestException)
            {
                IsLoggedIn = false;
                result.Message = "No connection.";
            }
            
            return result;
        }

        public async Task<HttpMessage> DoRegisterAsync(RegisterModel registerModel)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("code", registerModel.code),
                new KeyValuePair<string, string>("firstname", registerModel.firstname),
                new KeyValuePair<string, string>("surname", registerModel.surname),
                new KeyValuePair<string, string>("vereniging", registerModel.vereniging.ToString()),
                new KeyValuePair<string, string>("bus", registerModel.bus.ToString()),
                new KeyValuePair<string, string>("programme", registerModel.programme),
                new KeyValuePair<string, string>("programmeOther", registerModel.programmeOther),
                new KeyValuePair<string, string>("companyName", registerModel.companyName),
                new KeyValuePair<string, string>("vegetarian", registerModel.vegetarian.ToString()),
                new KeyValuePair<string, string>("specialNeeds", registerModel.specialNeeds),
                new KeyValuePair<string, string>("email", registerModel.email),
                new KeyValuePair<string, string>("password", registerModel.password),
                new KeyValuePair<string, string>("confirm", registerModel.confirm),
                new KeyValuePair<string, string>("privacyPolicyAgree", registerModel.privacyPolicyAgree.ToString()),
                new KeyValuePair<string, string>("subscribe", registerModel.subscribe.ToString()),
            });
            HttpMessage result = new HttpMessage();
            try
            {
                var request = await _client.PostAsync("register", formContent);
                string json = await request.Content.ReadAsStringAsync();
                if (json.Contains("/register"))
                {
                    // We failed.
                    IsLoggedIn = false;
                    result.Success = false;
                    result.Message = "Incorrect ticketcode.";
                }
                else
                {
                    IsLoggedIn = true;
                    result.Success = true;
                }
            }
            catch (HttpRequestException)
            {
                IsLoggedIn = false;
                result.Message = "No connection.";
            }
            return result;
        }

        public async Task<bool> GetUser(bool forceRefresh = false)
        {
            // TODO: auto refresh every x time. 
            if(User != null && !forceRefresh)
            {
                return true;
            }

            var request = await _client.GetAsync("api/user");
            if(!request.IsSuccessStatusCode)
            {
                //TODO: try again?
                IsLoggedIn = false;
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
            if(IsLoggedIn && User != null)
            {
                observer.OnUserReceived(User);
            }
        }

        private void CancelTasks()
        {
            foreach(var token in _cancellationTokenSources)
            {
                if(token != null)
                {
                    token.Cancel();
                }
            }
        }

        private CancellationTokenSource AddCancellableTask()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            _cancellationTokenSources.Add(cts);
            return cts;
        }

        public async Task<HttpMessage> DoGet(string url, bool cancellable = true)
        {
            if (url.StartsWith("/", StringComparison.Ordinal))
            {
                url = url.Substring(1);
            }

            HttpMessage result = new HttpMessage();
            try
            {
                var request = cancellable ? await _client.GetAsync(url, AddCancellableTask().Token) : await _client.GetAsync(url);

                result.Success = request.IsSuccessStatusCode;
                if (request.RequestMessage.RequestUri.AbsolutePath == "/login" && !url.Equals("/login"))
                {
                    // we're not logged in. The user should log in again.
                    result.Message = "Not logged in.";
                    result.Success = false;
                }
                result.Response = request;
            }
            catch (OperationCanceledException)
            {
                result.Message = "Cancelled request";
            }
            catch (HttpRequestException e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        public async Task<HttpMessage> DoPost(string url)
        {
            return await DoPost(url, new StringContent(""));

        }

        public async Task<HttpMessage> DoPost(string url, HttpContent stringContent)
        {
            if (url.StartsWith("/", StringComparison.Ordinal))
            {
                url = url.Substring(1);
            }

            HttpMessage result = new HttpMessage();
            try
            {
                var request = await _client.PostAsync(url, stringContent);

                result.Success = request.IsSuccessStatusCode;
                if (request.RequestMessage.RequestUri.AbsolutePath == "/login" && !url.Equals("/login"))
                {
                    // we're not logged in. The user should log in again.
                    result.Message = "Not logged in.";
                    result.Success = false;
                }
                result.Response = request;
            }
            catch (HttpRequestException e)
            {
                result.Message = e.Message;
            }
            return result;
        }
    }
}
