using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace TimeTrackingApp.Model
{
    [Table("Project")]
    public class Project {

        [Key]
        [Column("ProjectID")]
        public int ProjectId{get;set;}

        [Column("ProjectName")]
        public required string ProjectName {get;set;}

        [Column("Description")]

        public required string Description{get;set;}

    }

}
