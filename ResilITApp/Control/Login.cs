using System;
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

        public void DoLogin()
        {
            IsLoggedIn = true;
        }
    }
}
