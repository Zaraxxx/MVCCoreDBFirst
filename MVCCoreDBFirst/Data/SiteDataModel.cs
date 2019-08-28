using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCoreDBFirst.Data
{
    public class SiteDataModel 
    {
        [Key]
        public int ID { get; set; }


        [Required]
        [MaxLength(250)]
        public string SiteName { get; set; }

        [MaxLength(250)]
        public string SiteDescription { get; set; }

        [Required]
        public bool blRecycle { get; set; } = false;

        //public ICollection<MsysUsersDataModel> Users { get; set; }

    }
}
