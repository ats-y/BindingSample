using System;
using System.Collections.Generic;

namespace BindingSample.Models
{
    public class GoOutController
    {
        public GoOutController()
        {
        }

        public class Result
        {
            public Employee Employee;
            public List<Team> Teams;
        }

        public Result Get(string employeeId)
        {
            Employee emp = EmployeeTestDatas.Find(x => x.Id == employeeId);
            if (emp == null)
            {
                return null;
            }

            return new Result
            {
                Employee = emp,
                Teams = TeamsTestDatas,
            };
        }

        protected List<Employee> EmployeeTestDatas = new List<Employee>
        {
            new Employee {
                Id = "0001",
                FamilyName = "社員",
                GivenName = "太郎",
                Sex = Employee.ESex.Male,
            },
            new Employee {
                Id = "0002",
                FamilyName = "従業員",
                GivenName = "一美",
                Sex = Employee.ESex.Female,
            },
        };

        protected List<Team> TeamsTestDatas = new List<Team>
        {
            new Team
            {
                Id = "0001",
                Name = "TeamA",
                GoOuts = new List<GoOut>()
                {
                    new GoOut
                    {
                        EventTime = new DateTime(2019,12,1,12,0,0),
                        Content = "たいとる-ひとつめ",
                        Comment = $"コメント{Environment.NewLine}改行。長い行あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめやゆよらりるれろ",
                    },
                    new GoOut
                    {
                        EventTime = new DateTime(2019,12,2,13,15,0),
                        Content = "たいとる-ふたつめ",
                        Status = GoOut.EStatus.OnHold,
                    },
                    new GoOut
                    {
                        EventTime = new DateTime(2019,12,2,14,15,0),
                        Content = "たいとる-3つめ",
                    },
                },
            },
            new Team
            {
                Id = "0002",
                Name = "TeamB",
                GoOuts = new List<GoOut>()
                {
                    new GoOut
                    {
                        EventTime = new DateTime(2019,12,31,23,59,0),
                        Content = "たいとる-ひとつめ",
                        Comment = $"コメント{Environment.NewLine}改行。長い行あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめやゆよらりるれろ",
                    },
                }
            },
            new Team
            {
                Id = "0003",
                Name = "TeamC",
                GoOuts = new List<GoOut>()
                {
                    new GoOut
                    {
                        EventTime = new DateTime(2020,1,1,0,0,0),
                        Content = "たいとる-ひとつめ",
                        Comment = $"コメント{Environment.NewLine}改行。長い行あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめやゆよらりるれろ",
                    },
                }
            },
            new Team
            {
                Id = "0004",
                Name = "TeamD",
                GoOuts = new List<GoOut>()
                {
                    new GoOut
                    {
                        EventTime = new DateTime(2020,1,8,9,00,0),
                        Content = "たいとる-ひとつめ",
                        Comment = $"コメント{Environment.NewLine}改行。長い行あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめやゆよらりるれろ",
                    },
                }
            }
        };
    }
}
