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
        public ReactiveProperty<DepartmentViewModel> CheckedDepartment { get; set; } = new ReactiveProperty<DepartmentViewModel>();
        public ObservableCollection<TeamViewModel> CheckedTeams { get; set; } = new ObservableCollection<TeamViewModel>();

        public MainPageViewModel()
        {
            CheckedDepartment.Value = null;
        }

        public void SetEmployeeNo(string input)
        {
            // ひとまず保持データを消去する。
            CheckedDepartment.Value = null;
            CheckedTeams.Clear();

            // データを取得する。
            DepartmentController ctrl = new DepartmentController();
            Department dept = ctrl.Get(input);
            if (dept == null)
            {
                return;
            }

            // 部署名を表示する。
            CheckedDepartment.Value = new DepartmentViewModel
            {
                Department = dept,
            };

            // 一覧を表示するViewModelを生成する。
            foreach(Team team in dept.Teams)
            {
                TeamViewModel teamVm = new TeamViewModel
                {
                    Team = team,
                    EmployeeSafetyVms = new ObservableCollection<EmployeeSafetyViewModel>(),
                };

                ObservableCollection<EmployeeSafetyViewModel> detailVm = new ObservableCollection<EmployeeSafetyViewModel>();
                foreach(Employee emp in team.Employees)
                {
                    detailVm.Add(new EmployeeSafetyViewModel(emp));
                }
                teamVm.EmployeeSafetyVms = detailVm;

                CheckedTeams.Add(teamVm);
            }
        }
    }
}
