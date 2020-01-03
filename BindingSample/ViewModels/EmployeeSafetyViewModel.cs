using System;
using System.ComponentModel;
using BindingSample.Models;
using Prism.Mvvm;
using Reactive.Bindings;
using Xamarin.Forms;

namespace BindingSample.ViewModels
{
    /// <summary>
    /// 社員安否情報ViewModel
    /// </summary>
    public class EmployeeSafetyViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 社員情報
        /// </summary>
        public Employee Employee { get; private set; } = new Employee();

        /// <summary>
        /// 背景色
        /// </summary>
        public ReactiveProperty<Color> BackgroundColor { get; set; } = new ReactiveProperty<Color>();

        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime EventTime { get => Employee.Safety.UpdateDateTime; }

        /// <summary>
        /// 安否状態文字列
        /// </summary>
        public string Content { get => Employee.Safety.Status.ToString(); }

        /// <summary>
        /// 安否状態文字列（リアクティブ版）
        /// </summary>
        public ReactiveProperty<string> SafetyStatus { get; set; } = new ReactiveProperty<string>();

        /// <summary>
        /// 安否状態変更コマンド
        /// </summary>
        public ReactiveCommand ChangeStatusCommand { get; set; } = new ReactiveCommand();

        /// <summary>
        /// プロパティ変更イベント。
        /// 本クラスはプロパティが変更されたら本イベントを呼び出す。
        /// 他クラスは本イベントのイベントハンドラを設定することで、
        /// 本クラスのプロパティが変更されたことを知ることができる。
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 出社可否文字列
        /// </summary>
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

        /// <summary>
        /// コメント
        /// </summary>
        public string Comment { get => Employee.Safety.Remarks; }

        /// <summary>
        /// 社員情報表示ViewModel
        /// </summary>
        public EmployeeViewModel EmployeeVm { get; set; }

        /// <summary>
        /// ユーザ操作で変更可能な安否状態。
        /// </summary>
        public Safety.EStatus CurrentStatus { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="emp"></param>
        public EmployeeSafetyViewModel(Employee emp)
        {
            Employee = emp;
            EmployeeVm = new EmployeeViewModel
            {
                Employee = emp,
            };

            SetSafetyStatus(emp.Safety.Status);

            // 安否状態変更コマンドの処理定義。
            ChangeStatusCommand.Subscribe(x => OnChangeStatusCommand());
        }

        /// <summary>
        /// 安否状態変更コマンド処理。
        /// </summary>
        private void OnChangeStatusCommand()
        {
            // 安否状態を遷移させる。
            Safety.EStatus newStatus;
            switch (CurrentStatus)
            {
                case Safety.EStatus.Safe:
                    // 無事　→　怪我
                    newStatus = Safety.EStatus.Injury;
                    break;
                case Safety.EStatus.Injury:
                    // 怪我　→　病気
                    newStatus = Safety.EStatus.Sick;
                    break;
                default:
                    // 上記以外は無事に遷移させる。
                    newStatus = Safety.EStatus.Safe;
                    break;
            }
            SetSafetyStatus(newStatus);

            // 安否状態を変更したことを通知する。
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(CurrentStatus)));
        }

        /// <summary>
        /// 指定の安否状態を保存する。
        /// また、その安否状態に合わせて各種プロパティを変更し、
        /// 安否状態が変更されたことを通知する。
        /// </summary>
        /// <param name="status">安否状態</param>
        private void SetSafetyStatus(Safety.EStatus status)
        {
            // 安否状態を保存し、各種プロパティを安否状態に合わせて変更する。
            CurrentStatus = status;
            switch (CurrentStatus)
            {
                case Safety.EStatus.Injury:
                    SafetyStatus.Value = "怪我";
                    BackgroundColor.Value = Color.Orange;
                    break;
                case Safety.EStatus.Sick:
                    SafetyStatus.Value = "病気";
                    BackgroundColor.Value = Color.DarkGray;
                    break;
                default:
                    SafetyStatus.Value = "無事";
                    BackgroundColor.Value = Color.White;
                    break;
            }
        }

        /// <summary>
        /// 安否状態が最初の状態から変更されているかを返す。
        /// </summary>
        public bool IsEditedStatus
        {
            get => (Employee.Safety.Status != CurrentStatus);
        }
    }
}
