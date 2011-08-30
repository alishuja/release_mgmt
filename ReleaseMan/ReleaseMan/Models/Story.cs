using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace ReleaseMan.Models
{
    public class Story
    {
        [Key]
        public int ID { get; set; }
        public int ReleaseId { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Release Release { get; set; }

    }


}