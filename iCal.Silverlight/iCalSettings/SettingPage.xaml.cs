// Copyright 2011 Miyako Komooka
// Based on Silverlight Vista Sidebar Gadget Template by Ioan Lazarciuc
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Messaging;
using System.Windows.Shapes;
using System.Windows.Browser;

using iCalControls;
using SilverlightGadgetUtilities;

namespace iCalSettings
{
    public partial class SettingPage : UserControl
    {
        public SettingPage()
        {
            InitializeComponent();
            evHelper = new SilverlightGadgetEvents("SilverlightGadgetEvents");
            evHelper.SettingsClosing += new EventHandler<SettingsClosingEventArgs>(evHelper_SettingsClosing);
            evHelper.SettingsClosed += new EventHandler<SettingsClosingEventArgs>(evHelper_SettingsClosed);

            HtmlPage.RegisterScriptableObject("settingGadget", this);

            // Set settings page size according to the one specified in the Silverlight control.
            // SilverlightGadget.SetPageSize(Width, Height);

            string gadgetPath = SilverlightGadget.Path;
            if( gadgetPath == null || gadgetPath.Length == 0 ){ // for test

                Help.NavigateUri = new Uri("/help.html",
                                           UriKind.RelativeOrAbsolute );
            } else {
                Help.NavigateUri = new Uri( gadgetPath + "\\" + 
                                            Localize.StringLibrary.HelpFile,
                                            UriKind.RelativeOrAbsolute );
            }

            messageSender = new LocalMessageSender( "iCalDockedReceiver");
            messageSender.SendCompleted += sender_SendCompleted;


            iCalSettingsCollection Settings = SilverlightGadget.Settings;

            ColorPickers = new Dictionary<string, ColorPicker>();
            ColorPickers[ Settings.KeyBackgroundColor ] = BackgroundColor;
            ColorPickers[ Settings.KeyArrowColor ] = ArrowColor;
            ColorPickers[ Settings.KeyHomeIconColor ] = HomeIconColor;
            ColorPickers[ Settings.KeyReloadIconColor ] = ReloadIconColor;
            ColorPickers[ Settings.KeyClockIconColor ] = ClockIconColor;
            ColorPickers[ Settings.KeyInfoIconColor ] = InfoIconColor;

            ColorPickers[ Settings.KeyNormalDayBackground ] = NormalBack;
            ColorPickers[ Settings.KeyNormalDayForeground ] = NormalFore;
            ColorPickers[ Settings.KeyTodayBackground ] = TodayBack;
            ColorPickers[ Settings.KeyTodayForeground ] = TodayFore;
            ColorPickers[ Settings.KeyEventDayBackground ] = EventBack;
            ColorPickers[ Settings.KeyEventDayForeground ] = EventFore;
            ColorPickers[ Settings.KeyTodoDayBackground ] = TodoBack;
            ColorPickers[ Settings.KeyTodoDayForeground ] = TodoFore;

            CheckBoxes = new Dictionary<string, CheckBox>();
            CheckBoxes[ Settings.KeyNormalDayBold ] = NormalBold;
            CheckBoxes[ Settings.KeyNormalDayUnderline ] = NormalUnderline;
            CheckBoxes[ Settings.KeyTodayBold ] = TodayBold;
            CheckBoxes[ Settings.KeyTodayUnderline ] = TodayUnderline;
            CheckBoxes[ Settings.KeyEventDayBold ] = EventBold;
            CheckBoxes[ Settings.KeyEventDayUnderline ] = EventUnderline;
            CheckBoxes[ Settings.KeyTodoDayBold ] = TodoBold;
            CheckBoxes[ Settings.KeyTodoDayUnderline ] = TodoUnderline;


            CalendarFile.Text = Settings.CalendarFile;
            UID.Text = Settings.CalendarUID;
            Password.Password = Settings.CalendarPassword;
            HolidayFile.Text = Settings.HolidayFile;
            UID2.Text = Settings.HolidayUID;
            Password2.Password = Settings.HolidayPassword;
            ReloadInterval.Text = Settings.ReloadInterval.ToString();

            NormalFore.SelectedItem = Settings.NormalDayForeground;
            NormalBack.SelectedItem = Settings.NormalDayBackground;
            NormalBold.IsChecked = Settings.NormalDayBold;
            NormalUnderline.IsChecked = Settings.NormalDayUnderline;

            TodayFore.SelectedItem = Settings.TodayForeground;
            TodayBack.SelectedItem = Settings.TodayBackground;
            TodayBold.IsChecked = Settings.TodayBold;
            TodayUnderline.IsChecked = Settings.TodayUnderline;

            EventFore.SelectedItem = Settings.EventDayForeground;
            EventBack.SelectedItem = Settings.EventDayBackground;
            EventBold.IsChecked = Settings.EventDayBold;
            EventUnderline.IsChecked = Settings.EventDayUnderline;

            TodoFore.SelectedItem = Settings.TodoDayForeground;
            TodoBack.SelectedItem = Settings.TodoDayBackground;
            TodoBold.IsChecked = Settings.TodoDayBold;
            TodoUnderline.IsChecked = Settings.TodoDayUnderline;

            BackgroundColor.SelectedItem = Settings.BackgroundColor;
            ArrowColor.SelectedItem = Settings.ArrowColor;
            HomeIconColor.SelectedItem = Settings.HomeIconColor;
            ReloadIconColor.SelectedItem = Settings.ReloadIconColor;
            ClockIconColor.SelectedItem = Settings.ClockIconColor;
            InfoIconColor.SelectedItem = Settings.InfoIconColor;

            CommandText.Text = Settings.Command;
            Argument.Text = Settings.Argument;

            MonthViewTitleFormat.Text = Settings.MonthViewTitleFormat;
            DayViewTitleFormat.Text = Settings.DayViewTitleFormat;

            SendMessageEnabled = true;
        }

