using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BindingSample.Models;
using Prism.Mvvm;
using Reactive.Bindings;

namespace BindingSample.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        public ReactiveProperty<EmployeeViewModel> CheckedEmployee { get; set; } = new ReactiveProperty<EmployeeViewModel>();

        protected List<Employee> Employees = new List<Employee>
        {
            new Employee {
                Id = "0001",
                FamilyName = "社員",
                GivenName = "太郎",
                Sex = Employee.ESex.Male,
            },
            new Employee {
                Id = "0002",
                FamilyName = "従業員",
                GivenName = "一美",
                Sex = Employee.ESex.Female,
            },
        };

        public MainPageViewModel()
        {

        }

        public void SetEmployeeNo(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                CheckedEmployee.Value = null;
                return;
            }

            Employee emp = Employees.Find(x => x.Id == input);
            if (emp == null)
            {
                CheckedEmployee.Value = null;
                return;
            }

            EmployeeViewModel empVm = new EmployeeViewModel
            {
                Employee = emp,
            };
            CheckedEmployee.Value = empVm;
        }

        public ObservableCollection<GoOutViewModel> GoOuts { get; set; } = new ObservableCollection<GoOutViewModel>
        {
            new GoOutViewModel
            {
                Title = "1st",
                Details = new ObservableCollection<GoOutDetaiViewModel>
                {
                    new GoOutDetaiViewModel(
                        new GoOut
                        {
                            EventTime = new DateTime(2019,12,1,12,0,0),
                            Content = "たいとる-ひとつめ",
                            Comment = $"コメント{Environment.NewLine}改行。長い行あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめやゆよらりるれろ",
                        }),
                    new GoOutDetaiViewModel(
                        new GoOut
                        {
                            EventTime = new DateTime(2019,12,2,13,15,0),
                            Content = "たいとる-ふたつめ",
                            Status = GoOut.EStatus.OnHold,
                        }),
                    new GoOutDetaiViewModel(
                        new GoOut
                        {
                            EventTime = new DateTime(2019,12,2,14,15,0),
                            Content = "たいとる-3つめ",
                        }),
                }
            },
            new GoOutViewModel
            {
                Title = "2nd",
                Details = new ObservableCollection<GoOutDetaiViewModel>
                {
                    new GoOutDetaiViewModel(
                        new GoOut
                        {
                            EventTime = new DateTime(2019,12,31,23,59,0),
                            Content = "たいとる-ひとつめ",
                            Comment = $"コメント{Environment.NewLine}改行。長い行あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめやゆよらりるれろ",
                        }),
                }
            },
            new GoOutViewModel
            {
                Title = "3rd",
                Details = new ObservableCollection<GoOutDetaiViewModel>
                {
                    new GoOutDetaiViewModel(
                        new GoOut
                        {
                            EventTime = new DateTime(2019,12,31,23,59,0),
                            Content = "たいとる-ひとつめ",
                            Comment = $"コメント{Environment.NewLine}改行。長い行あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめやゆよらりるれろ",
                        }),
                }
            },
            new GoOutViewModel
            {
                Title = "4th",
                Details = new ObservableCollection<GoOutDetaiViewModel>
                {
                    new GoOutDetaiViewModel(
                        new GoOut
                        {
                            EventTime = new DateTime(2019,12,31,23,59,0),
                            Content = "たいとる-ひとつめ",
                            Comment = $"コメント{Environment.NewLine}改行。長い行あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめやゆよらりるれろ",
                        }),
                }
            }
        };
    }
}
