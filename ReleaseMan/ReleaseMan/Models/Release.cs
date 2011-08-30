using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace ReleaseMan.Models
{
    public class Release
    {
        [Key]
        public int ID { get; set; }
        public int ProjectId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual Project Project { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }
        public virtual ICollection<Story> Stories { get; set; }
        public virtual ICollection<ReleaseNote> Notes { get; set; }
    }


}