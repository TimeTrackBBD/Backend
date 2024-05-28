using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace TimeTrackingApp.Model
{
    [Table("UserProject")]
    public class UserProject {

        [Key]
        [Column("UserProjectID")]
        public int UserProjectId{get;set;}
       
        [ForeignKey("UserID")]
        public int UserID{get;set;}

        [ForeignKey("ProjectID")]
        public int ProjectID{get;set;}

        [Column("StartDate")]
        public required DateAndTime StartDate {get;set;}

        [Column("EndDate")]
        public required DateAndTime EndDate {get;set;}

    }

}
