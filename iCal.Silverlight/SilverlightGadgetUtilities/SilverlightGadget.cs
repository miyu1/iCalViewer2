// Copyright 2011 Miyako Komooka
// Based on Silverlight Vista Sidebar Gadget Template by Ioan Lazarciuc
using System;
using System.Windows.Browser;

namespace SilverlightGadgetUtilities
{
    /// <summary>
    /// Utility class for interacting with the Gadget Sidebar API.
    /// </summary>
    public static class SilverlightGadget
    {
        /// <summary>
        /// Gets or sets settings for the gadget.
        /// </summary>
        /*
        public static GadgetSettingsCollection Settings
        {
            get { return GadgetSettingsCollection.Instance; }
        }
        */

        public static iCalSettingsCollection Settings =
            iCalSettingsCollection.Instance;

        /// <summary>
        /// Sets the URL for the HTML page used as flyout.
        /// <remarks>Write-only because getter throws exception</remarks>
        /// </summary>
        public static string FlyoutPage
        {
            //get
            //{
            //    return HtmlPage.Window.Eval("System.Gadget.Flyout.file") as string;
            //}
            set
            {
                String str = String.Format("if( window.System != undefined )" +
                                           " System.Gadget.Flyout.file=\"{0}\";",
                                           value);
                HtmlPage.Window.Eval( str );
                // HtmlPage.Window.Eval(String.Format("System.Gadget.Flyout.file=\"{0}\";", value));
            }
        }

        /// <summary>
        /// Gets or sets the URL for the HTML page used as a settings page.
        /// </summary>
        public static string SettingsUI
        {
            get
            {
                String str = "if( window.System == undefined ) null; else"
                             + " System.Gadget.settingsUI;";
                return HtmlPage.Window.Eval( str ) as string;
                // return HtmlPage.Window.Eval("System.Gadget.settingsUI") as string;
            }
            set
            {
                String str = String.Format("if( window.System != undefined )" +
                                           " System.Gadget.settingsUI=\"{0}\";"
                                           , value);
                HtmlPage.Window.Eval( str );
                // HtmlPage.Window.Eval(String.Format("System.Gadget.settingsUI=\"{0}\";", value));
            }
        }

        /// <summary>
        /// Gets or sets the URL for the image used as background for the gadget.
        /// </summary>
        public static string BackgroundImageUrl
        {
            get
            {
                String str = "if( window.System == undefined ) null; " +
                    "else System.Gadget.background; ";
                return HtmlPage.Window.Eval( str ) as string;
                // return HtmlPage.Window.Eval("System.Gadget.background") as string;
            }
            set
            {
                String str = String.Format("if( window.System != undefined )" +
                                           " System.Gadget.background=\"{0}\";"
                                           , value);
                HtmlPage.Window.Eval( str );
                // HtmlPage.Window.Eval(String.Format("System.Gadget.background=\"{0}\";", value));
            }
        }

        /// <summary>
        /// Gets the docked state of the gadget.
        /// </summary>
        public static bool Docked
        {
            get
            {
                // String str = "if( window.System == undefined ) true; else System.Gadget.docked";
                String str = "if( window.System == undefined ) false; else System.Gadget.docked";
                Object ret = HtmlPage.Window.Eval(str);
                return Convert.ToBoolean(ret);
                // return Convert.ToBoolean(HtmlPage.Window.Eval("System.Gadget.docked"));
            }
        }

        /// <summary>
        /// Gets the name of the gadget.
        /// </summary>
        public static string Name
        {
            get
            {
                String str = "if( window.System == undefined ) null; " +
                    " else System.Gadget.name; ";
                return Convert.ToString( HtmlPage.Window.Eval( str ) );
            }
        }

        /// <summary>
        /// Gets the opacity of the gadget.
        /// </summary>
        public static byte Opacity
        {
            get
            {
                String str = "if( window.System == undefined ) 0; " +
                    " else System.Gadget.opacity; ";
                return Convert.ToByte(HtmlPage.Window.Eval(str));
            }
        }

        /// <summary>
        /// Gets the UNC path where the gadget is installed.
        /// </summary>
        public static string Path
        {
            get
            {
                String str = "if( window.System == undefined ) null; " +
                    " else System.Gadget.path; ";
                return Convert.ToString(HtmlPage.Window.Eval(str));
                // return Convert.ToString(HtmlPage.Window.Eval("System.Gadget.path"));
            }
        }

        /// <summary>
        /// Gets the version of the Sidebar API.
        /// </summary>
        public static string PlatformVersion
        {
            get
            {
                String str = "if( window.System == undefined ) null; " +
                    " else System.Gadget.platformVersion; ";
                return Convert.ToString(HtmlPage.Window.Eval(str));
                // return Convert.ToString(HtmlPage.Window.Eval("System.Gadget.platformVersion"));
            }
        }

        /// <summary>
        /// Gets the version of the gadget.
        /// </summary>
        public static string Version
        {
            get
            {
                String str = "if( window.System == undefined ) null; " +
                    " else System.Gadget.version; ";
                return Convert.ToString(HtmlPage.Window.Eval(str));
                 // return Convert.ToString(HtmlPage.Window.Eval("System.Gadget.version"));
             }
         }

