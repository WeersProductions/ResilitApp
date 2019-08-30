using System;
using System.Collections.Generic;
using System.ComponentModel;
using ResilITApp.Model;

namespace ResilITApp.Control
{
    public static class AppController
    {
        private static List<AppModel> _listeners;

        private static HashSet<object> _bussyOwners;

        public static bool IsBussy
        {
            get
            {
                return _bussyOwners.Count > 0;
            }
        }

        public static void AddBusy(object owner)
        {
            if(!_bussyOwners.Contains(_bussyOwners))
            {
                _bussyOwners.Add(owner);
                NotifyListeners();
            }
        }

        public static void RemoveBusy(object owner)
        {
            _bussyOwners.Remove(owner);
            NotifyListeners();
        }

        static AppController()
        {
            _listeners = new List<AppModel>();
            _bussyOwners = new HashSet<object>();
        }

        public static void AddListener(AppModel appModel)
        {
            if(!_listeners.Contains(appModel))
            {
                _listeners.Add(appModel);
            }
        }

        public static void RemoveListener(AppModel appModel)
        {
            _listeners.Remove(appModel);
        }

        public static void NotifyListeners()
        {
            foreach(AppModel listener in _listeners)
            {
                listener.RefreshBussy();
            }
        }
    }
}
