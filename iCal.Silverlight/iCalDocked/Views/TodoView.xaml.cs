// Copyright 2011 Miyako Komooka
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using iCalLibrary;
using iCalLibrary.Component;
using iCalLibrary.Property;

namespace iCalDocked.Views {
    public partial class TodoView : System.Windows.Controls.Page {
        public TodoView() {
            InitializeComponent();

            int fontsize = (int)myTreeView.FontSize;
            TVItem.StaticLineHeight = fontsize + 1;

        }

        public ObservableCollection<TVEvent> TVEvents =
            new ObservableCollection<TVEvent>();
        public iCalDocked.Page NavigationParent = null;
        
        protected override void OnNavigatedTo(NavigationEventArgs e) {
            Refresh();
        }

        private void Refresh()
        {
            if( NavigationParent != null && NavigationParent.iColl != null ){
                TVEvents = new ObservableCollection<TVEvent>();
                
                foreach( iCalendar calendar in NavigationParent.iColl.CalendarList ){
                    foreach( iCalToDo todo in calendar.ToDoList ){
                        if( todo.Status == null ||
                            todo.Status.Value !=
                            iCalStatus.ValueType.Completed ){
                            
                            TVEvent tvevent = new TVEvent( todo, this, false );
                            TVEvents.Add( tvevent );

                        }
                    }
                }

                myTreeView.DataContext = TVEvents;
            } else {
                myTreeView.DataContext = null;
            }
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
        }

        public void ApplyAndRefresh()
        {
            ApplySettings();
            Refresh();
        }

        private void myTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            // TVEvent tvevent = myTreeView.SelectedItem as TVEvent;
            TVEvent tvevent = e.NewValue as TVEvent;
            if( tvevent != null ){
                iCalEntry entry = null;
                iCalToDo todo = null;
                if( tvevent.Event != null ){
                    entry = tvevent.Event;
                } else if( tvevent.ToDo != null ){
                    entry = tvevent.ToDo;
                    todo = tvevent.ToDo;
                } 

                if( entry == null ){
                    return;
                }

                string year="", month="", day="", uid="";

                if( todo.DateTimeDue != null ){
                    iCalLibrary.DataType.iCalTimeRelatedType timeRelated =
                        todo.DateTimeDue.Value;

                    if( timeRelated.Year > 0 ){
                        year = timeRelated.Year.ToString();
                    }
                    
                    if( timeRelated.Month > 0 ){
                        month = timeRelated.Month.ToString();
                    }
                    
                    if( timeRelated.Day > 0 ){
                        day = timeRelated.Day.ToString();
                    }

                } else if( entry.DateTimeStart != null ){

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
}
