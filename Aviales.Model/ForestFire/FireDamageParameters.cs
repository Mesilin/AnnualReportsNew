using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Persistence;

namespace Aviales.Model.ForestFire
{
    [Serializable]
    public abstract class FireDamageParameters : AuditModelBase
    {
        /// <summary>
        /// Величина ущерба
        /// </summary>
        [DisplayName("Величина ущерба")]
        public decimal? FireDamageValue { get; set; }

        /// <summary>
        /// Стоимость ущерба
        /// </summary>
        [DisplayName("Стоимость ущерба, тыс. руб")]
        public decimal? FireDamageCost { get; set; }

        /// <summary>
        /// Идентификатор типа ущерба
        /// </summary>
        [DisplayName("Идентификатор типа ущерба")]
        public Guid FireDamageTypeId { get; set; }

        /// <summary>
        /// Объект пожара
        /// </summary>
        [ForeignKey("FireDamageTypeId")]
        public virtual FireDamageType FireDamageType { get; set; } = null!;
    }
}
