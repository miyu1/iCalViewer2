// Copyright 2011 Miyako Komooka
using System;
using System.Collections.Generic;

namespace SilverlightGadgetUtilities
{
    public class iCalTempSettingsCollection : iCalSettingsCollection
    {
        private static iCalTempSettingsCollection inst;
        static iCalTempSettingsCollection() {
            inst = new iCalTempSettingsCollection();
        }

        public static new iCalTempSettingsCollection Instance {
            get { return inst; }
        }
            
        public iCalTempSettingsCollection(){
            string[] keys = new string[] {
                KeyDefaultSet,

                KeyCalendarFile,
                KeyCalendarUID,
                KeyCalendarPassword,

                KeyHolidayFile,
                KeyHolidayUID,
                KeyHolidayPassword,

                KeyReloadInterval,

                KeyBackgroundColor,
                KeyArrowColor,
                KeyHomeIconColor,
                KeyReloadIconColor,
                KeyClockIconColor,
                KeyInfoIconColor,

                KeyNormalDayBackground,
                KeyNormalDayForeground,
                KeyNormalDayBold,
                KeyNormalDayUnderline,

                KeyTodayBackground,
                KeyTodayForeground,
                KeyTodayBold,
                KeyTodayUnderline,

                KeyEventDayBackground,
                KeyEventDayForeground,
                KeyEventDayBold,
                KeyEventDayUnderline,

                KeyTodoDayBackground,
                KeyTodoDayForeground,
                KeyTodoDayBold,
                KeyTodoDayUnderline,

                KeyCommand,
                KeyArgument,

                KeyDayViewTitleFormat ,
                KeyMonthViewTitleFormat ,
                KeyYearViewMonthFormat
            };


            iCalSettingsCollection Settings = SilverlightGadget.Settings;
            for( int i=0; i < keys.Length; i++ ){
                string key = keys[i];

                TempSettings[ key ] = Settings[ key ];
            }
        }


        Dictionary<string, string> TempSettings =
            new Dictionary<string, string>();


        public override string this[string key]
        {
            get { return TempSettings[key]; }
            set {
                /*
                if( value == null || value.Length == 0 ){
                    TempSettings[key] = base[ key ];
                } else {
                    TempSettings[key] = value;
                }
                */
                TempSettings[key] = value;
            }
        }
    }
}
