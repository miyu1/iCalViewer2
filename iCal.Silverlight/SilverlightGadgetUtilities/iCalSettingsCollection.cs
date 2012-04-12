// Copyright 2011 Miyako Komooka
using System;
using System.Net;
using System.Collections.Generic;

namespace SilverlightGadgetUtilities
{
    public class iCalSettingsCollection : GadgetSettingsCollection
    {
        private static iCalSettingsCollection inst;
        static iCalSettingsCollection()
        {
            inst = new iCalSettingsCollection();
        }

        public static new iCalSettingsCollection Instance {
            get { return inst; }
        }

        protected iCalSettingsCollection()
        {
            Colors = SilverlightColors.Colors();
        }

        protected Dictionary<string, ColorItem> Colors;

        public readonly string KeyDefaultSet        = "DefaultSet";

        public readonly string KeyCalendarFile        = "CalendarFile";
        public readonly string KeyCalendarUID         = "CalendarUID";
        public readonly string KeyCalendarPassword    = "CalendarPass";

        public readonly string KeyHolidayFile         = "HolidayFile";
        public readonly string KeyHolidayUID          = "HolidayUID";
        public readonly string KeyHolidayPassword     = "HolidayPass";

        public readonly string KeyReloadInterval      = "Interval";

        public readonly string KeyBackgroundColor     = "Background";
        public readonly string KeyArrowColor          = "Arrow";
        public readonly string KeyHomeIconColor       = "HomeIcon";
        public readonly string KeyReloadIconColor     = "ReloadIcon";
        public readonly string KeyClockIconColor      = "ClockIcon";
        public readonly string KeyInfoIconColor       = "InfoIcon";

        public readonly string KeyNormalDayBackground = "NormalBack";
        public readonly string KeyNormalDayForeground = "NormalFore";
        public readonly string KeyNormalDayBold       = "NormalBold";
        public readonly string KeyNormalDayUnderline  = "NormalUnder";

        public readonly string KeyTodayBackground  = "TodayBack";
        public readonly string KeyTodayForeground  = "TodayFore";
        public readonly string KeyTodayBold        = "TodayBold";
        public readonly string KeyTodayUnderline   = "TodayUnder";

        public readonly string KeyEventDayBackground  = "EventBack";
        public readonly string KeyEventDayForeground  = "EventFore";
        public readonly string KeyEventDayBold        = "EventBold";
        public readonly string KeyEventDayUnderline   = "EventUnder";

        public readonly string KeyTodoDayBackground   = "TodoBack";
        public readonly string KeyTodoDayForeground   = "TodoFore";
        public readonly string KeyTodoDayBold         = "TodoBold";
        public readonly string KeyTodoDayUnderline    = "TodoUnder";

        public readonly string KeyCommand             = "Command";
        public readonly string KeyArgument            = "Argument";

        public readonly string KeyDayViewTitleFormat    =  "DVTitleFormat";
        public readonly string KeyMonthViewTitleFormat  =  "MVTitleFormat";
        public readonly string KeyYearViewMonthFormat = "YVMonthFormat";

        public bool DefaultSet {
            get {
                string ret = this[KeyDefaultSet];

                if( ret == null || ret.Length == 0 ){
                    return false;
                }
                if( ret == "true" ){
                    return true;
                }
                return false;
            }
            set {
                string str;
                if( value ){
                    str = "true";
                } else {
                    str = "false";
                }
                this[KeyDefaultSet] = str;
            }
        }

        public string CalendarFile {
            get { return this[KeyCalendarFile]; }
            set { this[KeyCalendarFile] = value; }
        }

        public string CalendarUID {
            get { return this[KeyCalendarUID]; }
            set { this[KeyCalendarUID] = value; }
        }

        public string CalendarPassword {
            get { return decodePassword( this[KeyCalendarPassword] ); }
            set { this[KeyCalendarPassword] = encodePassword( value ); }
        }

        public string HolidayFile {
            get { return this[KeyHolidayFile]; }
            set { this[KeyHolidayFile] = value; }
        }

        public string HolidayUID {
            get { return this[KeyHolidayUID]; }
            set { this[KeyHolidayUID] = value; }
        }

        public string HolidayPassword {
            get { return decodePassword( this[KeyHolidayPassword] ); }
            set { this[KeyHolidayPassword] = encodePassword( value ); }
        }

        public int ReloadInterval {
            get {
                string ret = this[KeyReloadInterval];
                if( ret == null || ret.Length == 0 ){
                    return 15;
                }
                try {
                    return int.Parse( ret );
                } catch( System.SystemException ){
                    return 15;
                }
            }

            set {
                if( value >= 0 ){
                    this[KeyReloadInterval] = value.ToString();
                } else {
                    this[KeyReloadInterval] = "";
                }
            }
        }

