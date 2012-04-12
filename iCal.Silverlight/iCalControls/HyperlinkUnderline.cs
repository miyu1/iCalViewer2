// Copyright 2011 Miyako Komooka
using System;
using System.Collections;
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

namespace iCalControls {

    [TemplateVisualStateAttribute(Name = "ShowUnderline", GroupName = "UnderlineMode")]
    [TemplateVisualStateAttribute(Name = "HideUnderline", GroupName = "UnderlineMode")]
    public class HyperlinkUnderline : HyperlinkButton {

        public static readonly DependencyProperty UnderlineProperty =
            DependencyProperty.Register(
                "Underline",
                typeof(Boolean),
                typeof(HyperlinkUnderline),
                new PropertyMetadata(new PropertyChangedCallback(UnderlineModeChangedCallback)) );

        public Boolean Underline {
            get {
                return (Boolean)GetValue( UnderlineProperty );
            }

            set {
                SetValue( UnderlineProperty, value );
            }
        }


        private static void
            UnderlineModeChangedCallback( DependencyObject obj,
                                          DependencyPropertyChangedEventArgs args)
        {
            HyperlinkUnderline ctl = (HyperlinkUnderline)obj;

            ctl.UpdateStatus( true );
        }

        public HyperlinkUnderline() {
            this.DefaultStyleKey = typeof(HyperlinkUnderline);
        }

        private void UpdateStatus(bool useTransition)
        {
            if( Underline ){
                VisualStateManager.GoToState( this,"ShowUnderline",
                                              useTransition);
            } else {
                VisualStateManager.GoToState( this,"HideUnderline",
                                              useTransition);
            }
        }

        public override void OnApplyTemplate()
        {
            /*
            UpButtonElement = GetTemplateChild("UpButton") as RepeatButton;
            DownButtonElement = GetTemplateChild("DownButton") as RepeatButton;
            TextElement = GetTemplateChild("TextBlock") as TextBlock;
            */

            TextBlock textBlock = GetTemplateChild("UnderlineTextBlock") as TextBlock;
            Line line = GetTemplateChild("Underline") as Line;

            base.OnApplyTemplate();

            // Object obj = GetTemplateChild("Header");
            UpdateStatus(false);
        }
    }
}
