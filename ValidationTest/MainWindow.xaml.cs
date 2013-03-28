using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ReactiveUI;
using ReactiveUI.Xaml;

namespace ValidationTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewFor<ViewModel>
    {
        IDisposable disableValidation;

        public MainWindow()
        {
            InitializeComponent();

            ViewModel = new ViewModel();

            this.Bind(ViewModel, x => x.MyValidatedProperty, v => v.MyTextBox.Text);
            this.DisplayValidationFor(x => x.MyValidatedProperty);

            this.Bind(ViewModel, x => x.NestedViewModel.MyNotifyProperty, v => v.MySecondTextBox.Text);
            disableValidation = this.DisplayValidationFor(x => x.NestedViewModel.MyNotifyProperty);

        }


        #region ViewModel
        public ViewModel ViewModel
        {
            get { return (ViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(ViewModel), typeof(MainWindow), new PropertyMetadata(null));


        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ViewModel)value; }
        }
        #endregion

        void SetError_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NestedViewModel.HasErrors = true;
        }

        void ClearError_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NestedViewModel.HasErrors = false;
        }

        void DisableError_OnClick(object sender, RoutedEventArgs e)
        {
            disableValidation.Dispose();
        }
    }
}
