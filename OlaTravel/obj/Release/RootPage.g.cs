﻿#pragma checksum "C:\Users\Sanchit\documents\visual studio 2013\Projects\OlaTravel\OlaTravel\RootPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E2335C0858AA64BFC14123132FB88EAD"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace OlaTravel {
    
    
    public partial class RootPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.Pivot piv;
        
        internal System.Windows.Controls.ListBox CurrTours_List;
        
        internal System.Windows.Controls.Button BookBut;
        
        internal Microsoft.Phone.Shell.ApplicationBar AppBar;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/OlaTravel;component/RootPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.piv = ((Microsoft.Phone.Controls.Pivot)(this.FindName("piv")));
            this.CurrTours_List = ((System.Windows.Controls.ListBox)(this.FindName("CurrTours_List")));
            this.BookBut = ((System.Windows.Controls.Button)(this.FindName("BookBut")));
            this.AppBar = ((Microsoft.Phone.Shell.ApplicationBar)(this.FindName("AppBar")));
        }
    }
}

