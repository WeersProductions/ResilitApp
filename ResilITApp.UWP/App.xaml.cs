using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
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

using Syncfusion.SfPdfViewer.XForms.UWP; 

using Syncfusion.SfRangeSlider.XForms.UWP; 

using Syncfusion.SfSchedule.XForms.UWP;

using Syncfusion.XForms.UWP.ProgressBar; 

using Syncfusion.SfCalendar.XForms.UWP;

using Syncfusion.SfBusyIndicator.XForms.UWP;

using Syncfusion.SfNavigationDrawer.XForms.UWP;

using Syncfusion.SfNumericTextBox.XForms.UWP;

using Syncfusion.SfNumericUpDown.XForms.UWP;

using Syncfusion.SfRadialMenu.XForms.UWP;

using Syncfusion.SfPullToRefresh.XForms.UWP;

using Syncfusion.XForms.UWP.DataForm;

using Syncfusion.XForms.UWP.PopupLayout;

using Syncfusion.XForms.UWP.TabView;

using Syncfusion.XForms.UWP.Buttons;

using Syncfusion.XForms.UWP.Accordion;

namespace ResilITApp.UWP
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {


            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

				List<Assembly> assembliesToInclude = new List<Assembly>();

				assembliesToInclude.Add(typeof(SfAccordionRenderer).GetTypeInfo().Assembly);

				assembliesToInclude.Add(typeof(SfButtonRenderer).GetTypeInfo().Assembly);

				assembliesToInclude.Add(typeof(SfCheckBoxRenderer).GetTypeInfo().Assembly);

				assembliesToInclude.Add(typeof(SfTabViewRenderer).GetTypeInfo().Assembly);

				assembliesToInclude.Add(typeof(SfPopupLayoutRenderer).GetTypeInfo().Assembly);

				assembliesToInclude.Add(typeof(SfDataFormRenderer).GetTypeInfo().Assembly);

				assembliesToInclude.Add(typeof(SfPullToRefreshRenderer).GetTypeInfo().Assembly);

				assembliesToInclude.Add(typeof(SfRadialMenuRenderer).GetTypeInfo().Assembly);

				assembliesToInclude.Add(typeof(SfNumericUpDownRenderer).GetTypeInfo().Assembly);

				assembliesToInclude.Add(typeof(SfNumericTextBoxRenderer).GetTypeInfo().Assembly);

				assembliesToInclude.Add(typeof(SfNavigationDrawerRenderer).GetTypeInfo().Assembly);

				assembliesToInclude.Add(typeof(SfBusyIndicatorRenderer).GetTypeInfo().Assembly);

				assembliesToInclude.Add(typeof(SfCalendarRenderer).GetTypeInfo().Assembly);

				assembliesToInclude.Add(typeof(SfCircularProgressBarRenderer).GetTypeInfo().Assembly);

				assembliesToInclude.Add(typeof(SfLinearProgressRenderer).GetTypeInfo().Assembly);

				assembliesToInclude.Add(typeof(SfScheduleRenderer).GetTypeInfo().Assembly);

				assembliesToInclude.Add(typeof(SfRangeSliderRenderer).GetTypeInfo().Assembly);

				assembliesToInclude.Add(typeof(SfPdfDocumentViewRenderer).GetTypeInfo().Assembly);

<<<<<<< HEAD
				assembliesToInclude.Add(typeof(SfDataGridRenderer).GetTypeInfo().Assembly);

                Xamarin.Forms.Forms.Init(e, assembliesToInclude);

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
=======
				assembliesToInclude.Add(typeof(SfDataGridRenderer).GetTypeInfo().Assembly);

				assembliesToInclude.AddRange(Rg.Plugins.Popup.Popup.GetExtraAssemblies());

				Xamarin.Forms.Forms.Init(e, assembliesToInclude);

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
>>>>>>> 3b4abd2462fef39006991519b6b2ccf91e72f5a1
