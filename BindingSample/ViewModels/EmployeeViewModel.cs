using System;
using BindingSample.Models;
using Prism.Mvvm;
using Xamarin.Forms;

namespace BindingSample.ViewModels
{
    /// <summary>
    /// 社員情報表示ViewModel。
    /// </summary>
    public class EmployeeViewModel
    {
        /// <summary>
        /// 対象社員情報。
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public EmployeeViewModel()
        {
        }

        /// <summary>
        /// 表示用社員名。
        /// </summary>
        public string DisplayName
        {
            get => $"{Employee.FamilyName} {Employee.GivenName}";
        }

        /// <summary>
        /// 表示用性別。
        /// </summary>
        public string DisplaySex
        {
            get
            {
                switch (Employee.Sex)
                {
                    case Employee.ESex.Male:
                        return "男性";
                    case Employee.ESex.Female:
                        return "女性";
                }
                return "不明";
            }
        }

        /// <summary>
        /// 性別色。
        /// </summary>
        public Color SexColor
        {
            get
            {
                switch (Employee.Sex)
                {
                    case Employee.ESex.Male:
                        return Color.Blue;
                    case Employee.ESex.Female:
                        return Color.DarkRed;
                }
                return Color.Black;
            }
        }
    }
}
