//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AlexRogoBeltApp.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class MemberMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MemberMaster()
        {
            this.GoalTargets = new HashSet<GoalTarget>();
            this.ProcessGoalMappings = new HashSet<ProcessGoalMapping>();
            this.TransactionMasters = new HashSet<TransactionMaster>();
        }
    
        public int MemberID { get; set; }
        public string MemberUserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public int NoOfEmployed { get; set; }
        public Nullable<decimal> AnnualRevenue { get; set; }
        public string BusinessEnvironment { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoalTarget> GoalTargets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProcessGoalMapping> ProcessGoalMappings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionMaster> TransactionMasters { get; set; }
    }
}
