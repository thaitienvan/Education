using System;
using System.Collections.Generic;

#nullable disable

namespace APIEducation.Models
{
    public partial class Student
    {
        public int Studentid { get; set; }
        public string Name { get; set; }
        public string Birthday { get; set; }
        public string Address { get; set; }
    }
}
