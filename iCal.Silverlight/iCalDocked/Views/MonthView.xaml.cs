// Copyright 2011 Miyako Komooka
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Navigation;

using iCalLibrary;
using iCalLibrary.Component;
using SilverlightGadgetUtilities;
using iCalControls;

namespace iCalDocked.Views
{
    public partial class MonthView : System.Windows.Controls.Page
    {
        private HyperlinkUnderline[] Days;
        private Label[] Labels;
        private TextBlock[] LabelTexts;
        private BackgroundWorker bwCalendar = new BackgroundWorker();

        public MonthView()
        {
            InitializeComponent();
            Days = new HyperlinkUnderline[38];
            Days[1] = Day1;
            Days[2] = Day2;
            Days[3] = Day3;
            Days[4] = Day4;
            Days[5] = Day5;
            Days[6] = Day6;
            Days[7] = Day7;
            Days[8] = Day8;
            Days[9] = Day9;
            Days[10] = Day10;
            Days[11] = Day11;
            Days[12] = Day12;
            Days[13] = Day13;
            Days[14] = Day14;
            Days[15] = Day15;
            Days[16] = Day16;
            Days[17] = Day17;
            Days[18] = Day18;
            Days[19] = Day19;
            Days[20] = Day20;
            Days[21] = Day21;
            Days[22] = Day22;
            Days[23] = Day23;
            Days[24] = Day24;
            Days[25] = Day25;
            Days[26] = Day26;
            Days[27] = Day27;
            Days[28] = Day28;
            Days[29] = Day29;
            Days[30] = Day30;
            Days[31] = Day31;
            Days[32] = Day32;
            Days[33] = Day33;
            Days[34] = Day34;
            Days[35] = Day35;
            Days[36] = Day36;
            Days[37] = Day37;

            Labels = new Label[7];
            Labels[0] = Sun;
            Labels[1] = Mon;
            Labels[2] = Tue;
            Labels[3] = Wed;
            Labels[4] = Thu;
            Labels[5] = Fri;
            Labels[6] = Sat;

            LabelTexts = new TextBlock[7];
            LabelTexts[0] = SunText;
            LabelTexts[1] = MonText;
            LabelTexts[2] = TueText;
            LabelTexts[3] = WedText;
            LabelTexts[4] = ThuText;
            LabelTexts[5] = FriText;
            LabelTexts[6] = SatText;

            bwCalendar.WorkerReportsProgress = false;
            bwCalendar.WorkerSupportsCancellation = true;

            bwCalendar.DoWork += new DoWorkEventHandler( bwCalendar_DoWork );
            bwCalendar.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler( bwCalendar_Completed );
            
            Colors = SilverlightColors.Colors();

            ApplySettings();
        }

        public int Year = 0;
        public int Month = 0;
        public int Day = 0;

        public iCalDocked.Page NavigationParent = null;
        public bool NeedRefresh = false;
        public bool ShowCurrentMonth = false;
        
        Dictionary<string, ColorItem> Colors;

        private void ApplySettings()
        {
            iCalSettingsCollection Settings = SilverlightGadget.Settings;

            HomeIcon.FillColor = Settings.HomeIconColor.Color;
            ReloadIcon.FillColor = Settings.ReloadIconColor.Color;
            ClockIcon.FillColor = Settings.ClockIconColor.Color;

            Color color = Settings.InfoIconColor.Color;
            long x = ( color.R * 299 + color.G * 587 + color.B * 114 ) / 1000;
            if( x > 240 ){
                InfoIcon.StrokeColor = Colors[ "Gray" ].Color;
            } else if( x < 140 ){
                InfoIcon.StrokeColor = Colors[ "LightGray" ].Color;
            } else {
                InfoIcon.StrokeColor = Color.FromArgb( 0xFF, 0x60, 0x60, 0x60 );
            }
            InfoIcon.FillColor = color;

            NormalDayBackground = Settings.NormalDayBackground.Solid;
            NormalDayForeground = Settings.NormalDayForeground.Solid;
            NormalDayBold = Settings.NormalDayBold;
            NormalDayUnderline = Settings.NormalDayUnderline;

            TodayBackground = Settings.TodayBackground.Solid;
            TodayForeground = Settings.TodayForeground.Solid;
            TodayBold = Settings.TodayBold;
            TodayUnderline = Settings.TodayUnderline;

            EventDayBackground = Settings.EventDayBackground.Solid;
            EventDayForeground = Settings.EventDayForeground.Solid;
            EventDayBold = Settings.EventDayBold;
            EventDayUnderline = Settings.EventDayUnderline;

            TodoDayBackground = Settings.TodoDayBackground.Solid;
            TodoDayForeground = Settings.TodoDayForeground.Solid;
            TodoDayBold = Settings.TodoDayBold;
            TodoDayUnderline = Settings.TodoDayUnderline;


            for( int i = 0; i < Labels.Length; i++ ){
                Labels[i].Background = NormalDayBackground;
                Labels[i].Foreground = NormalDayForeground;
                if( NormalDayBold ){
                    Labels[i].FontWeight = FontWeights.Bold;
                } else {
                    Labels[i].FontWeight = FontWeights.Normal;
                }
                // Labels[i].Underline = NormalDayUnderline;
            }
            Sun.Foreground = Colors[ "Red" ].Solid;
            Sat.Foreground = Colors[ "Blue" ].Solid;

            for( int i = 0; i < LabelTexts.Length; i++ ){
                if( NormalDayUnderline ){
                    LabelTexts[i].TextDecorations = TextDecorations.Underline;
                } else {
                    LabelTexts[i].TextDecorations = null;
                }
            }

        }

