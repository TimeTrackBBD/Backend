using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace TimeTrackingApp.Model
{
    [Table("ProjectTask")]
    public class ProjectTask {

        [Key]
        [Column("ProjectTaskID")]
        public int ProjectTaskId{get;set;}

        [Column("ProjectID")]
        public int ProjectID{get;set;}

        [Column("TaskID")]
        public int UserID{get;set;}

        [Column("StatusID")]
        public int StatusID{get;set;}
 
        [Column("PriorityID")]
        public int PriorityID{get;set;}
    }

}