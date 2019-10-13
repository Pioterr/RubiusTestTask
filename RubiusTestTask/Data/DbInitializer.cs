using RubiusTestTask.Models;
using System;
using System.Linq;
using RubiusTestTask.Models;

namespace RubiusTestTask.Data
{
    public static class DbInitializer
    {
        public static void Initialize(RecordContext context)
        {
            context.Database.EnsureCreated();

            // Look for any record.
            if (context.Records.Any())
            {
                return;   // DB has been seeded
            }

            var records = new Record[]
            {
                new Record {Comments = "comment1", Project = "ProjectA", Date = DateTime.Parse("2001-08-01")},
                new Record {Comments = "comment2", Project = "ProjectB", Date = DateTime.Parse("2017-09-01")},
                new Record {Comments = "comment3", Project = "ProjectC", Date = DateTime.Parse("2013-06-01")},
                new Record {Comments = "comment4", Project = "ProjectD", Date = DateTime.Parse("2007-06-02")},
                new Record {Comments = "comment5", Project = "ProjectE", Date = DateTime.Parse("2016-05-08")},
                new Record {Comments = "comment6", Project = "ProjectA", Date = DateTime.Parse("2000-07-30")},
                new Record {Comments = "comment7", Project = "ProjectA", Date = DateTime.Parse("2012-03-21")},
                new Record {Comments = "comment8", Project = "ProjectD", Date = DateTime.Parse("2017-01-12")}
            };
            foreach (Record s in records)
            {
                context.Records.Add(s);
            }
            context.SaveChanges();

           
        }
    }
}