        public void ApplyAndRefresh()
        {
            ApplySettings();
            Refresh();
        }

        // ユーザーがこのページに移動したときに実行されます。
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            int year, month;

            if( NavigationParent != null &&
                NavigationParent.SettingMode == true ){
                Clock.Visibility = Visibility.Visible;
                Info.Visibility = Visibility.Visible;
            }

            if (NavigationContext.QueryString.ContainsKey("year")) {
                year = int.Parse(NavigationContext.QueryString["year"]);
            } else {
                year = DateTime.Now.Year;
            }

            if (NavigationContext.QueryString.ContainsKey("month")) {
                month = int.Parse(NavigationContext.QueryString["month"]);
            } else {
                month = DateTime.Now.Month;
            }

            if( month != Month || year != Year || NeedRefresh ){
                Month = month;
                Year = year;
                NeedRefresh = false;

                if( Year == DateTime.Now.Year &&
                    Month == DateTime.Now.Month ){
                    ShowCurrentMonth = true;
                } else {
                    ShowCurrentMonth = false;
                }
                
                Refresh();
            }

            if( NavigationParent != null ){
                NavigationParent.MonthViewShowedCallback( Year, Month );
            }

            if( ShowCurrentMonth ){
                DayMoveTimerCallback( this );
            }
        }

        Brush NormalDayBackground = null;
        Brush NormalDayForeground = null;
        bool  NormalDayBold = false;
        bool  NormalDayUnderline = false;

        Brush TodayBackground = null;
        Brush TodayForeground = null;
        bool  TodayBold = false;
        bool  TodayUnderline = false;

        Brush EventDayBackground = null;
        Brush EventDayForeground = null;
        bool  EventDayBold = false;
        bool  EventDayUnderline = false;

        Brush TodoDayBackground = null;
        Brush TodoDayForeground = null;
        bool  TodoDayBold = false;
        bool  TodoDayUnderline = false;

