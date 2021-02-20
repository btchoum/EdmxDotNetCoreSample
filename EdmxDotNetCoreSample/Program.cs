using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Xml;

namespace EdmxDotNetCoreSample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new Entities())
            {
                db.Database.CreateIfNotExists();

                AddPerson(db);

                Console.WriteLine(db.People.Include(p=>p.Things).Count());
            }
        }

        private static void AddPerson(Entities db)
        {
            db.People.Add(new Person
            {
                Name = $"Sample Person {Guid.NewGuid()}"
            });

            db.SaveChanges();
        }
    }
}
