using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BindingSample.ViewModels;
using Xamarin.Forms;

namespace BindingSample.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel Vm { get => this.BindingContext as MainPageViewModel; }

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnEmployeeTextChanged(object sender, TextChangedEventArgs args)
        {
            string input = args.NewTextValue;
            if( input.Length == (int)Resources["EMPLOYEE_NO_LENGTH"])
            {
                Vm.SetEmployeeNo(input);
            }
            else
            {
                Vm.SetEmployeeNo(null);
            }
        }
    }
}
