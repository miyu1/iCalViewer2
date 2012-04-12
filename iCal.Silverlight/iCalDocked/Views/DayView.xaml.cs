// Copyright 2011 Miyako Komooka
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;

using SilverlightGadgetUtilities;
using iCalControls;
using iCalLibrary.Component;
using iCalLibrary.DataType;

namespace iCalDocked.Views
{
    public partial class DayView : System.Windows.Controls.Page
    {
        // static public ObservableCollection<Topic> Topics =
        //     new ObservableCollection<Topic>();

        public ObservableCollection<TVEvent> TVEvents =
            new ObservableCollection<TVEvent>();
        

        public DayView()
        {
            InitializeComponent();

            int fontsize = (int)myTreeView.FontSize;
            TVItem.StaticLineHeight = fontsize + 1;

            ApplySettings();
        }

        public void ApplySettings()
        {
            string background =
                SilverlightGadget.Settings.NormalDayBackground.Name;
            string foreground = 
                SilverlightGadget.Settings.NormalDayForeground.Name;

            TVEvent.DefaultForeground = foreground;
            TVEvent.DefaultBackground = background;
            TVItem.DefaultForeground = foreground;
            TVItem.DefaultBackground = background;

            /*
            foreach( TVEvent tvevent in TVEvents ){
                if( tvevent.TitleOnly == false ){
                    tvevent.ForegroundColor = foreground;
                }
                tvevent.BackgroundColor = background;
            }
            */
        }

        public void ApplyAndRefresh()
        {
            ApplySettings();
            Refresh();
        }

        public bool Holiday = false;
        public int Year = 0;
        public int Month = 0;
        public int Day = 0;
        public iCalDocked.Page NavigationParent = null;

        // ユーザーがこのページに移動したときに実行されます。
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.ContainsKey("year")) {
                Year = int.Parse(NavigationContext.QueryString["year"]);
            } else {
                Year = DateTime.Now.Year;
            }

            if (NavigationContext.QueryString.ContainsKey("month")) {
                Month = int.Parse(NavigationContext.QueryString["month"]);
            } else {
                Month = DateTime.Now.Month;
            }

            if (NavigationContext.QueryString.ContainsKey("day")) {
                Day = int.Parse(NavigationContext.QueryString["day"]);
            } else {
                Day = DateTime.Now.Day;
            }

