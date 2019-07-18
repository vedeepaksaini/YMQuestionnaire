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
    
    public partial class AnswerMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AnswerMaster()
        {
            this.TransactionMasters = new HashSet<TransactionMaster>();
            this.TransactionMasters1 = new HashSet<TransactionMaster>();
        }
    
        public int ID { get; set; }
        public int QuestionID { get; set; }
        public string Answers { get; set; }
        public bool Deactive { get; set; }
        public string ErrorMSG { get; set; }
        public string ErrorAction { get; set; }
        public Nullable<int> ControlID { get; set; }
        public Nullable<int> ActionID { get; set; }
    
        public virtual ActionMaster ActionMaster { get; set; }
        public virtual ControlMaster ControlMaster { get; set; }
        public virtual QuestionMaster QuestionMaster { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionMaster> TransactionMasters { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionMaster> TransactionMasters1 { get; set; }
    }
}
