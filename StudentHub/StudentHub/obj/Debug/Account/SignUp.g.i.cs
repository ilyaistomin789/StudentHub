﻿#pragma checksum "..\..\..\Account\SignUp.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "981904004BC3CD403D28AE0A3AEB230F6E8250667A69F0A7C54C5989639057F6"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro.IconPacks;
using MahApps.Metro.IconPacks.Converter;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using StudentHub.Account;
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


namespace StudentHub.Account {
    
    
    /// <summary>
    /// SignUp
    /// </summary>
    public partial class SignUp : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 33 "..\..\..\Account\SignUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Image;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Account\SignUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox reg_UserName;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\Account\SignUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox reg_Password;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\Account\SignUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox reg_PasswordConfirm;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\..\Account\SignUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button reg_SignUp;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\..\Account\SignUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button reg_GoToLogin;
        
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
            System.Uri resourceLocater = new System.Uri("/StudentHub;component/account/signup.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Account\SignUp.xaml"
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
            
            #line 11 "..\..\..\Account\SignUp.xaml"
            ((StudentHub.Account.SignUp)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.SignUp_OnMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Image = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.reg_UserName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.reg_Password = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 5:
            this.reg_PasswordConfirm = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 6:
            this.reg_SignUp = ((System.Windows.Controls.Button)(target));
            
            #line 113 "..\..\..\Account\SignUp.xaml"
            this.reg_SignUp.Click += new System.Windows.RoutedEventHandler(this.Reg_SignUp_OnClick);
            
            #line default
            #line hidden
            return;
            case 7:
            this.reg_GoToLogin = ((System.Windows.Controls.Button)(target));
            
            #line 114 "..\..\..\Account\SignUp.xaml"
            this.reg_GoToLogin.Click += new System.Windows.RoutedEventHandler(this.Reg_GoToLogin_OnClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

