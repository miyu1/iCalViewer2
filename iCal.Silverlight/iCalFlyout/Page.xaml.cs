// Silverlight Vista Sidebar Gadget Template created by Ioan Lazarciuc
// http://www.lazarciuc.ro/ioan
// Contact form present on website
// Based on the Vista Sidebar Gadget Template created by Tim Heuer 
// http://timheuer.com/blog/

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Browser;
using SilverlightGadgetUtilities;

namespace iCalFlyout
{
    public partial class Page : UserControl
    {
        SilverlightGadgetEvents evHelper;
        public Page()
        {
            InitializeComponent();
            // Event helper not used because gadget events can't be triggered when the flyout is shown.
            //evHelper = new SilverlightGadgetEvents("SilverlightGadgetEvents");

            // Set gadget flyout size according to the one specified in the Silverlight control.
            SilverlightGadget.SetFlyoutSize(Width, Height);
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // If through code the size of the Silverlight control is changed, change the size in the javascript gadget also.
            SilverlightGadget.SetFlyoutSize(e.NewSize.Width, e.NewSize.Height);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SilverlightGadget.Flyout(false);
        }
    }
}
