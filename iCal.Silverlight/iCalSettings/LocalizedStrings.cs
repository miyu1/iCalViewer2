// Copyright 2011 Miyako Komooka
using System;
using System.Globalization;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Threading;

namespace iCalSettings {
    public class LocalizedStrings {
        public LocalizedStrings() {
            // for test
            // Thread.CurrentThread.CurrentCulture = new CultureInfo( "en" );
            // Thread.CurrentThread.CurrentUICulture = new CultureInfo( "en" );
        }

        private static Localize.StringLibrary stringLibrary = new Localize.StringLibrary();

        public Localize.StringLibrary StringLibrary { get { return stringLibrary; } }
    }
}
