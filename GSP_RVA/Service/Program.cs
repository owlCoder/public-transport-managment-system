using Service.Database;
using Service.Database.CRUDOperations.VozacCrud;
using System;

namespace Service
{
    public class Program
    {
        static void Main(string[] args)
        {
            var db = DatabaseService.Instance;
           
            InsertVozac insert = new InsertVozac(db.Context);

            insert.Insert(new Database.Models.Vozac() {  Ime = "ime", Prezime = "prezime", Password = "password", Username = "ol", Role = Common.Enums.UserRole.Admin, Oznaka = "ORR1" });

            ReadVozac read = new ReadVozac(db.Context);

            var usr = read.Read(1);

            Console.ReadLine();
        }
    }
}
