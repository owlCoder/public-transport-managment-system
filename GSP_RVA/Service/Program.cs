﻿using Service.Database;
using Service.Database.CRUD;
using Service.Database.CRUDOperations.AutobusCrud;
using Service.Database.CRUDOperations.LinijaCrud;
using Service.Database.CRUDOperations.LinijaCrud.FindLinija;
using Service.Database.CRUDOperations.VozacCrud;
using Service.Database.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Proxies;

namespace Service
{
    public class Program
    {
        static void Main(string[] args)
        {
            var db = DatabaseService.Instance;

            /*InsertAutobus insertAutobus = new InsertAutobus(db.Context);
            insertAutobus.Insert(new Database.Models.Autobus() { Oznaka = "sombor" });

            ReadAutobus read = new ReadAutobus(db.Context);
            var user = read.Read(1);

            UpdateAutobus update = new UpdateAutobus(db.Context);
            user.Oznaka = "Kragujevac";
            update.Update(1, user);

            DeleteAutobus delete = new DeleteAutobus(db.Context);
            delete.Delete(1);*/

            //InsertLinija insertLinija = new InsertLinija(db.Context);
            //insertLinija.Insert(new Database.Models.Linija() { Oznaka = "ko", Polaziste = "centar", Odrediste = "liman1" });

            //DeleteLinija  del = new DeleteLinija(db.Context);
            //del.Delete(2);

            //readlinija read = new readlinija(db.context);
            //var usr = read.read(3);

            //updatelinija update = new updatelinija(db.context);
            //usr.polaziste = "klisa";
            //update.update(4, usr);


            //readall vozac
            // service find
            /// FindService(IFindOperation op, string Criteria)
            /// --> findService( new FindByOdrediste(), "Detelinara");
            /// telo  FindService(IFindOperation op, string Criteria)
            /// list<linija> podaci = _context.readall();
            /// op.find(podaci, criteria);

            // Dependency injection
            //IFindOperation<Linija> findop = new FindByOdrediste();

            //findop.FindByCriteria(new List<Linija>(), "critea");

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
