using Common.Enums;
using Common.Interfaces;
using Service.Database.Context;
using Service.Database.Models;
using Service.Database.Operations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Database.CRUDOperations.LinijaCrud
{
    /// <summary>
    /// Implements read operations for the Linija entity.
    /// </summary>
    public class ReadLinija : IReadOperation<Linija>
    {
        private readonly DatabaseContext _context;
        private static readonly object _lock = new object();
        private static ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the ReadLinija class with the specified DatabaseContext.
        /// </summary>
        /// <param name="context">The DatabaseContext instance to use for database operations.</param>
        /// <param name="logger">The ILogger instance to use for logging.</param>
        public ReadLinija(DatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = Program.logger;
        }

        /// <inheritdoc/>
        public Linija Read(int id)
        {
            try
            {
                lock (_lock)
                {
                    return _context.Linije.AsNoTracking().FirstOrDefault(u => u.Id == id);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.Log(LogTraceLevel.ERROR, $"Error reading Linija with ID {id}: {ex.Message}");
                return null;
            }
        }

        /// <inheritdoc/>
        public List<Linija> ReadAll()
        {
            try
            {
                lock (_lock)
                {
                    return _context.Linije.AsNoTracking().ToList();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.Log(LogTraceLevel.ERROR, $"Error reading all Linija records: {ex.Message}");
                return new List<Linija>();
            }
        }

        /// <inheritdoc/>
        public Linija ReadByCriteria(Func<Linija, bool> filter)
        {
            try
            {
                lock (_lock)
                {
                    return _context.Linije.AsNoTracking().FirstOrDefault(filter);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.Log(LogTraceLevel.ERROR, $"Error reading Linija by criteria: {ex.Message}");
                return null;
            }
        }

        /// <inheritdoc/>
        public List<Linija> ReadAllByCriteria(Func<Linija, bool> filter)
        {
            try
            {
                lock (_lock)
                {
                    return _context.Linije.AsNoTracking().Where(filter).ToList();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.Log(LogTraceLevel.ERROR, $"Error reading all Linija records by criteria: {ex.Message}");
                return new List<Linija>();
            }
        }
    }
}