        protected void Refresh()
        {
            DateTime firstDay = new DateTime( Year, Month, 1);

            GregorianCalendar gc = new GregorianCalendar();
            DayOfWeek dowStart = gc.GetDayOfWeek(firstDay);

            int indexStart = 1;
            switch (dowStart) {
            case DayOfWeek.Monday:
                indexStart = 2;
                break;
            case DayOfWeek.Tuesday:
                indexStart = 3;
                break;
            case DayOfWeek.Wednesday:
                indexStart = 4;
                break;
            case DayOfWeek.Thursday:
                indexStart = 5;
                break;
            case DayOfWeek.Friday:
                indexStart = 6;
                break;
            case DayOfWeek.Saturday:
                indexStart = 7;
                break;
            default:
                break;
            }

            for( int i = 1; i < indexStart; i++ ){
                Days[i].Content="";
                Days[i].NavigateUri = null;
                Days[i].Background = null;
            }

            int daysInMonth = gc.GetDaysInMonth( Year, Month );
            for( int i = 0; i < daysInMonth; i++ ){
                Days[indexStart + i].Content = (i + 1).ToString();
                Days[indexStart + i].FontWeight = FontWeights.Thin;
                Days[indexStart + i].NavigateUri =
                    new Uri( "/Day/" + Year.ToString() + "/"
                             + Month.ToString() + "/"
                             + (i+1).ToString(),
                             UriKind.Relative  );
                Days[indexStart + i].Background = NormalDayBackground;
                if( NormalDayBold ){
                    Days[indexStart + i].FontWeight = FontWeights.Bold;
                } else {
                    Days[indexStart + i].FontWeight = FontWeights.Normal;
                }
                Days[indexStart + i].Underline = NormalDayUnderline;

                DateTime day = new DateTime( Year, Month, i + 1 );
                
                if( day.DayOfWeek == DayOfWeek.Saturday ){
                    Days[indexStart + i].Foreground = Colors[ "Blue" ].Solid;
                } else if( day.DayOfWeek == DayOfWeek.Sunday ){
                    Days[indexStart + i].Foreground = Colors[ "Red" ].Solid;
                } else {
                    Days[indexStart + i].Foreground = NormalDayForeground;
                }

                if( Year == DateTime.Now.Year &&
                    Month == DateTime.Now.Month &&
                    ( i + 1 ) == DateTime.Now.Day ){

                    Day = (i + 1 );

                    if( TodayBackground != null ){
                        Days[indexStart + i].Background = TodayBackground;
                    }
                    if( TodayForeground != null ){
                        Days[indexStart + i].Foreground = TodayForeground;
                    }
                    if( TodayBold ){
                        Days[indexStart + i].FontWeight = FontWeights.Bold;
                    } else {
                        Days[indexStart + i].FontWeight = FontWeights.Normal;
                    }
                    Days[indexStart + i].Underline = TodayUnderline;
                }
            }

            for( int i = indexStart + daysInMonth; i < 38; i++ ){
                Days[i].Content = "";
                Days[i].NavigateUri = null;
                Days[i].Background = null;
            }

            if( !bwCalendar.IsBusy && NavigationParent != null &&
                (NavigationParent.iColl != null || NavigationParent.iCollHoliday != null )){
                Clock.Visibility = Visibility.Visible;
                bwCalendar.RunWorkerAsync(this);
            }

        }

        private Timer dayMoveTimer = null;

        private static void DayMoveTimerCallback( Object state )
        {
            MonthView thisPage = state as MonthView;
            if( thisPage != null ){
                if( thisPage.dayMoveTimer != null ){
                    thisPage.dayMoveTimer.Dispose();
                }
                
                thisPage.Dispatcher.BeginInvoke( thisPage.DayMove );

                if( thisPage.ShowCurrentMonth ){
                    DateTime nextDay = DateTime.Now.AddDays( 1 );
                    nextDay = new DateTime( nextDay.Year,
                                            nextDay.Month,
                                            nextDay.Day,
                                            0, 0, 0 );
                    TimeSpan span = nextDay - DateTime.Now;                

                    thisPage.dayMoveTimer =
                        new Timer( DayMoveTimerCallback, thisPage,
                                   (int)(span.TotalMilliseconds) + 5000,
                                   Timeout.Infinite );
                } else {
                    thisPage.dayMoveTimer = null;
                }
            }
        }

        private void DayMove()
        {
            if( ShowCurrentMonth ){
                int year = DateTime.Now.Year;
                int month = DateTime.Now.Month;
                int day = DateTime.Now.Day;

                if( year != Year || month != Month || day != Day ){
                    Year = year;
                    Month = month;
                    Refresh();
                }
            }
        }

        protected override void OnNavigatingFrom( NavigatingCancelEventArgs e ){
            if( dayMoveTimer != null ){
                dayMoveTimer.Dispose();
                dayMoveTimer = null;
            }

            if( bwCalendar.IsBusy ){
                bwCalendar.CancelAsync();
            }
        }

        private void Reload_Calendar(object sender, RoutedEventArgs e)
        {
            // string str = "var xhr = new XMLHttpRequest(); xhr.open( \"GET\", \"JapanHoliday.ics\", false); xhr.send( null );xhr.responseText;";
            string str = "startLoadCalendar()";
            HtmlPage.Window.Eval( str );

            /*
            Object ret = HtmlPage.Window.Eval( str );

            string ical = ret as string;
            if( ical != null && NavigationParent != null ){
                NavigationParent.SetHolidayCalendar( ical );
            }
            */
        }