        public ColorItem BackgroundColor {
            get {
                string ret = this[KeyBackgroundColor];
                if( Colors.ContainsKey( ret ) ){
                    return Colors[ret];
                } else {
                    // return Colors["Pink"];
                    return Colors["White"];
                }
            }
            set {
                this[KeyBackgroundColor] = value.Name;
            }
        }

        public ColorItem ArrowColor {
            get {
                string ret = this[KeyArrowColor];
                if( Colors.ContainsKey( ret ) ){
                    return Colors[ret];
                } else {
                    return Colors["White"];
                }
            }
            set {
                this[KeyArrowColor] = value.Name;
            }
        }

        public ColorItem HomeIconColor {
            get {
                string ret = this[KeyHomeIconColor];
                if( Colors.ContainsKey( ret ) ){
                    return Colors[ret];
                } else {
                    return Colors["LightGray"];
                }
            }
            set {
                this[KeyHomeIconColor] = value.Name;
            }
        }

        public ColorItem ReloadIconColor {
            get {
                string ret = this[KeyReloadIconColor];
                if( Colors.ContainsKey( ret ) ){
                    return Colors[ret];
                } else {
                    return Colors["WhiteSmoke"];
                }
            }
            set {
                this[KeyReloadIconColor] = value.Name;
            }
        }

        public ColorItem ClockIconColor {
            get {
                string ret = this[KeyClockIconColor];
                if( Colors.ContainsKey( ret ) ){
                    return Colors[ret];
                } else {
                    return Colors["WhiteSmoke"];
                }
            }
            set {
                this[KeyClockIconColor] = value.Name;
            }
        }

        public ColorItem InfoIconColor {
            get {
                string ret = this[KeyInfoIconColor];
                if( Colors.ContainsKey( ret ) ){
                    return Colors[ret];
                } else {
                    return Colors["WhiteSmoke"];
                }
            }
            set {
                this[KeyInfoIconColor] = value.Name;
            }
        }

        public ColorItem NormalDayBackground {
            get {
                string ret = this[KeyNormalDayBackground];
                if( Colors.ContainsKey( ret ) ){
                    return Colors[ret];
                } else {
                    return Colors["Transparent"];
                }
            }
            set {
                this[KeyNormalDayBackground] = value.Name;
            }
        }

        public ColorItem NormalDayForeground {
            get {
                string ret = this[KeyNormalDayForeground];
                if( Colors.ContainsKey( ret ) ){
                    return Colors[ret];
                } else {
                    return Colors["Black"];
                }
            }
            set {
                this[KeyNormalDayForeground] = value.Name;
            }
        }

        public bool NormalDayBold {
            get {
                string ret = this[KeyNormalDayBold];

                if( ret == null || ret.Length == 0 ){
                    return false;
                }
                if( ret == "true" ){
                    return true;
                }
                return false;
            }
            set {
                string str;
                if( value ){
                    str = "true";
                } else {
                    str = "false";
                }
                this[KeyNormalDayBold] = str;
            }
        }

        public bool NormalDayUnderline {
            get {
                string ret = this[KeyNormalDayUnderline];

                if( ret == null || ret.Length == 0 ){
                    return false;
                }
                if( ret == "true" ){
                    return true;
                }
                return false;
            }
            set {
                string str;
                if( value ){
                    str = "true";
                } else {
                    str = "false";
                }
                this[KeyNormalDayUnderline] = str;
            }
        }

        public ColorItem TodayBackground {
            get {
                string ret = this[KeyTodayBackground];
                if( Colors.ContainsKey( ret ) ){
                    return Colors[ret];
                } else {
                    return Colors["LightGray"];
                }
            }
            set {
                this[KeyTodayBackground] = value.Name;
            }
        }

        public ColorItem TodayForeground {
            get {
                string ret = this[KeyTodayForeground];
                if( Colors.ContainsKey( ret ) ){
                    return Colors[ret];
                } else {
                    return ColorItem.BlankColorItem();
                }
            }
            set {
                this[KeyTodayForeground] = value.Name;
            }
        }

        public bool TodayBold {
            get {
                string ret = this[KeyTodayBold];

                if( ret == null || ret.Length == 0 ){
                    return false;
                }
                if( ret == "true" ){
                    return true;
                }
                return false;
            }
            set {
                string str;
                if( value ){
                    str = "true";
                } else {
                    str = "false";
                }
                this[KeyTodayBold] = str;
            }
        }

        public bool TodayUnderline {
            get {
                string ret = this[KeyTodayUnderline];

                if( ret == null || ret.Length == 0 ){
                    return false;
                }
                if( ret == "true" ){
                    return true;
                }
                return false;
            }
            set {
                string str;
                if( value ){
                    str = "true";
                } else {
                    str = "false";
                }
                this[KeyTodayUnderline] = str;
            }
        }

