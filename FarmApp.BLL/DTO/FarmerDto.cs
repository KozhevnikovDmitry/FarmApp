using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmApp.BLL.DTO
{
    /*
     * CR-1
     * Get rid of this class, it doesn't have its own meaning or purpose
     * It is used only in mapping chain Farmer => FarmerDto => NamedItemViewModel
     * Map Farmer directly to NamedItemViewModel
     */
    /// <summary>
    /// Сведения о фермере
    /// </summary>
    public class FarmerDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
