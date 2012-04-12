// Copyright 2011 Miyako Komooka
// Based on Silverlight Vista Sidebar Gadget Template by Ioan Lazarciuc
using System;
using System.Windows.Browser;

namespace SilverlightGadgetUtilities
{
    /// <summary>
    /// Enumeration representing the action (button) chosen when closing the settings window
    /// </summary>
    public enum CloseAction
    {
        /// <summary>
        /// Ok button pressed
        /// </summary>
        Commit,
        /// <summary>
        /// Cancel button pressed
        /// </summary>
        Cancel
    }

    /// <summary>
    /// The event argument that defines the properties for the closing functionality of the gadget Settings dialog.
    /// </summary>
    public class SettingsClosingEventArgs : EventArgs
    {
        private ScriptObject sevent;

        /// <summary>
        /// Gets the action chosen when closing the gadget settings window.
        /// </summary>
        public CloseAction Action { get; private set; }
        /// <summary>
        /// Gets whether the onSettingsClosing event can be cancelled.
        /// </summary>
        public bool Cancellable { get; private set; }
        /// <summary>
        /// Gets whether to cancel the onSettingsClosing event.
        /// <remarks>Read-only because the SetProperty throws an exception</remarks>
        /// </summary>
        public bool Cancel
        {
            get
            {
                return Convert.ToBoolean(sevent.GetProperty("cancel"));
            }
            //set
            //{
            //    sevent.SetProperty("cancel", value.ToString());
            //}
        }

        /// <summary>
        /// Constructs a managed closing event from a corresponding JavaScript event.
        /// </summary>
        /// <param name="sevent">The JavaScript event argument for the SettingsClosing event.</param>
        public SettingsClosingEventArgs(ScriptObject sevent)
            : base()
        {
            this.sevent = sevent;

            byte act = Convert.ToByte(sevent.GetProperty("closeAction"));

            if (act == 0)
                Action = CloseAction.Commit;
            else Action = CloseAction.Cancel;

            Cancellable = Convert.ToBoolean(sevent.GetProperty("cancellable"));
        }
    }

    /// <summary>
    /// Helper class to rig up events of the JavaScript Sidebar Gadget API into managed code
    /// <remarks>Instantiate only once for each Xaml page.</remarks>
    /// </summary>
    [ScriptableType]
    public class SilverlightGadgetEvents
    {
        /// <summary>
        /// Event fired when the gadget Settings dialog is closed.
        /// </summary>
        /// <remarks>This event cannot be triggered in the flyout.
        /// Settings events can only be used either in the settings page or in the gadget page due to imposibility
        /// of attaching multiple event handlers to gadget javascript events.</remarks>
        public event EventHandler<SettingsClosingEventArgs> SettingsClosed;
        /// <summary>
        /// Event fired when the gadget Settings dialog begins the process of closing.
        /// </summary>
        /// <remarks>This event cannot be triggered in the flyout.
        /// Settings events can only be used either in the settings page or in the gadget page due to imposibility
        /// of attaching multiple event handlers to gadget javascript events.</remarks>
        public event EventHandler<SettingsClosingEventArgs> SettingsClosing;
        /// <summary>
        /// Event fired when the gadget Settings dialog is requested.
        /// </summary>
        /// <remarks>This event does not get fired for the settings, flyout controls.
        /// Settings events can only be used either in the settings page or in the gadget page due to imposibility
        /// of attaching multiple event handlers to gadget javascript events.</remarks>
        public event EventHandler ShowSettings;
        /// <summary>
        /// Event fired when the gadget is docked on the Windows Sidebar.
        /// </summary>
        /// <remarks>This event does not get fired for the docked, undocked, flyout controls.</remarks>
        public event EventHandler Dock;
        /// <summary>
        /// Event fired when the gadget is undocked from the Windows Sidebar.
        /// </summary>
        /// <remarks>This event does not get fired for the docked, undocked, flyout controls.</remarks>
        public event EventHandler Undock;
        /// <summary>
        /// Event fired when the gadget visibility changes due to the Windows Sidebar being hidden or displayed.
        /// </summary>
        /// <remarks>This event does not get fired for the undocked(unable to produce), settings(confirmed), flyout(confirmed) controls.</remarks>
        public event EventHandler VisibilityChanged;

        public SilverlightGadgetEvents(string registerName)
        {
            HtmlPage.RegisterScriptableObject(registerName, this);
        }

        /// <summary>
        /// Callback to be used by the JavaScript code to trigger the managed SettingsClosed event.
        /// </summary>
        /// <param name="sevent">The JavaScript event argument for the SettingsClosed event.</param>
        [ScriptableMember]
        public void ScriptSettingsClosedCallback(ScriptObject sevent)
        {
            OnSettingsClosed(new SettingsClosingEventArgs(sevent));
        }

        /// <summary>
        /// Raises the SettingsClosed event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSettingsClosed(SettingsClosingEventArgs e)
        {
            if (SettingsClosed != null)
            {
                SettingsClosed(this, e);
            }
        }

        /// <summary>
        /// Callback to be used by the JavaScript code to trigger the managed SettingsClosing event.
        /// </summary>
        /// <param name="sevent">The JavaScript event argument for the SettingsClosing event.</param>
        [ScriptableMember]
        public void ScriptSettingsClosingCallback(ScriptObject sevent)
        {
            OnSettingsClosing(new SettingsClosingEventArgs(sevent));
        }

        /// <summary>
        /// Raises the SettingsClosing event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSettingsClosing(SettingsClosingEventArgs e)
        {
            if (SettingsClosing != null)
            {
                SettingsClosing(this, e);
            }
        }

        /// <summary>
        /// Callback to be used by the JavaScript code to trigger the managed ShowSettings event.
        /// </summary>
        [ScriptableMember]
        public void ScriptShowSettingsCallback()
        {
            OnShowSettings(EventArgs.Empty);
        }

        /// <summary>
        /// Raises the ShowSettings event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnShowSettings(EventArgs e)
        {
            if (ShowSettings != null)
            {
                ShowSettings(this, e);
            }
        }

        /// <summary>
        /// Callback to be used by the JavaScript code to trigger the managed Dock event.
        /// </summary>
        [ScriptableMember]
        public void ScriptDockCallback()
        {
            OnDock(EventArgs.Empty);
        }

        /// <summary>
        /// Raises the Dock event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnDock(EventArgs e)
        {
            if (Dock != null)
            {
                Dock(this, e);
            }
        }

        /// <summary>
        /// Callback to be used by the JavaScript code to trigger the managed Undock event.
        /// </summary>
        [ScriptableMember]
        public void ScriptUndockCallback()
        {
            OnUndock(EventArgs.Empty);
        }

        /// <summary>
        /// Raises the Undock event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnUndock(EventArgs e)
        {
            if (Undock != null)
            {
                Undock(this, e);
            }
        }

        /// <summary>
        /// Callback to be used by the JavaScript code to trigger the managed VisibilityChanged event.
        /// </summary>
        [ScriptableMember]
        public void ScriptVisibilityChangedCallback()
        {
            OnVisibilityChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Raises the VisibilityChanged event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnVisibilityChanged(EventArgs e)
        {
            if (VisibilityChanged != null)
            {
                VisibilityChanged(this, e);
            }
        }
    }
}