        SilverlightGadgetEvents evHelper;

        void evHelper_SettingsClosed(object sender, SettingsClosingEventArgs e)
        {
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // If through code the size of the Silverlight control is changed, change the size in the javascript gadget also.
            // SilverlightGadget.SetPageSize(e.NewSize.Width, e.NewSize.Height);
        }

        void evHelper_SettingsClosing(object sender, SettingsClosingEventArgs e)
        {
            if (e.Action == CloseAction.Commit)
            {
                // Put code for saving settings here
                //SilverlightGadget.Settings["key"] = value;

                iCalSettingsCollection Settings = SilverlightGadget.Settings;

                Settings.CalendarFile = CalendarFile.Text;
                Settings.CalendarUID = UID.Text;
                Settings.CalendarPassword = Password.Password;

                Settings.HolidayFile = HolidayFile.Text;
                Settings.HolidayUID = UID2.Text;
                Settings.HolidayPassword = Password2.Password;

                try {
                    int interval = int.Parse( ReloadInterval.Text );
                    Settings.ReloadInterval = interval;
                } catch( SystemException ){
                    Settings.ReloadInterval = -1;
                }

                Settings.NormalDayForeground = (ColorItem)NormalFore.SelectedItem;
                Settings.NormalDayBackground = (ColorItem)NormalBack.SelectedItem;
                Settings.NormalDayBold = (bool)NormalBold.IsChecked;
                Settings.NormalDayUnderline = (bool)NormalUnderline.IsChecked;
                
                Settings.TodayForeground = (ColorItem)TodayFore.SelectedItem;
                Settings.TodayBackground = (ColorItem)TodayBack.SelectedItem;
                Settings.TodayBold = (bool)TodayBold.IsChecked;
                Settings.TodayUnderline = (bool)TodayUnderline.IsChecked;
                
                Settings.EventDayForeground = (ColorItem)EventFore.SelectedItem;
                Settings.EventDayBackground = (ColorItem)EventBack.SelectedItem;
                Settings.EventDayBold = (bool)EventBold.IsChecked;
                Settings.EventDayUnderline = (bool)EventUnderline.IsChecked;
                
                Settings.TodoDayForeground = (ColorItem)TodoFore.SelectedItem;
                Settings.TodoDayBackground = (ColorItem)TodoBack.SelectedItem;
                Settings.TodoDayBold = (bool)TodoBold.IsChecked;
                Settings.TodoDayUnderline = (bool)TodoUnderline.IsChecked;
                
                Settings.BackgroundColor = (ColorItem)BackgroundColor.SelectedItem;
                Settings.ArrowColor = (ColorItem)ArrowColor.SelectedItem;
                Settings.HomeIconColor = (ColorItem)HomeIconColor.SelectedItem;
                Settings.ReloadIconColor = (ColorItem)ReloadIconColor.SelectedItem;
                Settings.ClockIconColor = (ColorItem)ClockIconColor.SelectedItem;
                Settings.InfoIconColor = (ColorItem)InfoIconColor.SelectedItem;

                Settings.Command = CommandText.Text;
                Settings.Argument = Argument.Text;

                Settings.MonthViewTitleFormat = MonthViewTitleFormat.Text;
                Settings.DayViewTitleFormat = DayViewTitleFormat.Text ;
            }
        }

        [ScriptableMember]
        public void SetCalendarInfo( String eventCalendarInfo,
                                     String holidayCalendarInfo )
        {
            CalendarInfo.Text = eventCalendarInfo;
            HolidayInfo.Text = holidayCalendarInfo;

            iCalSettingsCollection Settings = SilverlightGadget.Settings;

            Settings.CalendarFile = OriginalCalendarFile;
            Settings.CalendarUID = OriginalCalendarUID;
            Settings.CalendarPassword = OriginalCalendarPassword;

            Settings.HolidayFile = OriginalHolidayFile;
            Settings.HolidayUID = OriginalHolidayUID;
            Settings.HolidayPassword = OriginalHolidayPassword;
        }

        [ScriptableMember]
        public void SetCalendarFile( String filename ){
            CalendarFile.Text = filename;
        }

        private const int MAX_ATTEMPTS = 10000;
        private int attempt = 1;
        
