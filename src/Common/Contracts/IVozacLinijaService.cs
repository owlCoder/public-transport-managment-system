using System.ServiceModel;

namespace Common.Contracts
{
    /// <summary>
    /// Interface defining operations for assigning and removing drivers from bus lines.
    /// </summary>
    [ServiceContract]
    public interface IVozacLinijaService
    {
        /// <summary>
        /// Assigns a driver to a bus line.
        /// </summary>
        /// <param name="vozac_id">The ID of the driver to assign.</param>
        /// <param name="linija_id">The ID of the bus line.</param>
        /// <returns>True if the driver is successfully assigned to the bus line; otherwise, false.</returns>
        [OperationContract]
        bool DodajVozacaNaLiniju(int vozac_id, int linija_id);

        /// <summary>
        /// Removes a driver from a bus line.
        /// </summary>
        /// <param name="vozac_id">The ID of the driver to remove.</param>
        /// <param name="linija_id">The ID of the bus line.</param>
        /// <returns>True if the driver is successfully removed from the bus line; otherwise, false.</returns>
        [OperationContract]
        bool UkloniVozacaNaLiniji(int vozac_id, int linija_id);
    }
}
