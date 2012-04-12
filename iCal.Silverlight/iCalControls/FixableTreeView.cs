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

namespace iCalControls
{
    public class FixableTreeView : TreeView
    {
        public FixableTreeView()
        {
            this.DefaultStyleKey = typeof(FixableTreeView);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new FixableTreeViewItem();
        }

        protected override bool IsItemItsOwnContainerOverride(Object item){
            if( item is FixableTreeViewItem ){
                return true;
            } else {
                return false;
            }
        }

    }

    [TemplateVisualStateAttribute(Name = "Collapse", GroupName = "ChildDisplayMode")]
    [TemplateVisualStateAttribute(Name = "Fix", GroupName = "ChildDisplayMode")]
    public class FixableTreeViewItem : TreeViewItem
    {

        public FixableTreeViewItem()
        {
            this.DefaultStyleKey = typeof(FixableTreeViewItem);
            // ChildDisplayMode = ChildDisplayModeEnum.Collapse;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new FixableTreeViewItem();
        }

        protected override bool IsItemItsOwnContainerOverride(Object item){
            if( item is FixableTreeViewItem ){
                return true;
            } else {
                return false;
            }
        }

        public enum ChildDisplayModeEnum {
            Collapse,   // collapse/expand child items is allowed by button
            Fix         // fix tree item, collapse/expand not allowed,
                        // means always show child item.
        }

        public static readonly DependencyProperty ChildDisplayModeProperty =
            DependencyProperty.Register(
                "ChildDisplayMode",
                typeof(ChildDisplayModeEnum),
                typeof(FixableTreeViewItem),
                new PropertyMetadata(new PropertyChangedCallback(ChildDisplayModeChangedCallback))
            );

        public ChildDisplayModeEnum ChildDisplayMode {
            get {
                return (ChildDisplayModeEnum)GetValue(ChildDisplayModeProperty);
            }

            set {
                SetValue(ChildDisplayModeProperty, value);
            }
        }


        private static void
            ChildDisplayModeChangedCallback(DependencyObject obj,
                                 DependencyPropertyChangedEventArgs args)
        {
            FixableTreeViewItem ctl = (FixableTreeViewItem)obj;
            // ChildDisplayModeEnum value = (ChildDisplayModeEnum)args.NewValue;

            ctl.UpdateStatus(true);
            
            /*
            if( ctl.ChildDisplayMode != value ){
                ctl.ChildDisplayMode = value;
            }
             */
        }

        private void UpdateStatus(bool useTransition)
        {
            if( ChildDisplayMode == ChildDisplayModeEnum.Fix ){
                IsExpanded = true;
                VisualStateManager.GoToState(this,"Fix",useTransition);
            } else {
                VisualStateManager.GoToState(this,"Collapse",useTransition);
            }
        }

        public override void OnApplyTemplate()
        {
            /*
            UpButtonElement = GetTemplateChild("UpButton") as RepeatButton;
            DownButtonElement = GetTemplateChild("DownButton") as RepeatButton;
            TextElement = GetTemplateChild("TextBlock") as TextBlock;
            */

            base.OnApplyTemplate();

            // Object obj = GetTemplateChild("Header");
            UpdateStatus(false);
        }

        /* protected override void OnGotFocus( RoutedEventArgs e ) {} */

    }
}
