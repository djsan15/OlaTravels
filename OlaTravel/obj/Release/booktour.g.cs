﻿#pragma checksum "C:\Users\Sanchit\documents\visual studio 2013\Projects\OlaTravel\OlaTravel\booktour.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1EDC9989AF73792CC6B3A1662DFBC888"
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
    
    
    public partial class booktour : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBlock destination;
        
        internal System.Windows.Controls.TextBlock distanceblock;
        
        internal System.Windows.Controls.TextBlock fare;
        
        internal Microsoft.Phone.Controls.DatePicker dp;
        
        internal Microsoft.Phone.Controls.TimePicker tp;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/OlaTravel;component/booktour.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.destination = ((System.Windows.Controls.TextBlock)(this.FindName("destination")));
            this.distanceblock = ((System.Windows.Controls.TextBlock)(this.FindName("distanceblock")));
            this.fare = ((System.Windows.Controls.TextBlock)(this.FindName("fare")));
            this.dp = ((Microsoft.Phone.Controls.DatePicker)(this.FindName("dp")));
            this.tp = ((Microsoft.Phone.Controls.TimePicker)(this.FindName("tp")));
        }
    }
}

