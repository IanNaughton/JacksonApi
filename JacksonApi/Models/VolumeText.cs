using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JacksonApi.Models
{
    public partial class VolumeText
    {
        public System.Guid Id { get; set; }
        public System.Guid VolumeId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
    }
}