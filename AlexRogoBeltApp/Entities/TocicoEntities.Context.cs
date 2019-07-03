﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TOCICOEntities : DbContext
    {
        public TOCICOEntities()
            : base("name=TOCICOEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ActionMaster> ActionMasters { get; set; }
        public virtual DbSet<AnswerMaster> AnswerMasters { get; set; }
        public virtual DbSet<ConstraintMaster> ConstraintMasters { get; set; }
        public virtual DbSet<ControlMaster> ControlMasters { get; set; }
        public virtual DbSet<EnvironmentMaster> EnvironmentMasters { get; set; }
        public virtual DbSet<GoalMaster> GoalMasters { get; set; }
        public virtual DbSet<GoalTarget> GoalTargets { get; set; }
        public virtual DbSet<LevelMaster> LevelMasters { get; set; }
        public virtual DbSet<ProcessGoalMapping> ProcessGoalMappings { get; set; }
        public virtual DbSet<ProcessTemplateStep> ProcessTemplateSteps { get; set; }
        public virtual DbSet<QuestionMaster> QuestionMasters { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TargetMaster> TargetMasters { get; set; }
        public virtual DbSet<TransactionMaster> TransactionMasters { get; set; }
        public virtual DbSet<ProcessTemplateMaster> ProcessTemplateMasters { get; set; }
        public virtual DbSet<MemberMaster> MemberMasters { get; set; }
        public virtual DbSet<GuidMaster> GuidMasters { get; set; }
    }
}
