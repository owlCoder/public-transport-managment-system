using Common.DTO;
using System.Collections.Generic;
using System.ServiceModel;

namespace Common.Interfaces
{
    /// <summary>
    /// Interface defining operations for managing autobuses.
    /// </summary>
    [ServiceContract]
    public interface IAutobusService
    {
        /// <summary>
        /// Adds a new autobus with the specified label.
        /// </summary>
        /// <param name="oznaka">The label of the autobus.</param>
        /// <returns>True if the autobus is successfully added; otherwise, false.</returns>
        [OperationContract]
        bool DodajAutobus(string oznaka);

        /// <summary>
        /// Updates an existing autobus with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the autobus to update.</param>
        /// <param name="data">The data to update the autobus with.</param>
        /// <returns>True if the autobus is successfully updated; otherwise, false.</returns>
        [OperationContract]
        bool IzmeniAutobus(int id, AutobusDTO data);

        /// <summary>
        /// Deletes an existing autobus with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the autobus to delete.</param>
        /// <returns>True if the autobus is successfully deleted; otherwise, false.</returns>
        [OperationContract]
        bool ObrisiAutobus(int id);

        /// <summary>
        /// Retrieves the autobus with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the autobus to retrieve.</param>
        /// <returns>The autobus with the specified ID.</returns>
        [OperationContract]
        AutobusDTO Procitaj(int id);

        /// <summary>
        /// Retrieves all autobuses.
        /// </summary>
        /// <returns>A list of all autobuses.</returns>
        [OperationContract]
        List<AutobusDTO> ProcitajSve();
    }
}
