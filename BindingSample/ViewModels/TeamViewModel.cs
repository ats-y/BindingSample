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
    /// <summary>
    /// チーム表示用ViewModel
    /// </summary>
    public class TeamViewModel : BindableBase
    {
        /// <summary>
        /// 表示対象のチーム情報
        /// </summary>
        public Team Team { get; set; }

        /// <summary>
        /// チーム名。
        /// </summary>
        public string Title { get => Team.Name; }

        /// <summary>
        /// 背景色。
        /// </summary>
        public ReactiveProperty<Color> BackgroundColor { get; set; } = new ReactiveProperty<Color>();

        /// <summary>
        /// 社員安否情報ViewModel
        /// </summary>
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
                        detail.PropertyChanged -= EmployeeSafetyVmPropertyChanged;
                    }
                }

                _employeeSafetyVms = value;

                if(_employeeSafetyVms != null)
                {
                    foreach(EmployeeSafetyViewModel detail in _employeeSafetyVms)
                    {
                        detail.PropertyChanged += EmployeeSafetyVmPropertyChanged;
                    }

                    SetBackgroundColor();
                    
                }
            }
        }

        private void EmployeeSafetyVmPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SetBackgroundColor();

            RaisePropertyChanged(nameof(EmployeeSafetyVms));
        }

        private void SetBackgroundColor()
        {
            if (EmployeeSafetyVms.All(x => x.CurrentStatus == Safety.EStatus.Safe))
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

        internal void Clear()
        {
            foreach (EmployeeSafetyViewModel detail in _employeeSafetyVms)
            {
                detail.PropertyChanged -= EmployeeSafetyVmPropertyChanged;
            }
        }
    }
}
