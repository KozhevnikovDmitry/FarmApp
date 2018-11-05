/*
 * CR-1 - remove redundant usings below
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmApp.DAL
{
    /*
     * CR-1
     * Move entity classes to FarmApp.BLL assembly to namespase FarmApp.BLL.Entities
     * Make FarmApp.DAL refer on FarmApp.BLL
     */
    /// <summary>
    /// Ферма
    /// </summary>
    public class Farm
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Площадь
        /// </summary>
        public double Area { get; set; }

        /// <summary>
        /// Id фермера
        /// </summary>
        public int FarmerId { get; set; }

        /// <summary>
        /// Id региона
        /// </summary>
        public int RegionId { get; set; }

        /// <summary>
        /// Фермер-владелец
        /// </summary>
        public virtual Farmer Owner { get; set; }

        /// <summary>
        /// Регион-расположение
        /// </summary>
        public virtual Region Location { get; set; }

        /*
         * CR-1 - add crops collection here
         */
    }
}
