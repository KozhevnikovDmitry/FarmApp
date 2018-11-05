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
     * It is used only in mapping chain FarmCropViewModel => FarmCropDto => Farm
     * Map FarmCropViewModel directly to Farm
     */
    /// <summary>
    /// Информация по урожаю на ферме
    /// </summary>
    public class FarmCropDto
    {
        public string Name { get; set; }

        public int FarmerId { get; set; }

        public int RegionId { get; set; }

        public int AgricultureId { get; set; }

        public double Area { get; set; }

        public double Gather { get; set; }
    }
}
