using System;
using BindingSample.Models;

namespace BindingSample.ViewModels
{
    /// <summary>
    /// 部署ViewModel。
    /// </summary>
    public class DepartmentViewModel
    {
        /// <summary>
        /// 部署情報。
        /// </summary>
        public Department Department;

        /// <summary>
        /// 表示用部署名称。
        /// </summary>
        public string DisplayName { get => Department.Name; }

        /// <summary>
        /// 表示用部署種別。
        /// </summary>
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

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DepartmentViewModel()
        {
        }
    }
}
