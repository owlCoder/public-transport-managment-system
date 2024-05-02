﻿using Service.Database;
using Service.Database.CRUD;
using Service.Database.CRUDOperations.LinijaCrud.FindLinija;
using Service.Database.CRUDOperations.VozacCrud;
using Service.Database.Models;
using System;

namespace Service
{
    public class Program
    {
        static void Main(string[] args)
        {
            var db = DatabaseService.Instance;

            // Dependency injection
            IFindOperation<Linija> findop = new FindByOdrediste();

            findop.FindByCriteria("critea");

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
