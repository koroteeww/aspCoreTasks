using System;
using System.Collections.Generic;

namespace aspCoreEmpty.Models
{
    public partial class Types
    {
        public int Idtypes { get; set; }
        public string Name { get; set; }
        public int? Idcategory { get; set; }
        
    }
    public class TypesExtended
    {
        public int Idtypes { get; set; }
        public string Name { get; set; }
        public int? Idcategory { get; set; }
        public string NameCategory { get; set; }
    }
}
