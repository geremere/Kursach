﻿#pragma checksum "..\..\RiskTreeForRiskManager.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "822361F39A0650619D2FE0064A6354120205A4EE70B8A2AB1F61164E85A3C0FF"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using KursApp;
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


namespace KursApp {
    
    
    /// <summary>
    /// RiskTreeForRiskManager
    /// </summary>
    public partial class RiskTreeForRiskManager : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\RiskTreeForRiskManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grid;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\RiskTreeForRiskManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Back;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\RiskTreeForRiskManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid cnv;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\RiskTreeForRiskManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock RiskName;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\RiskTreeForRiskManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Prob;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\RiskTreeForRiskManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox COST;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\RiskTreeForRiskManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DESC;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\RiskTreeForRiskManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Add;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\RiskTreeForRiskManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ProbabilityTB;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\RiskTreeForRiskManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CostTB;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\RiskTreeForRiskManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DescriptionTB;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\RiskTreeForRiskManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Delite;
        
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
            System.Uri resourceLocater = new System.Uri("/KursApp;component/risktreeforriskmanager.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\RiskTreeForRiskManager.xaml"
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
            
            #line 8 "..\..\RiskTreeForRiskManager.xaml"
            ((KursApp.RiskTreeForRiskManager)(target)).Activated += new System.EventHandler(this.Window_Activated);
            
            #line default
            #line hidden
            return;
            case 2:
            this.grid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.Back = ((System.Windows.Controls.Button)(target));
            
            #line 10 "..\..\RiskTreeForRiskManager.xaml"
            this.Back.Click += new System.Windows.RoutedEventHandler(this.Back_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.cnv = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.RiskName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.Prob = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.COST = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.DESC = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.Add = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\RiskTreeForRiskManager.xaml"
            this.Add.Click += new System.Windows.RoutedEventHandler(this.AddNewVertex_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.ProbabilityTB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            this.CostTB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 12:
            this.DescriptionTB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 13:
            this.Delite = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\RiskTreeForRiskManager.xaml"
            this.Delite.Click += new System.Windows.RoutedEventHandler(this.Delite_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

