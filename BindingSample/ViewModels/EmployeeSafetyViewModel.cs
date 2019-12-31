using System;
using System.ComponentModel;
using BindingSample.Models;
using Prism.Mvvm;
using Reactive.Bindings;
using Xamarin.Forms;

namespace BindingSample.ViewModels
{
    public class EmployeeSafetyViewModel : INotifyPropertyChanged
    {
        public Employee Employee { get; private set; } = new Employee();
        public ReactiveProperty<Color> BackgroundColor { get; set; } = new ReactiveProperty<Color>();
        public ReactiveCommand DetailTapCommand { get; set; } = new ReactiveCommand();
        public DateTime EventTime { get => Employee.Safety.UpdateDateTime; }
        public string Content { get => Employee.Safety.Status.ToString(); }
        public ReactiveProperty<string> SafetyStatus { get; set; } = new ReactiveProperty<string>();

        public string CanWork
        {
            get
            {
                if (Employee.Safety.CanWork)
                {
                    return "出社可能";
                }
                return "出社不可";
            }
        }

        public string Comment { get => Employee.Safety.Remarks; }

        public EmployeeViewModel EmployeeVm { get; set; }

        public EmployeeSafetyViewModel(Employee emp)
        {
            Employee = emp;

            SetSafetyStatus();

            EmployeeVm = new EmployeeViewModel
            {
                Employee = emp,
            };
            SetBackgroundColor();

            DetailTapCommand.Subscribe(x => OnDetailTapCommand());
        }

        private void SetSafetyStatus()
        {
            string result = string.Empty;
            switch(Employee.Safety.Status){
                case Safety.EStatus.Injury:
                    result = "怪我";
                    break;
                case Safety.EStatus.Sick:
                    result = "病気";
                    break;
                default:
                    result = "無事";
                    break;
            }
            SafetyStatus.Value = result;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnDetailTapCommand()
        {
            // 状態遷移。
            switch (Employee.Safety.Status)
            {
                case Safety.EStatus.Safe:
                    Employee.Safety.Status = Safety.EStatus.Injury;
                    break;
                case Safety.EStatus.Injury:
                    Employee.Safety.Status = Safety.EStatus.Sick;
                    break;
                default:
                    Employee.Safety.Status = Safety.EStatus.Safe;
                    break;
            }

            SetSafetyStatus();

            // 状態に合わせて背景色変更。
            SetBackgroundColor();
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(BackgroundColor)));
        }

        private void SetBackgroundColor()
        {
            switch (Employee.Safety.Status)
            {
                case Safety.EStatus.Injury:
                    BackgroundColor.Value = Color.Orange;
                    break;
                case Safety.EStatus.Sick:
                    BackgroundColor.Value = Color.DarkGray;
                    break;
                default:
                    BackgroundColor.Value = Color.White;
                    break;
            }
        }
    }
}
