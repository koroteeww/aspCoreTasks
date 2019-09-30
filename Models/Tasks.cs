using System;
using System.Collections.Generic;

namespace aspCoreEmpty.Models
{
    public partial class Tasks
    {

        public Tasks()
        { }
        public Tasks(TasksExtended basic)
        {
            this.Idtype = basic.Idtype;
            this.Idstatus = basic.Idstatus;
            this.Ispolnitel = basic.Ispolnitel;
            this.Nomer = basic.Nomer;
            this.Pomeschenie = basic.Pomeschenie;
            this.Sozdatel = basic.Sozdatel;
            this.Sozdatelemail = basic.Sozdatelemail;
            this.Datasozdaniya = basic.Datasozdaniya;
            this.Datazakritiya = basic.Datazakritiya;
            this.Chtosdelat = basic.Chtosdelat;
            this.Id = basic.Id;
            this.Foto = basic.Foto;
        }

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

    public class TasksExtended
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

        public List<string> NameTypesAll { get; set; }
        public List<string> NameStatusesAll { get; set; }

        public string NameType { get; set; }
        public string NameStatus { get; set; }
        public TasksExtended()
        {
            NameTypesAll = new List<string>();
            NameStatusesAll = new List<string>();
        }
        public TasksExtended(Tasks basic)
        {
            this.Idtype = basic.Idtype;
            this.Idstatus = basic.Idstatus;
            this.Ispolnitel = basic.Ispolnitel;
            this.Nomer = basic.Nomer;
            this.Pomeschenie = basic.Pomeschenie;
            this.Sozdatel = basic.Sozdatel;
            this.Sozdatelemail = basic.Sozdatelemail;
            this.Datasozdaniya = basic.Datasozdaniya;
            this.Datazakritiya = basic.Datazakritiya;
            this.Chtosdelat = basic.Chtosdelat;
            this.Id = basic.Id;
            this.Foto = basic.Foto;
            
            NameTypesAll = new List<string>();
            NameStatusesAll = new List<string>();
        }
    }
}
