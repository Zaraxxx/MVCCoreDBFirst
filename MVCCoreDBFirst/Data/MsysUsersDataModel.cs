using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCoreDBFirst.Data
{
    public class MsysUsersDataModel
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string UserName { get; set; }

       
        public SiteDataModel Site { get; set; }

        public int? SiteID { get; set; }

        [Required]
        public bool blRecycle { get; set; }


    }
}
