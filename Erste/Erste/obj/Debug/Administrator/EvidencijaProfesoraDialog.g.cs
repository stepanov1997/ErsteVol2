﻿#pragma checksum "..\..\..\Administrator\EvidencijaProfesoraDialog.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C5F982417A0824D724F729B3CB3DE4C74A65DEBA251106C2F0034653B33A914C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Erste.Administrator;
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


namespace Erste.Administrator {
    
    
    /// <summary>
    /// EvidencijaProfesoraDialog
    /// </summary>
    public partial class EvidencijaProfesoraDialog : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\Administrator\EvidencijaProfesoraDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grid;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Administrator\EvidencijaProfesoraDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBox_Ime;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Administrator\EvidencijaProfesoraDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBox_Prezime;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Administrator\EvidencijaProfesoraDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBox_Email;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Administrator\EvidencijaProfesoraDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBox_BrojTelefona;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Administrator\EvidencijaProfesoraDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button1;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Administrator\EvidencijaProfesoraDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button2;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Administrator\EvidencijaProfesoraDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label;
        
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
            System.Uri resourceLocater = new System.Uri("/Erste;component/administrator/evidencijaprofesoradialog.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Administrator\EvidencijaProfesoraDialog.xaml"
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
            this.grid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.textBox_Ime = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.textBox_Prezime = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.textBox_Email = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.textBox_BrojTelefona = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.Button1 = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\Administrator\EvidencijaProfesoraDialog.xaml"
            this.Button1.Click += new System.Windows.RoutedEventHandler(this.Button1Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Button2 = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\Administrator\EvidencijaProfesoraDialog.xaml"
            this.Button2.Click += new System.Windows.RoutedEventHandler(this.Button_Otkazi_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.label = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

