using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace TimeTrackingApp.Model
{
    [Table("Status")]
    public class Status {

        [Key]
        [Column("StatusID")]
        public int StatusId{get;set;}

        [Column("StatusName")]
        public required string StatusName {get;set;}
    }

}
