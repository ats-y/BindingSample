using System;
using System.Collections.Generic;

namespace BindingSample.Models
{
    public class Team
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<GoOut> GoOuts { get; set; }

        public Team()
        {
        }
    }
}
