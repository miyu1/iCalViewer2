// Copyright 2011 Miyako Komooka
// Based on Silverlight Vista Sidebar Gadget Template by Ioan Lazarciuc
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;
using System.Threading;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Messaging;
using System.Windows.Shapes;
using System.Windows.Browser;
using SilverlightGadgetUtilities;

using iCalLibrary;

namespace iCalDocked
{
    public partial class Page : UserControl
    {
        public Page()
        {
            InitializeComponent();
            evHelper = new SilverlightGadgetEvents("SilverlightGadgetEventsD");
            evHelper.VisibilityChanged += new EventHandler(evHelper_VisibilityChanged);
            evHelper.SettingsClosed += new EventHandler<SettingsClosingEventArgs>(evHelper_SettingsClosed);
            evHelper.SettingsClosing += new EventHandler<SettingsClosingEventArgs>(evHelper_SettingsClosing);
            evHelper.ShowSettings += new EventHandler(evHelper_ShowSettings);
            evHelper.Dock += new EventHandler(evHelper_Dock);
            evHelper.Undock += new EventHandler(evHelper_Undock);

            HtmlPage.RegisterScriptableObject("dockedGadget", this);

            // show this month as current frame;
            CurrentDate = DateTime.Now;
            DecadeStartYear = CurrentDate.Year / 10 * 10;

            // initial value settings when gadget is attached
            iCalSettingsCollection Settings = SilverlightGadget.Settings;
            if( !Settings.DefaultSet ){
                Settings.MonthViewTitleFormat =
                    Localize.StringLibrary.MonthViewTitleFormat;
                Settings.DayViewTitleFormat =
                    Localize.StringLibrary.DayViewTitleFormat;

                Settings.YearViewMonthFormat = 
                    Localize.StringLibrary.YearViewMonthFormat;

                if( CultureInfo.CurrentCulture.EnglishName.StartsWith( "Japan" ) ){
                    Settings.HolidayFile =
                        SilverlightGadget.Path + "\\JapanHoliday.ics";
                }

                Settings.DefaultSet = true;
            }
            
            ApplySettings();
            ReloadTimerCallback( this );

            if( ! SilverlightGadget.GadgetEnvironment ){
                evHelper_Undock( null, null ); // for test
            }

        }

        SilverlightGadgetEvents evHelper;
        int DecadeStartYear;
        DateTime CurrentDate;

        Dictionary<string, ColorItem> ColorDict = SilverlightColors.Colors();

        /*
        [ScriptableMember]
        public bool IsVisible
        {
            get { return this.Visibility == System.Windows.Visibility.Visible; }
            set { this.Visibility = value ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed; }
        }
        */

        public iCalendarCollection iColl;
        public iCalendarCollection iCollHoliday;

        [ScriptableMember]
        public void BeforeLoadCalendar()
        {
            if( MonthView != null ){
                MonthView.Info.Visibility = Visibility.Visible;
            }
        }

        [ScriptableMember]
        public void SetCalendars( String eventCalendar, String holidayCalendar ) {

            SetEventCalendar( eventCalendar );
            // Log.Text = eventCalendar;
            SetHolidayCalendar( holidayCalendar );


            if( MonthView != null ){
                MonthView.NeedRefresh = true;
            }

            try {
               View.Refresh();
               View2.Refresh();
            } catch( ArgumentNullException ){}

            if( MonthView != null && SettingMode == false ){
                MonthView.Info.Visibility = Visibility.Collapsed;
            }
        }
            
        public void SetEventCalendar(String str)
        {
            // Log.Text = str;

            if( str != null && str.Length > 0 ){
                StringReader strReader = new StringReader( str );
                iCalParser parser = new iCalParser();
                iColl = parser.ParseStream( strReader );
            } else {
                iColl = null;
            }
        }

        public void SetHolidayCalendar(String str)
        {
            // Log.Text = str;

            if( str != null && str.Length > 0 ){
                StringReader strReader = new StringReader( str );
                iCalParser parser = new iCalParser();
                iCollHoliday = parser.ParseStream( strReader );
            } else {
                iCollHoliday = null;
            }
        }

        [ScriptableMember]
        public void test1()
        {
            if( MonthView != null ){
                MonthView.Info.Visibility = Visibility.Visible;
            }
        }

        [ScriptableMember]
        public bool SettingMode {
            get { return settingMode; }
            set {
                settingMode = value;
                if( settingMode ){
                    StartListening();
                    iCalTempSettingsCollection TmpSettings =
                        new iCalTempSettingsCollection();
                    SilverlightGadget.Settings = TmpSettings;
                }
            }
        }
        bool settingMode = false;


