using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BindingSample.ViewModels;
using Xamarin.Forms;

namespace BindingSample.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// 本ViewのViewModel。
        /// </summary>
        private MainPageViewModel ViewModel { get => this.BindingContext as MainPageViewModel; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // スクロールメッセージのサ購読開始。
            MessagingCenter.Subscribe<MainPageViewModel, string>(this, MessengerKeys.SCROLL_TO_EMPLOYEE
                ,async (sender, employeeId) => await ScrollToEmployeeAsync(employeeId));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            // スクロールメッセージの購読解除。
            MessagingCenter.Unsubscribe<MainPageViewModel, string>(this, MessengerKeys.SCROLL_TO_EMPLOYEE);
        }

        /// <summary>
        /// 指定した社員番号に該当するViewにスクロールする。
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        private async Task ScrollToEmployeeAsync(string employeeId)
        {
            Debug.WriteLine("ScrollToEmployee");

            Layout teamListStackLayout = TeamListViewElement.FindByName("TeamListStackLayout") as Layout;
            foreach (Layout teamView in teamListStackLayout.Children)
            {
                // teamViewは、TeamListStackLayoutのBindableLayout.ItemsSourceの子要素に対応する
                // TeamViewかTeamSingleViewの配列（teamTemplateSelectorによる）
                if (teamView.GetType() == typeof(TeamView))
                {
                    // TeamViewだったら、さらにEmpListの子要素をループ。
                    // （xamlファイルを生成して型で判定したほうがスムーズだった。。。）
                    TeamView teamMultipleView = (TeamView)teamView;
                    StackLayout empListStackLayout = teamMultipleView.FindByName("EmpList") as StackLayout;
                    if (empListStackLayout != null)
                    {
                        foreach (View emp in empListStackLayout.Children)
                        {
                            EmployeeSafetyViewModel empSafetyVm = emp.BindingContext as EmployeeSafetyViewModel;
                            if (empSafetyVm?.Employee.Id == employeeId)
                            {
                                await TeamListScrollView.ScrollToAsync(emp, ScrollToPosition.Start, true);
                                Debug.WriteLine("finished scroll.");
                                return;
                            }
                        }
                    }
                }
                else if(teamView.GetType() == typeof(TeamSingleView))
                {
                    // TeamSingleViewだったら、対応するViewModelの社員IDを比較。
                    // 一致していたらそのViewにスクロールする。
                    TeamSingleView teamSingleView = (TeamSingleView)teamView;
                    TeamViewModel teamVm = teamSingleView.BindingContext as TeamViewModel;
                    if (teamVm?.EmployeeSafetyVms[0].Employee.Id == employeeId)
                    {
                        await TeamListScrollView.ScrollToAsync(teamView, ScrollToPosition.Start, true);
                        Debug.WriteLine("finished scroll.");
                        return;
                    }
                }
                else
                {
                    // 何もしない。
                }
            }
        }

        /// <summary>
        /// 部署番号のTextChangedイベントハンドラ。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnDepartmentNoTextChanged(object sender, TextChangedEventArgs args)
        {
            string input = args.NewTextValue;
            if (input.Length == (int)Resources["DEPARTMENT_NO_LENGTH"])
            {
                ViewModel.SetDepartmentNo(input);
            }
            else
            {
                ViewModel.SetDepartmentNo(null);
            }
        }

        /// <summary>
        /// 社員番号のTextChangedイベントハンドラ。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnEmployeeNoTextChanged(object sender, TextChangedEventArgs args)
        {
            string input = args.NewTextValue;
            if (input.Length == (int)Resources["EMPLOYEE_NO_LENGTH"])
            {
                ViewModel.SetEmployeeNo(input);
            }
            else
            {
                ViewModel.SetEmployeeNo(null);
            }
        }
    }
}
