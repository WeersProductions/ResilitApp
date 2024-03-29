using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

using Syncfusion.SfDataGrid.XForms.iOS;

using Syncfusion.SfPdfViewer.XForms.iOS; 

using Syncfusion.SfRangeSlider.XForms.iOS; 

using Syncfusion.SfSchedule.XForms.iOS;

using Syncfusion.XForms.iOS.ProgressBar; 

using Syncfusion.SfCalendar.XForms.iOS;

using Syncfusion.SfBusyIndicator.XForms.iOS;

using Syncfusion.SfNavigationDrawer.XForms.iOS;

using Syncfusion.SfNumericTextBox.XForms.iOS;

using Syncfusion.SfNumericUpDown.XForms.iOS;

using Syncfusion.SfRadialMenu.XForms.iOS;

using Syncfusion.SfPullToRefresh.XForms.iOS;

using Syncfusion.XForms.iOS.DataForm;

using Syncfusion.XForms.iOS.PopupLayout;

using Syncfusion.XForms.iOS.TabView;

using Syncfusion.XForms.iOS.Buttons;

using Syncfusion.XForms.iOS.Accordion;
using FFImageLoading.Forms.Platform;
using System.Net;
using Syncfusion.ListView.XForms.iOS;

namespace ResilITApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            Rg.Plugins.Popup.Popup.Init();

            global::Xamarin.Forms.Forms.Init();
            
			SfDataGridRenderer.Init();
			
			
			SfPdfDocumentViewRenderer.Init(); 
			
			
			SfRangeSliderRenderer.Init(); 
			
			
			SfScheduleRenderer.Init();
			
			
			SfLinearProgressBarRenderer.Init(); 
			
			
			SfCircularProgressBarRenderer.Init(); 
			
			
			SfCalendarRenderer.Init();
			
			
			SfBusyIndicatorRenderer.Init();
			
			
			SfNavigationDrawerRenderer.Init();
			
			
			SfNumericTextBoxRenderer.Init();
			
			
			SfNumericUpDownRenderer.Init();
			
			
			SfRadialMenuRenderer.Init();
			
			
			SfPullToRefreshRenderer.Init();
			
			
			SfDataFormRenderer.Init();
			
			
			SfPopupLayoutRenderer.Init();
			
			
			SfTabViewRenderer.Init();
			
			
			SfCheckBoxRenderer.Init();
			
			
			SfButtonRenderer.Init();
			
			SfAccordionRenderer.Init();

            CachedImageRenderer.Init();

            SfListViewRenderer.Init();

            ZXing.Net.Mobile.Forms.iOS.Platform.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(uiApplication, launchOptions);
        }
    }
}
