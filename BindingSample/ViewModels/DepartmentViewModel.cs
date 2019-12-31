using System;
using BindingSample.Models;

namespace BindingSample.ViewModels
{
    public class DepartmentViewModel
    {
        public Department Department;
        public string DisplayName { get => Department.Name; }
        public string DisplayKind { get
            {
                switch (Department.Kind)
                {
                    case Department.EKind.OfficeWork:
                        return "事務";
                    case Department.EKind.Sales:
                        return "営業";
                    case Department.EKind.Development:
                        return "開発";
                    default:
                        return "その他";
                }
            }
        }

        public DepartmentViewModel()
        {
        }
    }
}