        public ColorItem EventDayBackground {
            get {
                string ret = this[KeyEventDayBackground];
                if( Colors.ContainsKey( ret ) ){
                    return Colors[ret];
                } else {
                    return ColorItem.BlankColorItem();
                }
            }
            set {
                this[KeyEventDayBackground] = value.Name;
            }
        }

        public ColorItem EventDayForeground {
            get {
                string ret = this[KeyEventDayForeground];
                if( Colors.ContainsKey( ret ) ){
                    return Colors[ret];
                } else {
                    return ColorItem.BlankColorItem();
                }
            }
            set {
                this[KeyEventDayForeground] = value.Name;
            }
        }

        public bool EventDayBold {
            get {
                string ret = this[KeyEventDayBold];

                if( ret == null || ret.Length == 0 ){
                    return true;
                }
                if( ret == "true" ){
                    return true;
                }
                return false;
            }
            set {
                string str;
                if( value ){
                    str = "true";
                } else {
                    str = "false";
                }
                this[KeyEventDayBold] = str;
            }
        }

        public bool EventDayUnderline {
            get {
                string ret = this[KeyEventDayUnderline];

                if( ret == null || ret.Length == 0 ){
                    return false;
                }
                if( ret == "true" ){
                    return true;
                }
                return false;
            }
            set {
                string str;
                if( value ){
                    str = "true";
                } else {
                    str = "false";
                }
                this[KeyEventDayUnderline] = str;
            }
        }
 
        public ColorItem TodoDayBackground {
            get {
                string ret = this[KeyTodoDayBackground];
                if( Colors.ContainsKey( ret ) ){
                    return Colors[ret];
                } else {
                    return ColorItem.BlankColorItem();
                }
            }
            set {
                this[KeyTodoDayBackground] = value.Name;
            }
        }

        public ColorItem TodoDayForeground {
            get {
                string ret = this[KeyTodoDayForeground];
                if( Colors.ContainsKey( ret ) ){
                    return Colors[ret];
                } else {
                    return ColorItem.BlankColorItem();
                }
            }
            set {
                this[KeyTodoDayForeground] = value.Name;
            }
        }

        public bool TodoDayBold {
            get {
                string ret = this[KeyTodoDayBold];

                if( ret == null || ret.Length == 0 ){
                    return false;
                }
                if( ret == "true" ){
                    return true;
                }
                return false;
            }
            set {
                string str;
                if( value ){
                    str = "true";
                } else {
                    str = "false";
                }
                this[KeyTodoDayBold] = str;
            }
        }

        public bool TodoDayUnderline {
            get {
                string ret = this[KeyTodoDayUnderline];

                if( ret == null || ret.Length == 0 ){
                    return true;
                }
                if( ret == "true" ){
                    return true;
                }
                return false;
            }
            set {
                string str;
                if( value ){
                    str = "true";
                } else {
                    str = "false";
                }
                this[KeyTodoDayUnderline] = str;
            }
        }
 
        public string Command {
            get { return this[KeyCommand]; }
            set { this[KeyCommand] = value; }
        }

        public string Argument {
            get { return this[KeyArgument]; }
            set { this[KeyArgument] = value; }
        }

        public string DayViewTitleFormat  {
            get {return this[KeyDayViewTitleFormat]; }
            set { this[KeyDayViewTitleFormat] = value; }
        }

        public string MonthViewTitleFormat  {
            get {return this[KeyMonthViewTitleFormat]; }
            set { this[KeyMonthViewTitleFormat] = value; }
        }

        public string YearViewMonthFormat {
            get {return this[KeyYearViewMonthFormat]; }
            set { this[KeyYearViewMonthFormat] = value; }
        }

        private string encodePassword( string pass ){
            string result = "";
            
            char[] array = pass.ToCharArray();
            pass = "";
            // pass1
            for( int i = 0; i < array.Length; i++ ){
                Int16 c = (Int16)array[i];
                if( c > 255 ){
                    pass += "%u" + c.ToString( "X" );
                } else if( array[i] == '%' ){
                    pass += "%" + c.ToString( "X" );
                } else {
                    pass += array[i];
                }
            }

            // pass2
            array = pass.ToCharArray();
            for( int i = 0; i < array.Length; i++ ){
                result += ((int)array[i] - 23).ToString();
            }
            return result;
        }

        private string decodePassword( string encodedStr ){
            string result = "";

            while( encodedStr.Length > 0 ){
                string str = encodedStr.Substring( 0, 2 );
                encodedStr = encodedStr.Substring( 2 );

                int num = int.Parse( str );
                result += (char)(num + 23);
            }
            result = System.Windows.Browser.HttpUtility.UrlDecode( result );
            return result;
        }
    }
}