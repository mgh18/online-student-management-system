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
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Invitation
    {
        [Key]
        public int Sysid { get; set; }
        
        public Nullable<int> Teacher_ID { get; set; }
       
        public Nullable<int> Course_ID { get; set; }
       
        public Nullable<int> Student_ID { get; set; }
       
        public bool Invite { get; set; }
        
        public Nullable<bool> IsAccepted { get; set; }
    
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
