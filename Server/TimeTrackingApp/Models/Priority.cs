using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace TimeTrackingApp.Model
{
    [Table("Priority")]
    public class Priority {

        [Key]
        [Column("PriorityID")]
        public int PriorityId{get;set;}

        [Column("PriorityName")]
        public required string PriorityName {get;set;}
    }

}
