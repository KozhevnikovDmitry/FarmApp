/*
 * CR-1 - remove redundant usings: System, System.Collections.Generic, System.Linq, System.Web
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarmApp.ViewModels
{
    /*
     * CR-1 
     * Add a comment to class
     * Make class sealed
     */
    public class FarmViewModel
	{
		public int Id { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }
	    
	    /*
         * CR-1 - remove unused property
         */
        public int FarmerId { get; set; }

        [Display(Name = "Имя фермера")]
        public string FarmerName { get; set; }

	    /*
         * CR-1 - remove unused property
         */
        public int RegionId { get; set; }

        [Display(Name = "Регион")]
        public string RegionName { get; set; }

        [Display(Name = "Площадь")]
        public double Area { get; set; }
	}
}