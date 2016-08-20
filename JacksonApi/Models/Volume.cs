using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JacksonApi.Models
{
    /// <summary>
    /// A class to model each episode of the spaceman jackson series
    /// </summary>
    public class Volume
    {
        public System.Guid Id { get; set; }
        public int Number { get; set; }
        public int ActNumber { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
    }
}