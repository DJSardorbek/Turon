// Updated by XamlIntelliSenseFileGenerator 05.05.2021 20:41:41
#pragma checksum "..\..\qayt_sotuv_ucont.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "05DD2E05385AA68CDA7BAC304C9B7ECD5A80939EED2E374F4CCD5BB6B35186A9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using SotuvPlatformasi;
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


namespace SotuvPlatformasi
{


    /// <summary>
    /// qayt_sotuv_ucont
    /// </summary>
    public partial class qayt_sotuv_ucont : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector
    {

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SotuvPlatformasi;component/qayt_sotuv_ucont.xaml", System.UriKind.Relative);

#line 1 "..\..\qayt_sotuv_ucont.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:

#line 9 "..\..\qayt_sotuv_ucont.xaml"
                    ((SotuvPlatformasi.qayt_sotuv_ucont)(target)).KeyUp += new System.Windows.Input.KeyEventHandler(this.UserControl_KeyUp);

#line default
#line hidden
                    return;
                case 2:
                    this.txtSearch = ((System.Windows.Controls.TextBox)(target));

#line 24 "..\..\qayt_sotuv_ucont.xaml"
                    this.txtSearch.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtSearch_TextChanged);

#line default
#line hidden

#line 24 "..\..\qayt_sotuv_ucont.xaml"
                    this.txtSearch.KeyUp += new System.Windows.Input.KeyEventHandler(this.txtSearch_KeyUp);

#line default
#line hidden
                    return;
                case 3:
                    this.dateTimePicker1 = ((System.Windows.Controls.DatePicker)(target));

#line 36 "..\..\qayt_sotuv_ucont.xaml"
                    this.dateTimePicker1.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.dateTimePicker1_SelectedDateChanged);

#line default
#line hidden
                    return;
                case 4:
                    this.scrollViewer = ((System.Windows.Controls.ScrollViewer)(target));
                    return;
                case 5:
                    this.dataGridQaytuv = ((System.Windows.Controls.DataGrid)(target));
                    return;
                case 6:
                    this.BtnAsosiy = ((System.Windows.Controls.Button)(target));

#line 122 "..\..\qayt_sotuv_ucont.xaml"
                    this.BtnAsosiy.Click += new System.Windows.RoutedEventHandler(this.BtnAsosiy_Click);

#line default
#line hidden
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

