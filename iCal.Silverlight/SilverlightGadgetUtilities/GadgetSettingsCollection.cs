// Copyright 2011 Miyako Komooka
// Based on Silverlight Vista Sidebar Gadget Template by Ioan Lazarciuc
using System;
using System.Windows.Browser;

namespace SilverlightGadgetUtilities
{
    /// <summary>
    /// Utility class that facilitates manipulation of Sidebar Gadget settings
    /// </summary>
    public class GadgetSettingsCollection
    {
        private static GadgetSettingsCollection inst;
        static GadgetSettingsCollection()
        {
            inst = new GadgetSettingsCollection();
        }

        /// <summary>
        /// Gets the singleton instance of the class
        /// </summary>
        public static GadgetSettingsCollection Instance
        {
            get
            {
                return inst;
            }
        }

        protected GadgetSettingsCollection()
        {
        }

        /// <summary>
        /// Gets or sets a Sidebar Gadget setting
        /// </summary>
        /// <param name="key">the key for the setting</param>
        /// <returns>the current value of the setting</returns>
        public virtual string this[string key]
        {
            get
            {
                string str =
                    String.Format( "if( window.System == undefined )" +
                                   " null; else " +
                                   "System.Gadget.Settings.readString(\"{0}\"); ",
                                   key);
                
                string result = HtmlPage.Window.Eval(str) as string;
                if( result == null ){
                    result = "";
                }

                return result;
            }
            set
            {
                ScriptObject sobj =
                    HtmlPage.Window.Eval("if( window.System == undefined ) null; else System.Gadget.Settings") as ScriptObject;
                if( sobj != null ){
                    sobj.Invoke("writeString", key, value);
                }
                // (HtmlPage.Window.Eval("System.Gadget.Settings") as ScriptObject).Invoke("writeString", key, value);
            }
        }
    }
}