        /// <summary>
        /// Gets the visibility of the gadget.
        /// </summary>
        public static bool Visible
        {
            get
            {
                String str = "if( window.System == undefined ) null; " +
                    " else System.Gadget.visible; ";
                return Convert.ToBoolean(HtmlPage.Window.Eval(str));
                // return Convert.ToBoolean(HtmlPage.Window.Eval("System.Gadget.visible"));
            }
        }

        /// <summary>
        /// Opens the flyout, with the currently specified page.
        /// </summary>
        public static void Flyout()
        {
            Flyout(true);
        }

        /// <summary>
        /// Opens or closes the flyout.
        /// </summary>
        /// <param name="show">true to open, false to close the flyout</param>
        public static void Flyout(bool show)
        {
            var value = show ? "true" : "false";

            String str = String.Format( "if( window.System != undefined )" +
                                        " System.Gadget.Flyout.show = {0};"
                                        , value);
            HtmlPage.Window.Eval( str );
            // HtmlPage.Window.Eval(String.Format("System.Gadget.Flyout.show = {0};", value));
        }

        /// <summary>
        /// Set the size of the body tag of the page that hosts the gadget.
        /// </summary>
        /// <param name="width">width in pixels</param>
        /// <param name="height">height in pixels</param>
        public static void SetGadgetSize(int width, int height)
        {
            String str =
                String.Format( "if( window.System != undefined ){{ " +
                               "var oBody = System.Gadget.document.body.style;"+
                               "oBody.width = '{0}px';" +
                               "oBody.height = '{1}px'; }}",
                               width, height);
            HtmlPage.Window.Eval( str );

            // HtmlPage.Window.Eval(String.Format("var oBody = System.Gadget.document.body.style;oBody.width" +
            //                                    " = '{0}px';oBody.height = '{1}px';", width, height));
        }

        /// <summary>
        /// Set the size of the body tag of the page that hosts the gadget.
        /// </summary>
        /// <param name="width">width in pixels</param>
        /// <param name="height">height in pixels</param>
        /// <remarks>size values are converted to integers by rounding.</remarks>
        public static void SetGadgetSize(double width, double height)
        {
            SetGadgetSize(Convert.ToInt32(width), Convert.ToInt32(height));
        }

        /// <summary>
        /// Set the size of the body tag of the page.
        /// </summary>
        /// <param name="width">width in pixels</param>
        /// <param name="height">height in pixels</param>
        public static void SetPageSize(int width, int height)
        {
            String str =
                String.Format( "if( window.System != undefined ){{ " +
                               "var oBody = document.body.style;"+
                               "oBody.width = '{0}px';" +
                               "oBody.height = '{1}px'; }}",
                               width, height );
            HtmlPage.Window.Eval( str );

            // HtmlPage.Window.Eval(String.Format("var oBody = document.body.style;oBody.width" +
            //                                    " = '{0}px';oBody.height = '{1}px';", width, height));
        }

        /// <summary>
        /// Set the size of the body tag of the page.
        /// </summary>
        /// <param name="width">width in pixels</param>
        /// <param name="height">height in pixels</param>
        /// <remarks>size values are converted to integers by rounding.</remarks>
        public static void SetPageSize(double width, double height)
        {
            SetPageSize(Convert.ToInt32(width), Convert.ToInt32(height));
        }

        /// <summary>
        /// Set the size of the body tag of the page that hosts the flyout.
        /// </summary>
        /// <param name="width">width in pixels</param>
        /// <param name="height">height in pixels</param>
        public static void SetFlyoutSize(int width, int height)
        {
            String str =
                String.Format( "if( window.System != undefined ){{ " +
                               "var oBody = System.Gadget.Flyout.document.body.style;"+
                               "oBody.width = '{0}px';" +
                               "oBody.height = '{1}px'; }}",
                               width, height );
            HtmlPage.Window.Eval( str );

            // HtmlPage.Window.Eval(String.Format("var oBody = System.Gadget.Flyout.document.body.style;oBody.width" +
            //                                    " = '{0}px';oBody.height = '{1}px';", width, height));
        }

        /// <summary>
        /// Set the size of the body tag of the page that hosts the flyout.
        /// </summary>
        /// <param name="width">width in pixels</param>
        /// <param name="height">height in pixels</param>
        /// <remarks>size values are converted to integers by rounding.</remarks>
        public static void SetFlyoutSize(double width, double height)
        {
            SetFlyoutSize(Convert.ToInt32(width), Convert.ToInt32(height));
        }

        /// <summary>
        /// Closes the current instance of the gadget.
        /// </summary>
        public static void Close()
        {
            String str = "if( window.System != undefined )" +
                " System.Gadget.close(); ";
            HtmlPage.Window.Eval( str );

            // HtmlPage.Window.Eval("System.Gadget.close();");
        }

        /// <summary>
        /// Check if currently runnning on Gadget Environment
        /// </summary>
        public static bool GadgetEnvironment
        {
            get {
                String str = "if( window.System == undefined ) false; else true";
                Object ret = HtmlPage.Window.Eval(str);
                return Convert.ToBoolean(ret);
            }
        }
    }
}
