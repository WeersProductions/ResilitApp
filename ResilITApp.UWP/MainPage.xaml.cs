using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using System.Reflection;

using Syncfusion.SfDataGrid.XForms.UWP;

using Syncfusion.SfPullToRefresh.XForms.UWP;

using Syncfusion.XForms.UWP.PopupLayout;

namespace ResilITApp.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

			SfPopupLayoutRenderer.Init();

			SfPullToRefreshRenderer.Init();

			SfDataGridRenderer.Init();

            LoadApplication(new ResilITApp.App());
        }
    }
}
