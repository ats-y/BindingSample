using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Windows.Input;
using BindingSample.Models;
using Prism.Mvvm;
using Prism.Services;
using Reactive.Bindings;
using Xamarin.Forms;

namespace BindingSample.ViewModels
{
    public class MainPageViewModel /*: BindableBase*/
    {
        /// <summary>
        /// 照合済み部署のViewModel。
        /// </summary>
        public ReactiveProperty<DepartmentViewModel> CheckedDepartment { get; set; } = new ReactiveProperty<DepartmentViewModel>();

        /// <summary>
        /// 照合済み部署に属するチームViewModel。
        /// </summary>
        public ObservableCollection<TeamViewModel> CheckedTeams { get; set; } = new ObservableCollection<TeamViewModel>();

        /// <summary>
        /// 登録コマンド。
        /// </summary>
        public ICommand RegisterCommand { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dialogService"></param>
        public MainPageViewModel(IPageDialogService dialogService)
        {
            // 照合済み部署はなしとする。
            CheckedDepartment.Value = null;

            // 登録コマンドの実装。
            RegisterCommand = new Command(
                execute: async () =>
                {
                    // 登録コマンドでは、ダイアログを表示する。
                    await dialogService.DisplayAlertAsync("たいとる", "きた", "OK");
                },
                canExecute: () =>
                {
                    // 登録コマンドが実行可能な条件。
                    return IsRegistable();
                });
        }

        /// <summary>
        /// 部署IDの入力。
        /// </summary>
        /// <param name="input"></param>
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

            // チームViewModel一覧を生成する。
            foreach (Team team in dept.Teams)
            {
                // まずは、チームに属する社員ViewModel一覧を生成。
                ObservableCollection<EmployeeSafetyViewModel> empSafetyVm = new ObservableCollection<EmployeeSafetyViewModel>();
                foreach (Employee emp in team.Employees)
                {
                    empSafetyVm.Add(new EmployeeSafetyViewModel(emp));
                }

                // チームViewModelを生成。
                TeamViewModel teamVm = new TeamViewModel
                {
                    Team = team,
                    EmployeeSafetyVms = empSafetyVm,
                };
                teamVm.PropertyChanged += OnTeamPropertyChanged;
                CheckedTeams.Add(teamVm);
            }
        }

        /// <summary>
        /// 社員IDの入力。
        /// </summary>
        /// <param name="input"></param>
        public void SetEmployeeNo(string input)
        {
            Debug.WriteLine($"Input Employee No = [{input}]");

            // 入力された社員IDと一致するVMを探す。
            foreach( TeamViewModel teamVm in CheckedTeams)
            {
                foreach( EmployeeSafetyViewModel employeeSafetyVm in teamVm.EmployeeSafetyVms)
                {
                    if( employeeSafetyVm.Employee.Id.Equals(input))
                    {
                        // 入力された社員IDにスクロールする。
                        // スクロールは表示に関することなので、Viewに対してメッセージを送信する。
                        Debug.WriteLine($"MessagingCenter.Send({MessengerKeys.SCROLL_TO_EMPLOYEE}) start");
                        MessagingCenter.Send(this, MessengerKeys.SCROLL_TO_EMPLOYEE, employeeSafetyVm.Employee.Id);
                        Debug.WriteLine($"MessagingCenter.Send({MessengerKeys.SCROLL_TO_EMPLOYEE}) end");
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
            // 登録コマンドの実行可否状態を更新する。
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
            // 照合済み部署をクリアする。
            CheckedDepartment.Value = null;

            // チームを全てクリアする。
            foreach(TeamViewModel team in CheckedTeams)
            {
                team.Clear();
                team.PropertyChanged -= OnTeamPropertyChanged;
            }
            CheckedTeams.Clear();

            // 登録コマンドの実行可否状態を更新する。
            ((Command)RegisterCommand).ChangeCanExecute();
        }
    }
}