        [ScriptableMember]
        public string SettingNormalDayBack {
            get{ return settingNormalDayBack; }
            set{
                settingNormalDayBack = value;
                Refresh();
            }
        }
        string settingNormalDayBack;
        

        void evHelper_Dock(object sender, EventArgs e)
        {
            // SilverlightGadget.SetGadgetSize(Width, Height);
            Root.Height = 126;            
            Root2.Visibility = System.Windows.Visibility.Collapsed;
        }

        void evHelper_Undock(object sender, EventArgs e)
        {
            // SilverlightGadget.SetGadgetSize(Width, Height);
            // Text1.Visibility = System.Windows.Visibility.Visible;
            Root.Height = 256;
            Root2.Visibility = System.Windows.Visibility.Visible;
        }

        public void evHelper_ShowSettings(object sender, EventArgs e)
        {
        }

        void evHelper_SettingsClosing(object sender, SettingsClosingEventArgs e)
        {
        }

        void evHelper_SettingsClosed(object sender, SettingsClosingEventArgs e)
        {
            if (e.Action == CloseAction.Commit){
                ApplySettings();
                ReloadTimerCallback( this );
                // Reload();
            }
        }

        void Reload() {
            string str = "startLoadCalendar()";
            HtmlPage.Window.Eval( str );
        }

        void evHelper_VisibilityChanged(object sender, EventArgs e)
        {
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // If through code the size of the Silverlight control is changed, change the size in the javascript gadget also.
            if (SilverlightGadget.Docked)
                SilverlightGadget.SetGadgetSize(e.NewSize.Width, e.NewSize.Height);
        }

        private void ApplySettings()
        {
            iCalSettingsCollection Settings = SilverlightGadget.Settings;

            Root1.Background = Settings.BackgroundColor.Solid;
            Root2.Background = Settings.BackgroundColor.Solid;
            LeftArrow.FillColor = Settings.ArrowColor.Color;
            RightArrow.FillColor = Settings.ArrowColor.Color;
            Title.Background = Settings.NormalDayBackground.Solid;

            if( Title_Content.Foreground != HolidayTitleColor &&
                Title_Content.Foreground != SaturdayTitleColor )
            {
                Title_Content.Foreground = Settings.NormalDayForeground.Solid;
            }
            NormalTitleColor = Settings.NormalDayForeground.Solid;

            if( Settings.NormalDayBold ){
                Title_Content.FontWeight = FontWeights.Bold;
            } else {
                Title_Content.FontWeight = FontWeights.Normal;
            }

            if( Settings.NormalDayUnderline ){
                Title_Content.TextDecorations = TextDecorations.Underline;
            } else {
                Title_Content.TextDecorations = null;
            }

            reloadInterval =
                SilverlightGadget.Settings.ReloadInterval * 60 * 1000;

            if( MonthView != null ){
                MonthView.ApplyAndRefresh();
            }
            if( DayView != null ){
                DayView.ApplyAndRefresh();
            }
            if( YearView != null ){
                YearView.ApplyAndRefresh();
            }
            if( DecadeView != null ){
                DecadeView.ApplyAndRefresh();
            }
            if( TodoView != null ){
                TodoView.ApplyAndRefresh();
            }

            MonthViewTitleFormat =
                SilverlightGadget.Settings.MonthViewTitleFormat;
            if( MonthViewTitleFormat == null ||  MonthViewTitleFormat.Length == 0 ){
                MonthViewTitleFormat =
                    Localize.StringLibrary.MonthViewTitleFormat;
            }
            DayViewTitleFormat =
                SilverlightGadget.Settings.DayViewTitleFormat;
            if( DayViewTitleFormat == null ||  DayViewTitleFormat.Length == 0 ){
                DayViewTitleFormat =
                    Localize.StringLibrary.DayViewTitleFormat;
            }
        }

        public void Refresh()
        {
            if( View.Source.ToString().Contains("Month") ){
                ShowMonth();
            } else if( View.Source.ToString().Contains("Day") ){
                ShowDay();
            } else if( View.Source.ToString().Contains("Year") ){
                ShowYear();
            }  else if( View.Source.ToString().Contains("Decade") ){
                ShowDecade();
            }
        }

        private void Forward(object sender, RoutedEventArgs e)
        {
            if( View.Source.ToString().Contains("Month") ){
                CurrentDate = CurrentDate.AddMonths( 1 );
                ShowMonth();
            } else if( View.Source.ToString().Contains("Day") ){
                CurrentDate = CurrentDate.AddDays( 1 );
                ShowDay();
                
            } else if( View.Source.ToString().Contains("Year") ){
                CurrentDate = CurrentDate.AddYears( 1 );
                ShowYear();
                
            }  else if( View.Source.ToString().Contains("Decade") ){
                DecadeStartYear += 10;
                ShowDecade();
            }
        }

