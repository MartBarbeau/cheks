﻿#pragma checksum "..\..\Visualizer.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "533EEFBF868994192A28A62F89D08819"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.18444
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace CHEKS.CHEKSVisualizer {
    
    
    /// <summary>
    /// Visualizer
    /// </summary>
    public partial class Visualizer : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\Visualizer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Viewport3D mainViewport;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\Visualizer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.Media3D.OrthographicCamera camera;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\Visualizer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.Media3D.ModelVisual3D Light1;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\Visualizer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.Media3D.ModelVisual3D Light2;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\Visualizer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.Media3D.ModelVisual3D Light3;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\Visualizer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas canvasOn3D;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\Visualizer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnChangerPlan;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\Visualizer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnIMG;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\Visualizer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock statusPane;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\Visualizer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas controlPane;
        
        #line default
        #line hidden
        
        
        #line 121 "..\..\Visualizer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblSysteme;
        
        #line default
        #line hidden
        
        
        #line 125 "..\..\Visualizer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblAgents;
        
        #line default
        #line hidden
        
        
        #line 131 "..\..\Visualizer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblRelations;
        
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
            System.Uri resourceLocater = new System.Uri("/CHEKSVisualizer;component/visualizer.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Visualizer.xaml"
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
            
            #line 4 "..\..\Visualizer.xaml"
            ((CHEKS.CHEKSVisualizer.Visualizer)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.OnKeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.mainViewport = ((System.Windows.Controls.Viewport3D)(target));
            return;
            case 3:
            this.camera = ((System.Windows.Media.Media3D.OrthographicCamera)(target));
            return;
            case 4:
            this.Light1 = ((System.Windows.Media.Media3D.ModelVisual3D)(target));
            return;
            case 5:
            this.Light2 = ((System.Windows.Media.Media3D.ModelVisual3D)(target));
            return;
            case 6:
            this.Light3 = ((System.Windows.Media.Media3D.ModelVisual3D)(target));
            return;
            case 7:
            this.canvasOn3D = ((System.Windows.Controls.Canvas)(target));
            
            #line 73 "..\..\Visualizer.xaml"
            this.canvasOn3D.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.OnViewportMouseUp);
            
            #line default
            #line hidden
            
            #line 74 "..\..\Visualizer.xaml"
            this.canvasOn3D.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.OnViewportMouseDown);
            
            #line default
            #line hidden
            
            #line 75 "..\..\Visualizer.xaml"
            this.canvasOn3D.MouseMove += new System.Windows.Input.MouseEventHandler(this.OnViewportMouseMove);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnChangerPlan = ((System.Windows.Controls.Button)(target));
            
            #line 81 "..\..\Visualizer.xaml"
            this.btnChangerPlan.Click += new System.Windows.RoutedEventHandler(this.btnChangerPlan_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnIMG = ((System.Windows.Controls.Button)(target));
            
            #line 93 "..\..\Visualizer.xaml"
            this.btnIMG.Click += new System.Windows.RoutedEventHandler(this.btnIMG_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.statusPane = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 11:
            this.controlPane = ((System.Windows.Controls.Canvas)(target));
            return;
            case 12:
            this.lblSysteme = ((System.Windows.Controls.Label)(target));
            return;
            case 13:
            this.lblAgents = ((System.Windows.Controls.Label)(target));
            return;
            case 14:
            this.lblRelations = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

