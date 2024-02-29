using System.ComponentModel.DataAnnotations.Schema;

namespace CropMeasureEligibility.Editor.Models
{
    public class CropMeasureEligibilityDto
    {
        /// <summary>
        /// Rezultat izračuna prava na mjere.
        /// Svaki CropRequestItem (Key, Guid) ima popis svih mjera na koje ima pravo.
        /// </summary>
        public Dictionary<Guid, List<ArkodCropMeasureEligibilityDto>> ArkodCropMeasureEligibilities { get; set; } = new();

        /// <summary>
        /// Popis svih MeasureDefinition-a na koje ima pravo.
        /// </summary>
        public IList<int> EligibileMeasureDefinitions { get; init; }

        /// <summary>
        /// Popis svih deklaracija na koje ima pravo.
        /// Value flag označava je li deklaracija označena ili ne.
        /// </summary>
        public Dictionary<int, bool> EligibleDeclarations { get; set; } = new();

        /// <summary>
        /// Svaki CropRequestItem (Key, Guid) ima dictionary svih mjera na koje ima pravo sa flagovima je li tražena potpora ili ne.
        /// Ako je vrijednost "true" znači da je označena potpora.
        /// Ako je vrijednost "false" znači da je označena deklaracija, ali odznačena potpora.
        /// Ako je vrijednost "null" znači da ima pravo na tu mjeru, ali nije označena deklaracija.
        /// </summary>
        public Dictionary<Guid, Dictionary<int, bool?>> ItemMeasures { get; set; } = new();

        /// <summary>
        /// Popis svih dodataka deklaracijama + deklaracije koje ne ovise o izračunu prava na mjere
        /// Izjave, deklaracije za mlade i male poljoprivrednike + maslinovo ulje i duhan
        /// </summary>
        public Dictionary<int, bool> DeclarationAdditions { get; set; } = new();
    }
}
