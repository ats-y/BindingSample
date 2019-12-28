using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BindingSample.Views
{
    public partial class EmployeeView : ContentView
    {
        public EmployeeView()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            IsVisible = (BindingContext != null);
        }
    }
}
