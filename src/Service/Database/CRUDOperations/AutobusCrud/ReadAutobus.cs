using Common.Enums;
using Common.Interfaces;
using Service.Database.Context;
using Service.Database.Models;
using Service.Database.Operations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Database.CRUDOperations.AutobusCrud
{
    /// <summary>
    /// Implements read operations for the Autobus entity.
    /// </summary>
    public class ReadAutobus : IReadOperation<Autobus>
    {
        private readonly DatabaseContext _context;
        private static readonly object _lock = new object();
        private static ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the ReadAutobus class with the specified DatabaseContext.
        /// </summary>
        /// <param name="context">The DatabaseContext instance to use for database operations.</param>
        /// <param name="logger">The ILogger instance to use for logging.</param>
        public ReadAutobus(DatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = Program.logger;
        }

        /// <inheritdoc/>
        public Autobus Read(int id)
        {
            try
            {
                lock (_lock)
                {
                    return _context.Autobusi.AsNoTracking().FirstOrDefault(u => u.Id == id);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.Log(LogTraceLevel.ERROR, $"Error reading Autobus with ID {id}: {ex.Message}");
                return null;
            }
        }

        /// <inheritdoc/>
        public List<Autobus> ReadAll()
        {
            try
            {
                lock (_lock)
                {
                    return _context.Autobusi.AsNoTracking().ToList();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.Log(LogTraceLevel.ERROR, $"Error reading all Autobus records: {ex.Message}");
                return new List<Autobus>();
            }
        }

        /// <inheritdoc/>
        public Autobus ReadByCriteria(Func<Autobus, bool> filter)
        {
            try
            {
                lock (_lock)
                {
                    return _context.Autobusi.AsNoTracking().FirstOrDefault(filter);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.Log(LogTraceLevel.ERROR, $"Error reading Autobus by criteria: {ex.Message}");
                return null;
            }
        }

        /// <inheritdoc/>
        public List<Autobus> ReadAllByCriteria(Func<Autobus, bool> filter)
        {
            try
            {
                lock (_lock)
                {
                    return _context.Autobusi.AsNoTracking().Where(filter).ToList();
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.Log(LogTraceLevel.ERROR, $"Error reading all Autobus records by criteria: {ex.Message}");
                return new List<Autobus>();
            }
        }
    }
}