        private void Backward(object sender, RoutedEventArgs e)
        {
            if( View.Source.ToString().Contains("Month") ){
                CurrentDate = CurrentDate.AddMonths( -1 );
                ShowMonth();
            } else if( View.Source.ToString().Contains("Day") ){
                CurrentDate = CurrentDate.AddDays( -1 );
                ShowDay();
            } else if( View.Source.ToString().Contains("Year") ){
                CurrentDate = CurrentDate.AddYears( -1 );
                ShowYear();
            }  else if( View.Source.ToString().Contains("Decade") ){
                DecadeStartYear -= 10;
                ShowDecade();
            }
        }

        private void Title_Click(object sender, RoutedEventArgs e)
        {
            if( View.Source.ToString().Contains("Month") ){
                // month -> year
                ShowYear();

            } else if( View.Source.ToString().Contains("Day") ){
                // day -> month
                ShowMonth();
                
            } else if( View.Source.ToString().Contains("Year") ){
                // year -> dacade
                DecadeStartYear = CurrentDate.Year / 10 * 10;
                ShowDecade();
                
            } 
        }

        private void ShowDecade()
        {
            View.Source = new Uri( "/Decade/" + DecadeStartYear.ToString(),
                                   UriKind.Relative );
        }

        private void ShowYear()
        {
            View.Source = new Uri( "/Year/" + CurrentDate.Year.ToString(),
                                   UriKind.Relative );
        }

        private void ShowMonth()
        {
            string uri = "/Month/" + CurrentDate.Year.ToString()
                + "/" + CurrentDate.Month.ToString();

            if( SettingMode ){
                uri += "?NormalBack=" + SettingNormalDayBack;
            }
            View.Source = new Uri( uri, UriKind.Relative );
        }

        private void ShowDay()
        {
            View.Source = new Uri( "/Day/" + CurrentDate.Year.ToString()
                                   + "/" + CurrentDate.Month.ToString()
                                   + "/" + CurrentDate.Day.ToString(),
                                   UriKind.Relative );
        }


        private Views.MonthView  MonthView = null;
        private Views.DayView    DayView = null;
        private Views.YearView   YearView = null;
        private Views.DecadeView DecadeView = null;

        private void View_Navigated( object sender, NavigationEventArgs e){
            Views.DecadeView dacadeView = e.Content as Views.DecadeView;
            if( dacadeView != null ){
                dacadeView.NavigationParent = this;
                DecadeView = dacadeView;
                // MonthView = null;
                DayView = null;
                YearView = null;
            }

            Views.YearView yearView = e.Content as Views.YearView;
            if( yearView != null ){
                yearView.NavigationParent = this;
                YearView = yearView;
                // MonthView = null;
                DayView = null;
                DecadeView = null;
            }

            Views.MonthView monthView = e.Content as Views.MonthView;
            if( monthView != null ){
                monthView.NavigationParent = this;
                this.MonthView = monthView;
                DayView = null;
                YearView = null;
                DecadeView = null;
            }

            Views.DayView dayView = e.Content as Views.DayView;
            if( dayView != null ){
                dayView.NavigationParent = this;
                DayView = dayView;
                // MonthView = null;
                YearView = null;
                DecadeView = null;
            }
        }

        private Views.TodoView TodoView = null;

        private void View2_Navigated( object sender, NavigationEventArgs e){
            Views.TodoView todoView = e.Content as Views.TodoView;
            if( todoView != null ){
                todoView.NavigationParent = this;
                TodoView = todoView;
            }
        }

        private Brush NormalTitleColor = new SolidColorBrush( Colors.Black );
        private Brush HolidayTitleColor = new SolidColorBrush( Colors.Red );
        private Brush SaturdayTitleColor = new SolidColorBrush( Colors.Blue );

        private string MonthViewTitleFormat = "";
        private string DayViewTitleFormat = "";

        public void DecadeViewShowedCallback( int decadeStartYear )
        {
            DecadeStartYear = decadeStartYear;
            Title_Content.Text = DecadeStartYear + "-" + (DecadeStartYear+10);
            Title_Content.Foreground = NormalTitleColor;
        }

