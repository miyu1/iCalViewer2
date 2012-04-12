// Copyright 2011 Miyako Komooka
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using SilverlightGadgetUtilities;

namespace iCalControls {
    public class ColorPicker : ComboBox {
        public ColorPicker() {
            this.DefaultStyleKey = typeof(ColorPicker);

            ObservableCollection<ColorItem> collection = 
                new ObservableCollection<ColorItem>( SilverlightColors.Colors().Values );

            ColorItem defaultItem = new ColorItem( "", 0x00, 0xFF, 0xFF, 0xFF );
            defaultItem.Border = "Transparent";
            collection.Insert( 0, defaultItem );

            this.ItemsSource = collection;
            // this.ItemsSource = new ObservableCollection<ColorItem>( SilverlightColors.Colors().Values );   
            // this.ItemTemplate = (DataTemplate)this.Resources["ColorListTemplate"];
        }
    }
}
