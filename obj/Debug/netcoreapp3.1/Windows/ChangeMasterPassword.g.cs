﻿#pragma checksum "..\..\..\..\Windows\ChangeMasterPassword.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "B23B15ED4EBE76739C3132F59467E66E44B85DF0"
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


namespace OlibPasswordManager.Windows {
    
    
    /// <summary>
    /// ChangeMasterPassword
    /// </summary>
    public partial class ChangeMasterPassword : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\..\Windows\ChangeMasterPassword.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox TxtOldPassword;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\Windows\ChangeMasterPassword.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxtOldPasswordCollapsed;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\Windows\ChangeMasterPassword.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CbOldHide;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\Windows\ChangeMasterPassword.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox TxtPassword;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\Windows\ChangeMasterPassword.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxtPasswordCollapsed;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\Windows\ChangeMasterPassword.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CbHide;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\Windows\ChangeMasterPassword.xaml"
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
            System.Uri resourceLocater = new System.Uri("/OlibPasswordManager;V1.3.0.296;component/windows/changemasterpassword.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Windows\ChangeMasterPassword.xaml"
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
            this.TxtOldPassword = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 2:
            this.TxtOldPasswordCollapsed = ((System.Windows.Controls.TextBox)(target));
            
            #line 24 "..\..\..\..\Windows\ChangeMasterPassword.xaml"
            this.TxtOldPasswordCollapsed.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtOldPasswordCollapsed_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.CbOldHide = ((System.Windows.Controls.CheckBox)(target));
            
            #line 25 "..\..\..\..\Windows\ChangeMasterPassword.xaml"
            this.CbOldHide.Checked += new System.Windows.RoutedEventHandler(this.OldCollapsedPassword);
            
            #line default
            #line hidden
            
            #line 25 "..\..\..\..\Windows\ChangeMasterPassword.xaml"
            this.CbOldHide.Unchecked += new System.Windows.RoutedEventHandler(this.OldCollapsedPassword);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TxtPassword = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 31 "..\..\..\..\Windows\ChangeMasterPassword.xaml"
            this.TxtPassword.PasswordChanged += new System.Windows.RoutedEventHandler(this.txtPassword_PasswordChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.TxtPasswordCollapsed = ((System.Windows.Controls.TextBox)(target));
            
            #line 32 "..\..\..\..\Windows\ChangeMasterPassword.xaml"
            this.TxtPasswordCollapsed.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPasswordCollapsed_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.CbHide = ((System.Windows.Controls.CheckBox)(target));
            
            #line 33 "..\..\..\..\Windows\ChangeMasterPassword.xaml"
            this.CbHide.Checked += new System.Windows.RoutedEventHandler(this.CollapsedPassword);
            
            #line default
            #line hidden
            
            #line 33 "..\..\..\..\Windows\ChangeMasterPassword.xaml"
            this.CbHide.Unchecked += new System.Windows.RoutedEventHandler(this.CollapsedPassword);
            
            #line default
            #line hidden
            return;
            case 7:
            this.PbHard = ((System.Windows.Controls.ProgressBar)(target));
            
            #line 36 "..\..\..\..\Windows\ChangeMasterPassword.xaml"
            this.PbHard.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.pbHard_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 44 "..\..\..\..\Windows\ChangeMasterPassword.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

