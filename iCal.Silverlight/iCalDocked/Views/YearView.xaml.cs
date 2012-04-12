// Copyright 2011 Miyako Komooka
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;

using SilverlightGadgetUtilities;
using iCalControls;

namespace iCalDocked.Views {
    public partial class YearView : System.Windows.Controls.Page {

        private HyperlinkUnderline[] Month;

        public YearView() {
            InitializeComponent();
            Month = new HyperlinkUnderline[12];
            Month[0] = Mon1;
            Month[1] = Mon2;
            Month[2] = Mon3;
            Month[3] = Mon4;
            Month[4] = Mon5;
            Month[5] = Mon6;
            Month[6] = Mon7;
            Month[7] = Mon8;
            Month[8] = Mon9;
            Month[9] = Mon10;
            Month[10] = Mon11;
            Month[11] = Mon12;

            
        }

        private void ApplySettings()
        {
            iCalSettingsCollection Settings = SilverlightGadget.Settings;
            DateTime now = DateTime.Now;

            HomeIcon.FillColor = Settings.HomeIconColor.Color;

            for( int i = 0; i < 12; i++ ){
                Month[i].Foreground = Settings.NormalDayForeground.Solid;
                Month[i].Background = Settings.NormalDayBackground.Solid;
                if( Settings.NormalDayBold ){
                    Month[i].FontWeight = FontWeights.Bold;
                } else {
                    Month[i].FontWeight = FontWeights.Normal;
                }
                Month[i].Underline = Settings.NormalDayUnderline;

                if( Year == now.Year && i + 1 == now.Month ){
                    if( Settings.TodayForeground.Solid != null ){
                        Month[i].Foreground = Settings.TodayForeground.Solid;
                    }
                    if( Settings.TodayBackground.Solid != null ){
                        Month[i].Background = Settings.TodayBackground.Solid;
                    }
                    if( Settings.TodayBold ){
                        Month[i].FontWeight = FontWeights.Bold;
                    } else {
                        Month[i].FontWeight = FontWeights.Normal;
                    }
                    Month[i].Underline = Settings.TodayUnderline;
                }
            }
        }

        public void ApplyAndRefresh()
        {
            ApplySettings();
        }

        public int Year = 0;
        public iCalDocked.Page NavigationParent = null;

        // ユーザーがこのページに移動したときに実行されます。
        protected override void OnNavigatedTo(NavigationEventArgs e) {
            if( NavigationContext.QueryString.ContainsKey("year") ){
                Year = int.Parse(NavigationContext.QueryString["year"]);
            } else {
                Year = DateTime.Now.Year;
            }

            
            String format = SilverlightGadget.Settings.YearViewMonthFormat;
            if( format == null || format.Length == 0 ){
                format = Localize.StringLibrary.YearViewMonthFormat;
            }
            for( int i = 0; i < 12; i++ ){
                Month[i].NavigateUri = 
                    new Uri( "/Month/" + Year .ToString() + "/"
                             + (i+1).ToString(),
                             UriKind.Relative );

                DateTime dt = new DateTime( Year, i+1, 1 );
                Month[i].Content = dt.ToString( format );
            }            

            ApplySettings();

            if( NavigationParent != null ){
                NavigationParent.YearViewShowedCallback( Year );
            }
        }
    }
}