        private void bwCalendar_DoWork(object sender, DoWorkEventArgs e) {

            BackgroundWorker worker = sender as BackgroundWorker;
            MonthView thisPage = e.Argument as MonthView;

            if( thisPage != null ){
                int year = thisPage.Year;
                int month = thisPage.Month;
                iCalendarCollection calendar = null;
                if( thisPage.NavigationParent != null ){
                    calendar = thisPage.NavigationParent.iColl;
                }
                iCalendarCollection holiday = null;
                if( thisPage.NavigationParent != null ){
                    holiday = thisPage.NavigationParent.iCollHoliday;
                }

                GregorianCalendar gc = new GregorianCalendar();
                int daysInMonth = gc.GetDaysInMonth( year, month );

                DayStatus[] result = new DayStatus[ daysInMonth + 1 ];
                for( int i = 1; i <= daysInMonth; i++ ){
                    if ((worker.CancellationPending == true)){
                        e.Cancel = true;
                        break;
                    }
                    
                    DayStatus status = new DayStatus();

                    if( calendar != null ){
                        List<iCalEvent> ret =
                            calendar.GetEventByDay( year, month, i );

                        if( ret.Count > 0 ){
                            status.isEventDay = true;
                        }


                        List<iCalToDo> ret2 = 
                            calendar.GetToDoByDay( Year, Month, i, false );
                        if( ret2.Count > 0 ){
                            status.isToDoDay = true;
                        }
                    }

                    if( holiday != null ){
                        List<iCalEvent> ret =
                            holiday.GetEventByDay( year, month, i );

                        if( ret.Count > 0 ){
                            status.isHoliday = true;
                        }
                    }

                    // result[i] = status;
                    result[i] = status;
                }

                if( !e.Cancel ){
                    e.Result = result;
                }
            }
        }
        
        private void bwCalendar_Completed(object sender,
                                           RunWorkerCompletedEventArgs e)
        {
            if( e.Cancelled ){
                NeedRefresh = true;
            } else if( !e.Cancelled && e.Error == null ){
                DayStatus[] result = e.Result as DayStatus[];
                if( result != null ){
                    int indexStart = -1;
                    for( int i = 1; i < Days.Length; i++ ){
                        string content = Days[i].Content as string;
                        if( content != null && content == "1" ){
                            indexStart = i;
                            break;
                        }
                    }

                    // DateTime now = DateTime.Now;
                    if( indexStart >= 0 ){
                        for( int i = 1; i < result.Length; i++ ){
                            DayStatus status = result[i];
                            int index = indexStart + i -1;
                            
                            if( status.isHoliday ){
                                Days[index].Foreground = Colors["Red"].Solid;
                            }
                            if( status.isToDoDay ){
                                if( TodoDayBackground != null ){
                                    Days[index].Background = TodoDayBackground;
                                }
                                if( TodoDayForeground != null ){
                                    Days[index].Foreground = TodoDayForeground;
                                }
                                if( TodoDayBold ){
                                    Days[index].FontWeight = FontWeights.Bold;
                                } else {
                                    Days[index].FontWeight = FontWeights.Normal;
                                } 
                                Days[index].Underline = TodoDayUnderline;
                            }
                            if( status.isEventDay ){
                                if( EventDayBackground != null ){
                                    Days[index].Background = EventDayBackground;
                                }
                                if( EventDayForeground != null ){
                                    Days[index].Foreground = EventDayForeground;
                                }
                                if( EventDayBold ){
                                    Days[index].FontWeight = FontWeights.Bold;
                                } /* else {
                                    Days[index].FontWeight = FontWeights.Normal;
                                    } */
                                
                                if( EventDayUnderline ){
                                    Days[index].Underline = EventDayUnderline;
                                }
                            }
                            /**/
                            if( Year == DateTime.Now.Year && 
                                Month == DateTime.Now.Month &&
                                i == DateTime.Now.Day ){
                                if( TodayBackground != null ){
                                    Days[index].Background = TodayBackground;
                                }
                                if( TodayForeground != null ){
                                    Days[index].Foreground = TodayForeground;
                                }
                                if( TodayBold ){
                                    Days[index].FontWeight = FontWeights.Bold;
                                }
                                if( TodayUnderline ){
                                    Days[index].Underline = TodayUnderline;
                                }
                            }
                        }
                    }
                }
            }

            if( NavigationParent == null ||
                NavigationParent.SettingMode == false ){
                Clock.Visibility = Visibility.Collapsed;
            }
        }
    }

    public class DayStatus
    {
        public bool isHoliday = false;
        public bool isEventDay = false;
        public bool isToDoDay = false;
    }
}
