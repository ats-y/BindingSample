using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BindingSample.Views
{
    public partial class DepartmentView : ContentView
    {
        public DepartmentView()
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
