﻿#pragma checksum "..\..\..\..\Views\CreatePasswordStorageWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "DA0222D03889752CEE09C209AE2FFEC3BAC06B76"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace OlibKey.Views {
    
    
    /// <summary>
    /// CreatePasswordStorageWindow
    /// </summary>
    public partial class CreatePasswordStorageWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\..\Views\CreatePasswordStorageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxtPathSelection;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\Views\CreatePasswordStorageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox TxtPassword;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\Views\CreatePasswordStorageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxtPasswordCollapsed;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\Views\CreatePasswordStorageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CbHide;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\Views\CreatePasswordStorageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar PbHard;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.8.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/OlibKey;V2.1.0.0;component/views/createpasswordstoragewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\CreatePasswordStorageWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.8.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.TxtPathSelection = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            
            #line 22 "..\..\..\..\Views\CreatePasswordStorageWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SelectDirectory);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TxtPassword = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 27 "..\..\..\..\Views\CreatePasswordStorageWindow.xaml"
            this.TxtPassword.PasswordChanged += new System.Windows.RoutedEventHandler(this.TxtPassword_PasswordChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TxtPasswordCollapsed = ((System.Windows.Controls.TextBox)(target));
            
            #line 28 "..\..\..\..\Views\CreatePasswordStorageWindow.xaml"
            this.TxtPasswordCollapsed.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPasswordCollapsed_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.CbHide = ((System.Windows.Controls.CheckBox)(target));
            
            #line 29 "..\..\..\..\Views\CreatePasswordStorageWindow.xaml"
            this.CbHide.Checked += new System.Windows.RoutedEventHandler(this.cbHide_Checked);
            
            #line default
            #line hidden
            
            #line 29 "..\..\..\..\Views\CreatePasswordStorageWindow.xaml"
            this.CbHide.Unchecked += new System.Windows.RoutedEventHandler(this.cbHide_Checked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.PbHard = ((System.Windows.Controls.ProgressBar)(target));
            
            #line 32 "..\..\..\..\Views\CreatePasswordStorageWindow.xaml"
            this.PbHard.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.PbHard_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 41 "..\..\..\..\Views\CreatePasswordStorageWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CreateStorageButton);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 42 "..\..\..\..\Views\CreatePasswordStorageWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CancelButton);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

