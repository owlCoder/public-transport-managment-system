using Common.Enums;
using Service.Database;
using Service.Database.CRUDOperations.AutobusCrud;
using Service.Database.CRUDOperations.LinijaCrud;
using Service.Database.CRUDOperations.VozacCrud;
using Service.Database.Models;
using System;

namespace Service.DataProvider
{
    /// <summary>
    /// Provides methods to check and initialize initial data in the database.
    /// </summary>
    public class InitialDataCreator
    {
        /// <summary>
        /// Checks if initial data exists in the database and initializes it if necessary.
        /// </summary>
        public static void CheckIfDataExist()
        {
            try
            {
                Console.WriteLine("Initializing metadata for the database...");

                // Initialize Vozac data
                ReadVozac readVozac = new ReadVozac(DatabaseService.Instance.Context);
                InsertVozac insertVozac = new InsertVozac(DatabaseService.Instance.Context);
                var admin = readVozac.ReadByCriteria(v => v.Username == "admin" && v.Password == "admin");
                var vozac = readVozac.ReadByCriteria(v => v.Username == "vozac" && v.Password == "vozac");

                if (admin == null)
                {
                    insertVozac.Insert(new Vozac() { Username = "admin", Password = "admin", Ime = "Admin", Prezime = "Admin", Oznaka = "Admin", Role = UserRole.Admin.ToString() });
                }

                if (vozac == null)
                {
                    insertVozac.Insert(new Vozac() { Username = "vozac", Password = "vozac", Ime = "vozac", Prezime = "vozac", Oznaka = "vozac", Role = UserRole.Vozac.ToString() });
                }

                // Initialize Autobus data
                ReadAutobus readAutobus = new ReadAutobus(DatabaseService.Instance.Context);
                InsertAutobus insertAutobus = new InsertAutobus(DatabaseService.Instance.Context);

                if (readAutobus.ReadAll().Count == 0)
                {
                    insertAutobus.Insert(new Autobus() { IdLinije = 0, Oznaka = "45L" });
                    insertAutobus.Insert(new Autobus() { IdLinije = 0, Oznaka = "7L" });
                    insertAutobus.Insert(new Autobus() { IdLinije = 0, Oznaka = "15A" });
                }

                // Initialize Linija data
                ReadLinija readLinija = new ReadLinija(DatabaseService.Instance.Context);
                InsertLinija insertLinija = new InsertLinija(DatabaseService.Instance.Context);

                if (readLinija.ReadAll().Count == 0)
                {
                    for (int i = 1; i <= 4; i++)
                    {
                        insertLinija.Insert(new Linija() { Odrediste = "Liman " + i, Polaziste = "Telep", Oznaka = "L" + i });
                    }
                }

                Console.WriteLine("Initialization of metadata has been completed!");
            }
            catch (Exception e)
            {
                // Log the exception
                Program.logger.Log(LogTraceLevel.ERROR, $"Error initializing metadata: {e.Message}");
            }
        }
    }
}
