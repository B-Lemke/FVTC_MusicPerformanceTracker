﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BAH.MusicPerformanceTracker.PL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MusicEntities : DbContext
    {
        public MusicEntities()
            : base("name=MusicEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblPerformance> tblPerformances { get; set; }
        public virtual DbSet<tblPerformancePiece> tblPerformancePieces { get; set; }
        public virtual DbSet<tblPieceGenre> tblPieceGenres { get; set; }
        public virtual DbSet<tblComposerType> tblComposerTypes { get; set; }
        public virtual DbSet<tblDirector> tblDirectors { get; set; }
        public virtual DbSet<tblGender> tblGenders { get; set; }
        public virtual DbSet<tblGenre> tblGenres { get; set; }
        public virtual DbSet<tblGroupMember> tblGroupMembers { get; set; }
        public virtual DbSet<tblInstrument> tblInstruments { get; set; }
        public virtual DbSet<tblLocation> tblLocations { get; set; }
        public virtual DbSet<tblPerformer> tblPerformers { get; set; }
        public virtual DbSet<tblPieceWriter> tblPieceWriters { get; set; }
        public virtual DbSet<tblRace> tblRaces { get; set; }
        public virtual DbSet<tblPiece> tblPieces { get; set; }
        public virtual DbSet<tblComposer> tblComposers { get; set; }
        public virtual DbSet<tblGroup> tblGroups { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }
        public virtual DbSet<SearchLog> SearchLogs { get; set; }
    }
}
