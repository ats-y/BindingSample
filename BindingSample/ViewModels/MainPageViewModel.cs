using System;
using System.Collections.Generic;
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

        internal void SetEmployeeNo(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                CheckedEmployee.Value = null;
                return;
            }

            Employee emp = Employees.Find(x => x.Id == input);
            if(emp == null)
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
    }
}
