using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using BindingSample.Models;
using Prism.Mvvm;
using Prism.Services;
using Reactive.Bindings;
using Xamarin.Forms;

namespace BindingSample.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        public ReactiveProperty<DepartmentViewModel> CheckedDepartment { get; set; } = new ReactiveProperty<DepartmentViewModel>();
        public ObservableCollection<TeamViewModel> CheckedTeams { get; set; } = new ObservableCollection<TeamViewModel>();

        public ICommand RegisterCommand { get; private set; }

        public MainPageViewModel(IPageDialogService dialogService)
        {
            CheckedDepartment.Value = null;

            // 登録コマンドの実装。
            RegisterCommand = new Command(
                execute: async () =>
                {
                    await dialogService.DisplayAlertAsync("たいとる", "きた", "OK");
                },
                canExecute: () =>
                {
                    return IsRegistable();
                });

            CheckedDepartment.Subscribe(x =>
            {
                ((Command)RegisterCommand).ChangeCanExecute();
            });

        }

        public void SetDepartmentNo(string input)
        {
            // ひとまず保持データを消去する。
            Clear();

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
            foreach (Team team in dept.Teams)
            {
                TeamViewModel teamVm = new TeamViewModel
                {
                    Team = team,
                    EmployeeSafetyVms = new ObservableCollection<EmployeeSafetyViewModel>(),
                };

                ObservableCollection<EmployeeSafetyViewModel> detailVm = new ObservableCollection<EmployeeSafetyViewModel>();
                foreach (Employee emp in team.Employees)
                {
                    detailVm.Add(new EmployeeSafetyViewModel(emp));
                }
                teamVm.EmployeeSafetyVms = detailVm;

                teamVm.PropertyChanged += OnTeamPropertyChanged;

                CheckedTeams.Add(teamVm);
            }
        }

        internal void SetEmployeeNo(string input)
        {
            Debug.WriteLine($"Input Employee No = [{input}]");

            foreach( TeamViewModel teamVm in CheckedTeams)
            {
                foreach( EmployeeSafetyViewModel employeeSafetyVm in teamVm.EmployeeSafetyVms)
                {
                    if( employeeSafetyVm.Employee.Id.Equals(input))
                    {
                        MessagingCenter.Send(this, MessengerKeys.SCROLL_TO_EMPLOYEE, employeeSafetyVm.Employee.Id);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// チームの状態変更通知イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTeamPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ((Command)RegisterCommand).ChangeCanExecute();
        }

        /// <summary>
        /// 登録可能な状態か判定する。
        /// </summary>
        /// <returns>true:登録可能</returns>
        protected bool IsRegistable()
        {
            foreach (TeamViewModel team in CheckedTeams)
            {
                foreach (EmployeeSafetyViewModel saftyVm in team.EmployeeSafetyVms)
                {
                    if (saftyVm.IsEditedStatus)
                    {
                        // 表示中の安否情報のうち、
                        // 一つでも安否状態が変更されたものがあれば登録可能とする。
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 保持データをクリアする。
        /// </summary>
        public void Clear()
        {
            CheckedDepartment.Value = null;
            foreach(TeamViewModel team in CheckedTeams)
            {
                team.Clear();
                team.PropertyChanged -= OnTeamPropertyChanged;
            }
            CheckedTeams.Clear();

            ((Command)RegisterCommand).ChangeCanExecute();
        }
    }
}