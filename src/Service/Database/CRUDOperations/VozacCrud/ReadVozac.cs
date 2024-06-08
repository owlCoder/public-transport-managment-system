using Common.Enums;
using Common.Interfaces;
using Service.Database.Context;
using Service.Database.Models;
using Service.Database.Operations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Database.CRUDOperations.VozacCrud
{
    /// <summary>
    /// Implements read operations for the Vozac entity.
    /// </summary>
    public class ReadVozac : IReadOperation<Vozac>
    {
        private readonly DatabaseContext _context;
        private static readonly object _lock = new object();
        private static ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the ReadVozac class with the specified DatabaseContext.
        /// </summary>
        /// <param name="context">The DatabaseContext instance to use for database operations.</param>
        /// <param name="logger">The ILogger instance to use for logging.</param>
        public ReadVozac(DatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = Program.logger;
        }

        /// <inheritdoc/>
        public Vozac Read(int id)
        {
            try
            {
                // Use a lock to ensure thread safety
                lock (_lock)
                {
                    return _context.Vozaci.AsNoTracking().FirstOrDefault(u => u.Id == id);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.Log(LogTraceLevel.ERROR, $"Error reading Vozac with ID {id}: {ex.Message}");
                return null;
            }
        }

        /// <inheritdoc/>
        public List<Vozac> ReadAll()
        {
            try
            {
                // Use a lock to ensure thread safety
                lock (_lock)
                {
                    return _context.Vozaci.AsNoTracking().ToList();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.Log(LogTraceLevel.ERROR, $"Error reading all Vozac records: {ex.Message}");
                return new List<Vozac>();
            }
        }

        /// <inheritdoc/>
        public List<Vozac> ReadAllByCriteria(Func<Vozac, bool> filter)
        {
            try
            {
                // Use a lock to ensure thread safety
                lock (_lock)
                {
                    return _context.Vozaci.AsNoTracking().Where(filter).ToList();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.Log(LogTraceLevel.ERROR, $"Error reading all Vozac records by criteria: {ex.Message}");
                return new List<Vozac>();
            }
        }

        /// <inheritdoc/>
        public Vozac ReadByCriteria(Func<Vozac, bool> filter)
        {
            try
            {
                lock (_lock)
                {
                    return _context.Vozaci.AsNoTracking().FirstOrDefault(filter);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.Log(LogTraceLevel.ERROR, $"Error reading Vozac by criteria: {ex.Message}");
                return null;
            }
        }
    }
}
