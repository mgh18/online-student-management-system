//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Teach
    {
        public int Sysid { get; set; }
        public Nullable<int> TeacherID { get; set; }
        public Nullable<int> CourseID { get; set; }
    
        public virtual Course Course { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