        public void YearViewShowedCallback( int year )
        {
            GregorianCalendar gc = new GregorianCalendar();
            int daysInMonth = gc.GetDaysInMonth( year,
                                                 CurrentDate.Month );

            if( CurrentDate.Day > daysInMonth ){
                CurrentDate = new DateTime( year,
                                            CurrentDate.Month,
                                            daysInMonth );
            } else {
                CurrentDate = new DateTime( year,
                                            CurrentDate.Month,
                                            CurrentDate.Day );
            }

            Title_Content.Text = year.ToString();
            Title_Content.Foreground = NormalTitleColor;
        }

        public void MonthViewShowedCallback( int year, int month )
        {
            GregorianCalendar gc = new GregorianCalendar();
            int daysInMonth = gc.GetDaysInMonth( year,
                                                 month );

            if( CurrentDate.Day > daysInMonth ){
                CurrentDate = new DateTime( year,
                                            month,
                                            daysInMonth );
            } else {
                CurrentDate = new DateTime( year,
                                            month,
                                            CurrentDate.Day );
            }

            //Title_Content.Text = year.ToString() + "." + month.ToString();
            try {
                Title_Content.Text = CurrentDate.ToString( MonthViewTitleFormat );
            } catch ( Exception ){
                Title_Content.Text = year.ToString() + "." + month.ToString();
            }
            Title_Content.Foreground = NormalTitleColor;
        }

        public void DayViewShowedCallback( int year, int month, int day,
                                           bool holiday )
        {
            CurrentDate = new DateTime( year, month, day );

            // Title_Content.Text = year.ToString() + "." + month.ToString()
            //     + "." + day.ToString() + " " + CurrentDate.ToString("ddd");

            Title_Content.Text = CurrentDate.ToString( DayViewTitleFormat);
            if( holiday ){
                Title_Content.Foreground = HolidayTitleColor;
            } else if( CurrentDate.DayOfWeek == DayOfWeek.Sunday ){
                Title_Content.Foreground = HolidayTitleColor;
            } else if( CurrentDate.DayOfWeek == DayOfWeek.Saturday ){
                Title_Content.Foreground = SaturdayTitleColor;
            } else {
                Title_Content.Foreground = NormalTitleColor;
            }
        }

        private void StartListening(){
            LocalMessageReceiver messageReceiver =
                new LocalMessageReceiver("iCalDockedReceiver" );
                // ReceiverNameScope.Global, LocalMessageReceiver.AnyDomain);
            messageReceiver.MessageReceived += messageReceiver_MessageReceived;
            try
            {
                messageReceiver.Listen();
            }
            catch (ListenFailedException)
            {
                // Log.Text = "Cannot receive messages." + Environment.NewLine +
                //     "There is already a receiver with the name 'receiver'.";
            }
        }

        private void messageReceiver_MessageReceived(
            object sender, MessageReceivedEventArgs e)
        {
            e.Response = "response to " + e.Message;
            /*
            Log.Text =
                "Message: " + e.Message + Environment.NewLine +
                "NameScope: " + e.NameScope + Environment.NewLine +
                "ReceiverName: " + e.ReceiverName + Environment.NewLine +
                "SenderDomain: " + e.SenderDomain + Environment.NewLine +
                "Response: " + e.Response;
            */

            iCalSettingsCollection Settings = SilverlightGadget.Settings;

            string[] split = e.Message.Split( new Char[] { ',' } );
            foreach( string s in split ){
                string key, value;
                int index = s.IndexOf( "=" );
                if( index >= 0 ){
                    key = s.Substring( 0, index );
                    value = s.Substring( index + 1 );
                } else {
                    key = s;
                    value = "";
                }

                Settings[ key ] = value;
            }

            ApplySettings();

        }

        private int reloadInterval; // in mili-seconds
        private Timer reloadTimer = null;
        private static void ReloadTimerCallback( Object state )
        {
            Page thisPage = state as Page;
            if( thisPage != null ){
                if( thisPage.reloadTimer != null ){
                    thisPage.reloadTimer.Dispose();
                }

                thisPage.Dispatcher.BeginInvoke( thisPage.Reload );

                if( thisPage.reloadInterval > 0 ){
                    thisPage.reloadTimer =
                        new Timer( ReloadTimerCallback, thisPage,
                                   thisPage.reloadInterval, 
                                   Timeout.Infinite );
                }
            }
        }


        /*--*/
        /*
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // One can save parameters that need to be passed to the flyout control here
            // The parameters can be save in the gadget settings
            //SilverlightGadget.Settings["temppar"] = value;

            // Open the flyout page
            // SilverlightGadget.Flyout();

            if( MonthView != null && SettingMode == false ){
                MonthView.Info.Visibility = Visibility.Collapsed;
            }        
        }
        
        private void test2(object sender, RoutedEventArgs e)
        {
            SilverlightGadget.Flyout(false);
        }

        */
    }
}
