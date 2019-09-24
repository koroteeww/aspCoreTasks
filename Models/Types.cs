using System;
using System.Collections.Generic;

namespace aspCoreEmpty.Models
{
    public partial class Types
    {
        public int Idtypes { get; set; }
        public string Name { get; set; }
        public int? Idcategory { get; set; }
        public Types(TypesExtended basic)
        {
            this.Idcategory = basic.Idcategory;
            this.Idtypes = basic.Idtypes;
            this.Name = basic.Name;
           
        }
        public Types()
        { }
    }
    public class TypesExtended
    {
        public int Idtypes { get; set; }
        public string Name { get; set; }
        public int? Idcategory { get; set; }
        public string NameCategory { get; set; }
        public List<string> NameCategoriesAll { get; set; }
        public TypesExtended(Types basic)
        {
            this.Idcategory = basic.Idcategory;
            this.Idtypes = basic.Idtypes;
            this.Name = basic.Name;
            NameCategoriesAll = new List<string>();
        }
        public TypesExtended()
        {
            NameCategoriesAll = new List<string>();
        }
    }
}
