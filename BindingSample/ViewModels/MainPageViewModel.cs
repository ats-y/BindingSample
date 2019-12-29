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

        public ObservableCollection<GoOutViewModel> CheckedGoOuts { get; set; } = new ObservableCollection<GoOutViewModel>();

        public MainPageViewModel()
        {

        }

        public void SetEmployeeNo(string input)
        {
            // ひとまず保持データを消去する。
            CheckedEmployee.Value = null;
            CheckedGoOuts.Clear();

            // データを取得する。
            GoOutController ctrl = new GoOutController();
            GoOutController.Result result = ctrl.Get(input);
            if(result == null)
            {
                return;
            }

            // 社員データを表示するViewModelを生成する。
            CheckedEmployee.Value = new EmployeeViewModel
            {
                Employee = result.Employee,
            };

            // 一覧を表示するViewModelを生成する。
            foreach(Team team in result.Teams)
            {
                GoOutViewModel goOutVm = new GoOutViewModel
                {
                    Title = team.Name,
                    Details = new ObservableCollection<GoOutDetaiViewModel>(),
                };

                ObservableCollection<GoOutDetaiViewModel> detailVm = new ObservableCollection<GoOutDetaiViewModel>();
                foreach(GoOut goOut in team.GoOuts)
                {
                    detailVm.Add(new GoOutDetaiViewModel(goOut));
                }
                goOutVm.Details = detailVm;

                CheckedGoOuts.Add(goOutVm);
            }
        }
    }
}
