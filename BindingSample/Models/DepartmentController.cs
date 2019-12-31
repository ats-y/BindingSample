using System;
using System.Collections.Generic;

namespace BindingSample.Models
{
    public class DepartmentController
    {
        public DepartmentController()
        {
        }

        public Department Get(string id)
        {
            return _testDatas.Find(x => x.Id == id);
        }

        protected List<Department> _testDatas = new List<Department>
        {
            new Department
            {
                 Id = "0001",
                 Name = "営業部",
                 Kind = Department.EKind.Sales,
                 Teams = new List<Team>()
                 {
                     new Team
                     {
                         Id = "T0001",
                         Name = "Aチーム",
                         Employees = new List<Employee>()
                         {
                            new Employee
                            {
                                Id = "E0001",
                                FamilyName = "社員",
                                GivenName = "太郎",
                                Sex = Employee.ESex.Male,
                                Safety  = new Safety
                                {
                                    UpdateDateTime = new DateTime(2019,12,30, 17,00,00),
                                    CanWork = true,
                                    Status = Safety.EStatus.Safe,
                                    Remarks = string.Empty,
                                }
                            },
                            new Employee {
                                Id = "E0002",
                                FamilyName = "従業員",
                                GivenName = "一美",
                                Sex = Employee.ESex.Female,
                                Safety  = new Safety
                                {
                                    UpdateDateTime = new DateTime(2019,12,31,8,00,00),
                                    CanWork = false,
                                    Status = Safety.EStatus.Injury,
                                    Remarks = "膝擦り剥きました。",
                                }
                            },
                         },
                     },
                     new Team
                     {
                         Id = "T0002",
                         Name =  "Bチーム",
                         Employees = new List<Employee>()
                         {
                             new Employee
                             {
                                 Id = "E0003",
                                 FamilyName = "社員",
                                 GivenName = "二郎",
                                 Sex = Employee.ESex.Male,
                                 Safety  = new Safety
                                 {
                                     UpdateDateTime = new DateTime(2019,12,31,10,0,0),
                                     CanWork = false,
                                     Status = Safety.EStatus.Sick,
                                     Remarks = "インフルです。",
                                 }
                             },
                         },
                     },
                     new Team
                     {
                         Id = "T0003",
                         Name = "Cチーム",
                         Employees = new List<Employee>()
                         {
                             new Employee
                             {
                                 Id = "E0004",
                                 FamilyName = "社員",
                                 GivenName = "みほ",
                                 Sex = Employee.ESex.Female,
                                 Safety  = new Safety
                                 {
                                     UpdateDateTime = new DateTime(2020,1,1,8,8,0),
                                     CanWork = true,
                                     Status = Safety.EStatus.Safe,
                                     Remarks = "元気です",
                                 }
                             },
                             new Employee
                             {
                                 Id = "E0005",
                                 FamilyName = "社員",
                                 GivenName = "のぞみ",
                                 Sex = Employee.ESex.Female,
                                 Safety  = new Safety
                                 {
                                     UpdateDateTime = new DateTime(2020,1,1,8,8,0),
                                     CanWork = true,
                                     Status = Safety.EStatus.Safe,
                                     Remarks = "普通です。",
                                 }
                             },
                             new Employee
                             {
                                 Id = "E0006",
                                 FamilyName = "社員",
                                 GivenName = "えみ",
                                 Sex = Employee.ESex.Female,
                                 Safety  = new Safety
                                 {
                                     UpdateDateTime = new DateTime(2020,1,1,8,8,0),
                                     CanWork = true,
                                     Status = Safety.EStatus.Safe,
                                     Remarks = "ぶじっす",
                                 }
                             }
                         }
                     }
                 }
            }
        };
    }
}
