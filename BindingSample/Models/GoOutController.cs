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
            public List<GoOut> GoOuts;
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
    }
}
