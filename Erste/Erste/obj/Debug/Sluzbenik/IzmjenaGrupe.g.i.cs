﻿#pragma checksum "..\..\..\Sluzbenik\IzmjenaGrupe.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "04B30B9667D442527FACBA0F0886D0A87E634917"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Erste.Sluzbenik;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Erste.Sluzbenik {
    
    
    /// <summary>
    /// IzmjenaGrupe
    /// </summary>
    public partial class IzmjenaGrupe : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\Sluzbenik\IzmjenaGrupe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NazivBox;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Sluzbenik\IzmjenaGrupe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button1;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Sluzbenik\IzmjenaGrupe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button2;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Sluzbenik\IzmjenaGrupe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Sluzbenik\IzmjenaGrupe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox JezikCombo;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Sluzbenik\IzmjenaGrupe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox NivoKursaCombo;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Sluzbenik\IzmjenaGrupe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker TimePickerOd;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Sluzbenik\IzmjenaGrupe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker TimePickerDo;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Erste;component/sluzbenik/izmjenagrupe.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Sluzbenik\IzmjenaGrupe.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.NazivBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.Button1 = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\Sluzbenik\IzmjenaGrupe.xaml"
            this.Button1.Click += new System.Windows.RoutedEventHandler(this.Button1Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Button2 = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\Sluzbenik\IzmjenaGrupe.xaml"
            this.Button2.Click += new System.Windows.RoutedEventHandler(this.Button_Otkazi_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.label = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.JezikCombo = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.NivoKursaCombo = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.TimePickerOd = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 8:
            this.TimePickerDo = ((System.Windows.Controls.DatePicker)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

