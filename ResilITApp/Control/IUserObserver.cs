using System;
using ResilITApp.Model;

namespace ResilITApp.Control
{
    public interface IUserObserver
    {
        void OnUserReceived(UserModel user);

        void OnUserLost();
    }
}
