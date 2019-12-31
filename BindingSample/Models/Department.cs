using System;
using System.Collections.Generic;

namespace BindingSample.Models
{
    public class Department
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public EKind Kind { get; set; }
        public List<Team> Teams { get; set; }

        public enum EKind
        {
            Development,
            Sales,
            OfficeWork,
        }

        public Department()
        {
        }
    }
}