            Refresh();
        }

        private void Refresh()
        {
            if( NavigationParent != null ){
                TVEvents = new ObservableCollection<TVEvent>();
                Holiday = false;
                if( NavigationParent.iCollHoliday != null ){
                    List<iCalEvent> ret =
                        NavigationParent.iCollHoliday.GetEventByDay( Year,
                                                                     Month,
                                                                     Day );
                
                    foreach( iCalEvent ev in ret ){
                        Holiday = true;
                        TVEvent tvevent = new TVEvent( ev, this, true );
                        TVEvents.Add( tvevent );
                    }
                }

                if( NavigationParent.iColl != null ){
                    List<iCalEvent> ret =
                        NavigationParent.iColl.GetEventByDay( Year, Month, Day );
                
                    foreach( iCalEvent ev in ret ){
                        TVEvent tvevent = new TVEvent( ev, this, false);
                        TVEvents.Add( tvevent );
                    }

                    List<iCalToDo> ret2 =
                        NavigationParent.iColl.GetToDoByDay( Year, Month, Day,
                                                             false );
                
                    foreach( iCalToDo ev in ret2 ){
                        TVEvent tvevent = new TVEvent( ev, this, false);
                        TVEvents.Add( tvevent );
                    }
                }

                myTreeView.DataContext = TVEvents;
            } else {
                myTreeView.DataContext = null;
            }

            if( NavigationParent != null ){
                NavigationParent.DayViewShowedCallback( Year, Month, Day, Holiday );
            }
        }

        /*
        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {

            TVEvent tvevent = myTreeView.SelectedItem as TVEvent;
            if( tvevent != null ){
                iCalEntry entry = null;
                if( tvevent.Event != null ){
                    entry = tvevent.Event;
                } else if( tvevent.ToDo != null ){
                    entry = tvevent.ToDo;
                } 

                if( entry == null ){
                    return;
                }

                string year="", month="", day="", uid="";

                if( entry.DateTimeStart != null ){

                    MiySoft.iCal.DataType.iCalTimeRelatedType timeRelated =
                        entry.DateTimeStart.DateTime;

                    if( timeRelated.Year > 0 ){
                        year = timeRelated.Year.ToString();
                    }
                    
                    if( timeRelated.Month > 0 ){
                        month = timeRelated.Month.ToString();
                    }
                    
                    if( timeRelated.Day > 0 ){
                        day = timeRelated.Day.ToString();
                    }
                }
                    
                if( entry.UID != null && entry.UID.UID != null ){
                    uid = entry.UID.UID.Text;
                    if( uid == null ){
                        uid = "";
                    }
                }
                
                // string command = SilverlightGadget.Settings.Command;
                string command = "http://www.google.co.jp/";
                if( command == null || command.Length == 0 ){
                    return;
                }
                command = command.Replace( "$Y", "{0}" );
                command = command.Replace( "$M", "{1}" );
                command = command.Replace( "$D", "{2}" );
                command = command.Replace( "$U", "{3}" );
                command = command.Replace( "$$", "{4}" );
                command = String.Format( command, year, month, day, uid, "$" );

                string argument = SilverlightGadget.Settings.Argument;
                argument = argument.Replace( "$Y", "{0}" );
                argument = argument.Replace( "$M", "{1}" );
                argument = argument.Replace( "$D", "{2}" );
                argument = argument.Replace( "$U", "{3}" );
                argument = argument.Replace( "$$", "{4}" );
                argument = String.Format( argument, year, month, day, uid, "$" );

                string jscommand = "ExecCommand( \"" +
                    command + "\", \"" + argument + "\" );";

                // HtmlPage.Window.Eval( "ExecCommand( \"http://www.google.co.jp/\", \"\" )" );
                HtmlPage.Window.Eval( jscommand );
            }
        }
        */

        private void myTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            // TVEvent tvevent = myTreeView.SelectedItem as TVEvent;
            TVEvent tvevent = e.NewValue as TVEvent;
            if( tvevent != null ){
                iCalEntry entry = null;
                if( tvevent.Event != null ){
                    entry = tvevent.Event;
                } else if( tvevent.ToDo != null ){
                    entry = tvevent.ToDo;
                } 

                if( entry == null ){
                    return;
                }

                string year="", month="", day="", uid="";

                if( entry.DateTimeStart != null ){

                    iCalLibrary.DataType.iCalTimeRelatedType timeRelated =
                        entry.DateTimeStart.Value;

                    if( timeRelated.Year > 0 ){
                        year = timeRelated.Year.ToString();
                    }
                    
                    if( timeRelated.Month > 0 ){
                        month = timeRelated.Month.ToString();
                    }
                    
                    if( timeRelated.Day > 0 ){
                        day = timeRelated.Day.ToString();
                    }
                }
                    
                if( entry.UID != null && entry.UID.Value != null ){
                    uid = entry.UID.Value.Text;
                    if( uid == null ){
                        uid = "";
                    }
                }
                
                string command = SilverlightGadget.Settings.Command;
                // string command = "http://www.google.co.jp/";
                if( command == null || command.Length == 0 ){
                    return;
                }
                command = command.Replace( "$Y", "{0}" );
                command = command.Replace( "$M", "{1}" );
                command = command.Replace( "$D", "{2}" );
                command = command.Replace( "$U", "{3}" );
                command = command.Replace( "$$", "{4}" );
                command = String.Format( command, year, month, day, uid, "$" );
                command = command.Replace( "\\", "\\\\" );
                command = command.Replace( "\"", "\\\"" );

                string argument = SilverlightGadget.Settings.Argument;
                // string argument = "-show \"te\\st\"";
                argument = argument.Replace( "$Y", "{0}" );
                argument = argument.Replace( "$M", "{1}" );
                argument = argument.Replace( "$D", "{2}" );
                argument = argument.Replace( "$U", "{3}" );
                argument = argument.Replace( "$$", "{4}" );
                argument = String.Format( argument, year, month, day, uid, "$" );
                argument = argument.Replace( "\\", "\\\\" );
                argument = argument.Replace( "\"", "\\\"" );

                string jscommand = "ExecCommand( \"" +
                    command + "\", \"" + argument + "\" );";

                // HtmlPage.Window.Eval( "ExecCommand( \"http://www.google.co.jp/\", \"\" )" );
                HtmlPage.Window.Eval( jscommand );
            }
        }

    }


    public class TVEvent
    {
        public static string DefaultForeground = "Black";
        public static string DefaultBackground = "Transparent";

        public string Title { get; set; }
        public bool TitleOnly = false;
        public iCalEvent Event;
        public iCalToDo  ToDo;

        private ObservableCollection<TVItem> childItems = new ObservableCollection<TVItem>();
        public ObservableCollection<TVItem> ChildItems {
            get{ return childItems; }
            set{ childItems = value; }
        }

        public string ForegroundColor { get; set; }
        public string BackgroundColor { get; set; }

        public Style Style { get; set; }

        private Visibility rectangleVisibility = Visibility.Collapsed;
        public Visibility RectangleVisibility {
            get { return rectangleVisibility; }
            set {
                if( rectangleVisibility != value ){
                    rectangleVisibility = value;
                    OnPropertyChanged( "RectangleVisibility" );
                }
            }
        }

        public TVEvent( iCalEvent ev, DayView dv, bool titleOnly ){
            Event = ev;
            Title = ev.Summary.Value.Text;

            RectangleVisibility = Visibility.Collapsed;

            TitleOnly = titleOnly;
            if( titleOnly ){
                ForegroundColor = "Red";
                Style = (Style)dv.Resources["LikeLabel"]; 
                return;
            } else {
                // Color = "#FF73A9D8";
                ForegroundColor = DefaultForeground;
                Style = (Style)dv.Resources["LinkType"]; 
            }
            BackgroundColor = DefaultBackground;

            iCalTimeRelatedType timeFromRelated = null;
            if( ev.DateTimeStart != null ){
                timeFromRelated = ev.DateTimeStart.Value;
            }
            iCalTimeRelatedType timeToRelated = null;
            if( ev.DateTimeEnd != null ){
                timeToRelated = ev.DateTimeEnd.Value;
            }

            string result = TimeString( timeFromRelated, timeToRelated, dv );

            TVItem item;
            if( result.Length > 0 ){
                item = new TVItem( result, true );
                childItems.Add( item );
            }

            if( ev.Location != null ){
                item = new TVItem( ev.Location.Value.Text, false );
                childItems.Add( item );
            }
        }

        public TVEvent( iCalToDo ev, DayView dv, bool titleOnly ){
            ToDo = ev;
            Title = ev.Summary.Value.Text;

            RectangleVisibility = Visibility.Visible;

            if( titleOnly ){
                ForegroundColor = "Red";
                Style = (Style)dv.Resources["LikeLabel"]; 
                return;
            } else {
                // Color = "#FF73A9D8";
                ForegroundColor = DefaultForeground;
                Style = (Style)dv.Resources["LinkType"]; 
            }
            BackgroundColor = DefaultBackground;
            
            iCalTimeRelatedType timeFromRelated = null;
            if( ev.DateTimeStart != null ){
                timeFromRelated = ev.DateTimeStart.Value;
            }
            iCalTimeRelatedType timeToRelated = null;
            if( ev.DateTimeDue != null ){
                timeToRelated = ev.DateTimeDue.Value;
            }

            string result = TimeString( timeFromRelated, timeToRelated, dv );

            TVItem item;
            if( result.Length > 0 ){
                item = new TVItem( result, true );
                childItems.Add( item );
            }

            if( ev.Location != null ){
                item = new TVItem( ev.Location.Value.Text, false );
                childItems.Add( item );
            }
        }

        public TVEvent( iCalToDo ev, TodoView dv, bool titleOnly ){
            ToDo = ev;
            Title = ev.Summary.Value.Text;

            RectangleVisibility = Visibility.Visible;

            ForegroundColor = DefaultForeground;
            Style = (Style)dv.Resources["LinkType"]; 

            BackgroundColor = DefaultBackground;
            
            iCalTimeRelatedType timeFromRelated = null;
            if( ev.DateTimeStart != null ){
                timeFromRelated = ev.DateTimeStart.Value;
            }

            iCalTimeRelatedType timeToRelated = null;
            if( ev.DateTimeDue != null ){
                timeToRelated = ev.DateTimeDue.Value;
            }

            iCalDate dateFrom = timeFromRelated as iCalDate;
            iCalDateTime datetimeFrom = timeFromRelated as iCalDateTime;
            iCalDate dateTo = timeToRelated as iCalDate;
            iCalDateTime datetimeTo = timeToRelated as iCalDateTime;

            DateTime now = DateTime.Now;

            string fromString = "";

            if( datetimeFrom != null ){
                dateFrom = datetimeFrom.Date;
            }
            if( dateFrom != null ){
                if( now.Year != dateFrom.Year ){
                    fromString = dateFrom.Year.ToString() + "/";
                }
                fromString += dateFrom.Month.ToString() + "/" +
                    dateFrom.Day.ToString();
            }
            if( datetimeFrom != null ){
                fromString += " " + datetimeFrom.Time.Hour.ToString() + ":" +
                    datetimeFrom.Time.Minute.ToString( "00" );

                if( datetimeFrom.Time.Second != 0 ){
                    fromString += ":" + datetimeFrom.Time.Second.ToString( "00" );
                }
            }

            string toString = "";

            if( datetimeTo != null ){
                dateTo = datetimeTo.Date;
            }
            if( dateTo != null && datetimeTo == null ){
                // with date(not datetime) spcified event,
                // end date should be set back 1 day.
                dateTo = dateTo.PreviousDay();
            }
            if( dateTo != null ){
                if( dateFrom != null && dateTo == dateFrom ){
                } else {
                    if( now.Year != dateTo.Year ){
                        toString = dateTo.Year.ToString() + "/";
                    }
                    toString += dateTo.Month.ToString() + "/" +
                        dateTo.Day.ToString();
                }
            }
            if( datetimeTo != null ){
                if( toString.Length > 0 ){
                    toString += " ";
                }
                toString += datetimeTo.Time.Hour.ToString() + ":" +
                    datetimeTo.Time.Minute.ToString( "00" );

                if( datetimeTo.Time.Second != 0 ){
                    toString += ":" + datetimeTo.Time.Second.ToString( "00" );
                }
            }
                    
            string result = "";
            if( fromString.Length > 0 ){
                result = fromString;
            }

            if( toString.Length > 0 ){
                result += " - " + toString;
            } else if( result.Length > 0 ){
                result += " - ";
            }
            
            TVItem item;
            if( result.Length > 0 ){
                item = new TVItem( result, true );
                childItems.Add( item );
            }

            if( ev.Priority != null ){
                item = new TVItem( ev.Priority.Value.ToString(), false );
                childItems.Add( item );
            }
        }

        protected String TimeString( iCalTimeRelatedType timeFromRelated,
                                     iCalTimeRelatedType timeToRelated,
                                     DayView dv )
        {
            iCalDate dateFrom = timeFromRelated as iCalDate;
            iCalDateTime datetimeFrom = timeFromRelated as iCalDateTime;
            iCalDate dateTo = timeToRelated as iCalDate;
            iCalDateTime datetimeTo = timeToRelated as iCalDateTime;

            string result = null;

            if( dateFrom != null && dateTo != null &&
                dateFrom.NextDay() == dateTo ){
                
                // result = "終日";
                result = Localize.StringLibrary.AllDay;
            } else {

                string from = "";

                bool isNeeded = false;

                if( datetimeFrom != null ){
                    dateFrom = datetimeFrom.Date;
                }

                if( dateFrom != null ){
                    if( dateFrom.Year != dv.Year ){
                        from = dateFrom.Year.ToString() + "/";
                        isNeeded = true;
                    }
                    if( isNeeded || dateFrom.Month != dv.Month ){
                        from += dateFrom.Month.ToString() + "/";
                        isNeeded = true;
                    }
                    if( isNeeded || dateFrom.Day != dv.Day ){
                        if( !isNeeded ){
                            switch( dateFrom.Day ){
                            case 1:
                                from += Localize.StringLibrary.Day1;
                                break;
                            case 2:
                                from += Localize.StringLibrary.Day2;
                                break;
                            case 3:
                                from += Localize.StringLibrary.Day3;
                                break;
                            default:
                                from += dateFrom.Day.ToString() +
                                    Localize.StringLibrary.DayOther;
                                break;
                            }
                            // from += dateFrom.Day.ToString() + "日";

                        } else {
                            from += dateFrom.Day.ToString();
                        }
                        isNeeded = true;
                    }
                }

                if( datetimeFrom != null ){
                    if( isNeeded ) from += " ";

                    from += datetimeFrom.Time.Hour.ToString() + ":";
                    from += datetimeFrom.Time.Minute.ToString( "00" );
                    if( datetimeFrom.Time.Second != 0 ){
                        from += ":" + datetimeFrom.Time.Second.ToString( "00" );
                    }
                }

                if( from.Length == 0 && timeFromRelated != null ){
                    // from = "今日";
                    from = Localize.StringLibrary.Today;
                }
            
                string to = "";
                isNeeded = false;
                if( datetimeTo != null ){
                    dateTo = datetimeTo.Date;
                }
                if( dateTo != null && datetimeTo == null ){
                    // with date(not datetime) spcified event,
                    // end date should be set back 1 day.
                    dateTo = dateTo.PreviousDay();
                }
                if( dateTo != null ){
                    if( dateTo.Year != dv.Year ){
                        to = dateTo.Year.ToString() + "/";
                        isNeeded = true;
                    }
                    if( isNeeded || dateTo.Month != dv.Month ){
                        to += dateTo.Month.ToString() + "/";
                        isNeeded = true;
                    }
                    if( isNeeded || dateTo.Day != dv.Day ){
                        if( !isNeeded ){
                            // if( dateFrom.NextDay() == dateTo ){
                            if( dateFrom == dateTo ){
                                to = ""; // all day?
                            } else {
                                // to += dateTo.Day.ToString() + "日";
                                switch( dateTo.Day ){
                                case 1:
                                    to += Localize.StringLibrary.Day1;
                                    break;
                                case 2:
                                    to += Localize.StringLibrary.Day2;
                                    break;
                                case 3:
                                    to += Localize.StringLibrary.Day3;
                                    break;
                                default:
                                    to += dateTo.Day.ToString() +
                                        Localize.StringLibrary.DayOther;
                                    break;
                                }
                            }
                        } else {
                            to += dateTo.Day.ToString();
                        }
                        isNeeded = true;
                    }
                    
                }

                if( datetimeTo != null ){
                    if( isNeeded ) to += " ";

                    to += datetimeTo.Time.Hour.ToString() + ":";
                    to += datetimeTo.Time.Minute.ToString( "00" );
                    if( datetimeTo.Time.Second != 0 ){
                        to += ":" + datetimeTo.Time.Second.ToString( "00" );
                    }
                }

                if( to.Length == 0 && timeToRelated != null ){
                    // to = "今日";
                    to = Localize.StringLibrary.Today;
                }

                if( dateTo == null ){
                    result = from + " - ";
                } else if( from.Length > 0 ){
                    result = from + " - ";
                    if( to.Length > 0 ){
                        result += to;
                    }
                } else {
                    if( to.Length > 0 ){
                        result = " -" + to;
                    } else {
                        // result = "終日";
                        result = Localize.StringLibrary.AllDay;
                    }
                }
            }

            return result;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }

    public class TVItem
    {
        public static int StaticLineHeight = 0;
        public static string DefaultForeground = "Black";
        public static string DefaultBackground = "Transparent";
        
        public string Title { get; set; }
        public string FontFamily { get; set; }
        public int LineHeight { get; set; }
        public string ForegroundColor { get; set; }
        public string BackgroundColor { get; set; }

        public TVItem( string title, bool timeString ){
            this.Title = title;

            if( timeString ){
                FontFamily = "Times New Roman";
            } else {
                FontFamily = "-";
            }
            LineHeight = StaticLineHeight;

            ForegroundColor = DefaultForeground;
            BackgroundColor = DefaultBackground;
        }
    }
}
