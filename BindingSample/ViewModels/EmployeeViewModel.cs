using System;
using BindingSample.Models;
using Prism.Mvvm;
using Xamarin.Forms;

namespace BindingSample.ViewModels
{
    public class EmployeeViewModel /*: BindableBase*/
    {
        public Employee Employee { get; set; }

        public EmployeeViewModel()
        {
        }

        public string DisplayName
        {
            get => $"{Employee.FamilyName} {Employee.GivenName}";
        }

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
