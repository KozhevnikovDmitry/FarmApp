/*
 * CR-1 - remove redundant usings - System, System.Linq, System.Text, System.Threading.Tasks
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
    /// С/х культура
    /// </summary>
    public class Agriculture
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
        /// Урожаи
        /// </summary>
        public virtual List<Crop> Crops { get; set; }

        public Agriculture()
        {
            Crops = new List<Crop>();
        }
    }
}
