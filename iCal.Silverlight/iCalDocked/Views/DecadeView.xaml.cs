// Copyright 2011 Miyako Komooka
using System;
using System.Collections.Generic;
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
    public partial class DecadeView : System.Windows.Controls.Page {

        private HyperlinkUnderline[] Years;

        public DecadeView() {
            InitializeComponent();

            Years = new HyperlinkUnderline[11];
            Years[0] = Year1;
            Years[1] = Year2;
            Years[2] = Year3;
            Years[3] = Year4;
            Years[4] = Year5;
            Years[5] = Year6;
            Years[6] = Year7;
            Years[7] = Year8;
            Years[8] = Year9;
            Years[9] = Year10;
            Years[10] = Year11;
        }

        private void ApplySettings()
        {
            iCalSettingsCollection Settings = SilverlightGadget.Settings;
            DateTime now = DateTime.Now;

            HomeIcon.FillColor = Settings.HomeIconColor.Color;

            for( int i = 0; i < 11; i++ ){
                Years[i].Foreground = Settings.NormalDayForeground.Solid;
                Years[i].Background = Settings.NormalDayBackground.Solid;
                if( Settings.NormalDayBold ){
                    Years[i].FontWeight = FontWeights.Bold;
                } else {
                    Years[i].FontWeight = FontWeights.Normal;
                }
                Years[i].Underline = Settings.NormalDayUnderline;

                if( DecadeStartYear + i == now.Year ){
                    if( Settings.TodayForeground.Solid != null ){
                        Years[i].Foreground = Settings.TodayForeground.Solid;
                    }
                    if( Settings.TodayBackground.Solid != null ){
                        Years[i].Background = Settings.TodayBackground.Solid;
                    }
                    if( Settings.TodayBold ){
                        Years[i].FontWeight = FontWeights.Bold;
                    } else {
                        Years[i].FontWeight = FontWeights.Normal;
                    }
                    Years[i].Underline = Settings.TodayUnderline;
                }
            }
        }

        public void ApplyAndRefresh()
        {
            ApplySettings();
        }

        public int DecadeStartYear = 0;
        public iCalDocked.Page NavigationParent = null;

        // ユーザーがこのページに移動したときに実行されます。
        protected override void OnNavigatedTo(NavigationEventArgs e) {
            if( NavigationContext.QueryString.ContainsKey("startyear") ){
                DecadeStartYear = int.Parse(NavigationContext.QueryString["startyear"]);
            } else {
                DecadeStartYear = DateTime.Now.Year / 10 * 10;
            }

            for( int i = 0; i < 11; i++ ){
                Years[i].Content = (DecadeStartYear + i).ToString();
                Years[i].NavigateUri =
                    new Uri( "/Year/" + (DecadeStartYear + i).ToString(),
                             UriKind.Relative );
            }

            ApplySettings();

            if( NavigationParent != null ){
                NavigationParent.DecadeViewShowedCallback( DecadeStartYear );
            }
        }
    }
}
