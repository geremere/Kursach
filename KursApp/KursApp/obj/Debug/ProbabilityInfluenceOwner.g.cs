﻿#pragma checksum "..\..\ProbabilityInfluenceOwner.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "264177117248FE5A45E1980D6D2E57445B2F4C28BAE85E6D54BBDB8D99C4EB35"
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
    /// ProbabilityInfluenceOwner
    /// </summary>
    public partial class ProbabilityInfluenceOwner : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\ProbabilityInfluenceOwner.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TBProb;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\ProbabilityInfluenceOwner.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LInf;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\ProbabilityInfluenceOwner.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LProb;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\ProbabilityInfluenceOwner.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Owners;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\ProbabilityInfluenceOwner.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SetUp;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\ProbabilityInfluenceOwner.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TBINf;
        
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
            System.Uri resourceLocater = new System.Uri("/KursApp;component/probabilityinfluenceowner.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ProbabilityInfluenceOwner.xaml"
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
            
            #line 8 "..\..\ProbabilityInfluenceOwner.xaml"
            ((KursApp.ProbabilityInfluenceOwner)(target)).Activated += new System.EventHandler(this.Window_Activated);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TBProb = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.LInf = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.LProb = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.Owners = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.SetUp = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\ProbabilityInfluenceOwner.xaml"
            this.SetUp.Click += new System.Windows.RoutedEventHandler(this.SetUp_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.TBINf = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