        private void sender_SendCompleted(object sender, SendCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // LogError(e);
                attempt++;
                if (attempt > MAX_ATTEMPTS)
                {
                    // Log.Text = "Could not send message.";
                    return;
                }
                SendMessage(e.Message);
                return;
            }

            /*
            Log.Text =
                "Message: " + e.Message + Environment.NewLine +
                "Attempt " + (int)e.UserState + 
                " completed." + Environment.NewLine +
                "Response: " + e.Response + Environment.NewLine +
                "ReceiverName: " + e.ReceiverName + Environment.NewLine + 
                "ReceiverDomain: " + e.ReceiverDomain;
            */

            // Reset attempt counter.
            attempt = 1;
        }

        private LocalMessageSender messageSender;

        private void SendMessage(string message)
        {
            messageSender.SendAsync(message, attempt);
        }

        private void SelectCalendar(object sender, RoutedEventArgs e) {
            string str = "choosefile()";
            Object ret = HtmlPage.Window.Eval( str );
            string result = ret as string;
            if( result != null && result.Length > 0 ){
                CalendarFile.Text = result;
            }
        }

        private void SelectHolidayCalendar(object sender, RoutedEventArgs e) {
            string str = "choosefile()";
            Object ret = HtmlPage.Window.Eval( str );
            string result = ret as string;
            if( result != null && result.Length > 0 ){
                HolidayFile.Text = result;
            }
        }

        private void SelectCommand(object sender, RoutedEventArgs e) {
            string str = "chooseCommand()";
            Object ret = HtmlPage.Window.Eval( str );
            string result = ret as string;
            if( result != null && result.Length > 0 ){
                CommandText.Text = result;
            }
        }

        string OriginalCalendarFile;
        string OriginalCalendarUID;
        string OriginalCalendarPassword;
        string OriginalHolidayFile;
        string OriginalHolidayUID;
        string OriginalHolidayPassword;

        private void ConnectionTest(object sender, RoutedEventArgs e) {
            iCalSettingsCollection Settings = SilverlightGadget.Settings;
            
            OriginalCalendarFile = Settings.CalendarFile;
            OriginalCalendarUID = Settings.CalendarUID;
            OriginalCalendarPassword = Settings.CalendarPassword;

            OriginalHolidayFile = Settings.HolidayFile;
            OriginalHolidayUID = Settings.HolidayUID;
            OriginalHolidayPassword = Settings.HolidayPassword;

            Settings.CalendarFile = CalendarFile.Text;
            Settings.CalendarUID = UID.Text;
            Settings.CalendarPassword = Password.Password;
            
            Settings.HolidayFile = HolidayFile.Text;
            Settings.HolidayUID = UID2.Text;
            Settings.HolidayPassword = Password2.Password;

            string str = "startLoadCalendar()";
            HtmlPage.Window.Eval( str );
        }

        private void ClearTest(object sender, RoutedEventArgs e) {
            CalendarInfo.Text = "";
            HolidayInfo.Text = "";
        }

        private void CheckboxChanged(object sender, RoutedEventArgs e) {
            ViewSettingsChanged();
        }

        private void ColorPickerChanged( object sender,
                                         SelectionChangedEventArgs e ) {
            ViewSettingsChanged();
        }
        

        Dictionary<string, ColorPicker> ColorPickers;
        Dictionary<string, CheckBox> CheckBoxes;
        bool SendMessageEnabled = false;

        private void ViewSettingsChanged()
        {
            if( !SendMessageEnabled ){
                return;
            }
            
            iCalSettingsCollection Settings = SilverlightGadget.Settings;

            string msg = "";
            string value;
            ColorItem colorItem;

            foreach( KeyValuePair<string, ColorPicker> kvp in ColorPickers ){
                string key = kvp.Key;
                ColorPicker colorPicker = kvp.Value;

                colorItem = colorPicker.SelectedItem as ColorItem;
                if( colorItem != null ){
                    value = colorItem.Name;
                } else {
                    value = "";
                }
                msg += key + "=" + value + ",";
            }

            foreach( KeyValuePair<string, CheckBox> kvp in CheckBoxes ){
                string key = kvp.Key;
                CheckBox checkBox = kvp.Value;

                if( checkBox.IsChecked == true ){
                    value = "true";
                } else {
                    value = "false";
                }
                msg += key + "=" + value + ",";
            }

            SendMessage( msg );            
        }

        private void TitleFormatPreview(object sender, RoutedEventArgs e) {
            if( !SendMessageEnabled ){
                return;
            }
            
            iCalSettingsCollection Settings = SilverlightGadget.Settings;
            string msg = "";
            if( MonthViewTitleFormat.Text.Length != 0 ){
                msg +=
                    Settings.KeyMonthViewTitleFormat + "=" +
                    MonthViewTitleFormat.Text;
            }
            if( DayViewTitleFormat.Text.Length != 0 ){
                msg +=
                    Settings.KeyDayViewTitleFormat + "=" +
                    DayViewTitleFormat.Text;
            }

            if( msg.Length != 0 ){
                SendMessage( msg );
            }
        }
    }
}
