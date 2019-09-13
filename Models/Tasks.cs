using System;
using System.Collections.Generic;

namespace aspCoreEmpty.Models
{
    public partial class Tasks
    {
        public int Id { get; set; }
        public int? Nomer { get; set; }
        public DateTime? Datasozdaniya { get; set; }
        public DateTime? Datazakritiya { get; set; }
        public string Pomeschenie { get; set; }
        public int? Idtype { get; set; }
        public int? Idstatus { get; set; }
        public string Ispolnitel { get; set; }
        public string Sozdatel { get; set; }
        public string Sozdatelemail { get; set; }
        public string Chtosdelat { get; set; }
        public byte[] Foto { get; set; }
    }
}
