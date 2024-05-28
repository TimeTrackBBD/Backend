using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace TimeTrackingApp.Model
{
    [Table("TimeEntry")]
    public class TimeEntry {

        [Key]
        [Column("TimeEntryID")]
        public int TimeEntryId{get;set;}

        [ForeignKey("TaskID")]
        public int TaskID{get;set;}

        [Column("Notes")]
        public required string Notes{get;set;}

        [Column("StartTime")]
        public required DateAndTime StartTime{get;set;}

        [Column("EndTime")]
        public required DateAndTime EndTime{get;set;}

        [Column("Duration")]
        public required DateAndTime Duration{get;set;}
    }

}

    