using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using BindingSample.Models;
using Prism.Mvvm;
using Reactive.Bindings;
using Xamarin.Forms;

namespace BindingSample.ViewModels
{
    public class TeamViewModel : BindableBase
    {
        public Team Team { get; set; }
        public string Title { get => Team.Name; }
        public ReactiveProperty<Color> BackgroundColor { get; set; } = new ReactiveProperty<Color>();
        protected ObservableCollection<EmployeeSafetyViewModel> _employeeSafetyVms = new ObservableCollection<EmployeeSafetyViewModel>();
        public ObservableCollection<EmployeeSafetyViewModel> EmployeeSafetyVms
        {
            get
            {
                return _employeeSafetyVms;
            }
            set
            {
                if (_employeeSafetyVms != null)
                {
                    foreach (EmployeeSafetyViewModel detail in _employeeSafetyVms)
                    {
                        detail.PropertyChanged -= Detail_PropertyChanged;
                    }
                }

                _employeeSafetyVms = value;

                if(_employeeSafetyVms != null)
                {
                    foreach(EmployeeSafetyViewModel detail in _employeeSafetyVms)
                    {
                        detail.PropertyChanged += Detail_PropertyChanged;
                    }

                    SetBackgroundColor();
                }
            }
        }

        private void Detail_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SetBackgroundColor();
        }

        private void SetBackgroundColor()
        {
            if (EmployeeSafetyVms.All(x => x.Employee.Safety.Status == Safety.EStatus.Safe))
            {
                BackgroundColor.Value = Color.Transparent;
            }
            else
            {
                BackgroundColor.Value = Color.Khaki;
            }
        }

        public TeamViewModel()
        {
        }

    }
}
