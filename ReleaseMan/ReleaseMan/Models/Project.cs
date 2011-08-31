using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace ReleaseMan.Models
{
    public class Project
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Release> Releases { get; set; }
        public virtual ICollection<Story> Stories { get; set; }

    }


    public class ProjectDBContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Release> Releases { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<ReleaseNote> ReleaseNotes { get; set; }
        public DbSet<Story> Stories { get; set; }
    }
 

       
   
}