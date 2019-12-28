using System;
namespace BindingSample.Models
{
    public class Employee
    {
        public string Id { get; set;  }
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public ESex Sex { get; set; }

        public enum ESex
        {
            Male,
            Female,
        }

        public Employee()
        {
        }
    }
}
