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
     * Rename class to CreateFarmViewModel, track down all variables of this type (and FarmCropDto too) and rename them
     * Add a comment to class
     * Make class sealed
     * Add here collections of regions, farmers and agricultures
     * Add here all validation rules for new farm data
     * See comments to post method FarmController.Create
     */
    public class FarmCropViewModel
    {
        [Display(Name = "Наименование")]
        [Required(ErrorMessage = "Поле должно быть заполнено", AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Display(Name = "Фермер")]
        [Required(ErrorMessage = "Нужно выбрать фермера")]        
        public int? FarmerId { get; set; }

        [Display(Name = "Регион")]
        [Required(ErrorMessage = "Нужно выбрать регион")]
        public int? RegionId { get; set; }

        [Display(Name = "C/х культура")]
        [Required(ErrorMessage = "Нужно выбрать с/х культуру")]
        public int? AgricultureId { get; set; }
                
        [Display(Name = "Площадь")]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public double? Area { get; set; }

        [Display(Name = "Урожай за прошлый год")]        
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public double? Gather { get; set; }
    }
}