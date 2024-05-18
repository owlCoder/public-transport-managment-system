﻿using Service.Database.Context;
using Service.Database.Models;
using Service.Database.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Database.CRUDOperations.AutobusCrud
{
    public class ReadAutobus : IReadOperation<Autobus>
    {
        private readonly DatabaseContext _context;
        private static readonly object _lock = new object();

        public ReadAutobus(DatabaseContext context)
        {
            _context = context;
        }

        public Autobus Read(int id)
        {
            lock (_lock)
            {
                return _context.Autobusi.AsNoTracking().FirstOrDefault(u => u.Id == id);
            }
        }

        public List<Autobus> ReadAll()
        {
            lock (_lock)
            {
                return _context.Autobusi.AsNoTracking().ToList();
            }
        }
    }
}
