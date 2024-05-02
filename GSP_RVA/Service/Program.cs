using Service.Database;
using Service.Database.CRUD;
using Service.Database.CRUDOperations.LinijaCrud.FindLinija;
using Service.Database.CRUDOperations.VozacCrud;
using Service.Database.Models;
using System;
using System.Collections.Generic;

namespace Service
{
    public class Program
    {
        static void Main(string[] args)
        {
            var db = DatabaseService.Instance;


            //readall vozac
            // service find
            /// FindService(IFindOperation op, string Criteria)
            /// --> findService( new FindByOdrediste(), "Detelinara");
            /// telo  FindService(IFindOperation op, string Criteria)
            /// list<linija> podaci = _context.readall();
            /// op.find(podaci, criteria);

            // Dependency injection
            IFindOperation<Linija> findop = new FindByOdrediste();

            findop.FindByCriteria(new List<Linija>(), "critea");

            //DeleteVozac  del = new DeleteVozac(db.Context);

            //del.Delete(1);
            //InsertVozac insert = new InsertVozac(db.Context);

            //insert.Insert(new Database.Models.Vozac() { Ime = "ime", Prezime = "prezime", Password = "password", Username = "ol", Role = Common.Enums.UserRole.Admin.ToString(), Oznaka = "ORR1" });

            //ReadVozac read = new ReadVozac(db.Context);

            //var usr = read.Read(1);

            //UpdateVozac update = new UpdateVozac(db.Context);

            //usr.Ime = "Olivera";

            //update.Update(1, usr);

            Console.ReadLine();
        }
    }
}
