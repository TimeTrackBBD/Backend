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

        [ForeignKey("ProjectID")]
        public int ProjectID{get;set;}

        [ForeignKey("TaskID")]
        public int UserID{get;set;}

        [ForeignKey("StatusID")]
        public int StatusID{get;set;}
 
        [ForeignKey("PriorityID")]
        public int PriorityID{get;set;}
    }

}