/*
 * CR-1 - remove redundant usings below
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmApp.ViewModels
{
    /*
     * CR-1 
     * Add a comment to class and properties
     * Make class sealed
     */
    public class NamedItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}