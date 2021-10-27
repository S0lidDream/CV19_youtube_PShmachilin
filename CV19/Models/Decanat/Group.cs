using System;
using System.Collections.Generic;
using System.Text;

namespace CV19.Models.Decanat
{
    internal class Group
    {
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }


    }
}
