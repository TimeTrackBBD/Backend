using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace TimeTrackingApp.Model
{
    [Table("User")]
    public class User {

        [Key]
        [Column("UserID")]
        public int UserId{get;set;}

        [Column("UserName")]
        public required string UserName {get;set;}

        [Column("PasswordHash")]
        public required string PasswordHash { get; set; }

    }

}
