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
                // すでに社員安否情報を保持している場合は、イベントハンドラを取り除く。
                if (_employeeSafetyVms != null)
                {
                    foreach (EmployeeSafetyViewModel detail in _employeeSafetyVms)
                    {
                        detail.PropertyChanged -= EmployeeSafetyVmPropertyChanged;
                    }
                }

                // 指定された社員安否情報を保持。
                _employeeSafetyVms = value;
                if(_employeeSafetyVms != null)
                {
                    // 社員安否情報に安否情報変化イベントハンドラを設定する。
                    foreach(EmployeeSafetyViewModel detail in _employeeSafetyVms)
                    {
                        detail.PropertyChanged += EmployeeSafetyVmPropertyChanged;
                    }

                    // 背景色を更新。
                    SetBackgroundColor();
                }
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TeamViewModel()
        {
        }

        /// <summary>
        /// 社員安否情報変更イベントハンドラ。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmployeeSafetyVmPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // 背景色を更新する。
            SetBackgroundColor();

            // EmployeeSafetyVmsプロパティが変更されたことをイベント発行で通知する。
            RaisePropertyChanged(nameof(EmployeeSafetyVms));
        }

        /// <summary>
        /// 背景色を設定する。
        /// </summary>
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

        /// <summary>
        /// チーム情報をクリアする。
        /// </summary>
        public void Clear()
        {
            foreach (EmployeeSafetyViewModel detail in _employeeSafetyVms)
            {
                detail.PropertyChanged -= EmployeeSafetyVmPropertyChanged;
            }
        }
    }
}
