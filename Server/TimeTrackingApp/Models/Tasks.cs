using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace TimeTrackingApp.Model
{
    [Table("Task")]
    public class Tasks {

        [Key]
        [Column("TaskID")]
        public int TaskID{get;set;}

        [Column("TaskName")]
        public required string TaskName {get;set;}

        [Column("Description")]
        public required string Description{get;set;}

    }

}
