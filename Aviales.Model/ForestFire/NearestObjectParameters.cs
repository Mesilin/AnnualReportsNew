using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Persistence;

namespace Aviales.Model.ForestFire
{
    [Serializable]
    public abstract class NearestObjectParameters : AuditModelBase
    {
        /// <summary>
        /// Идентификатор объекта
        /// </summary>
        [DisplayName("Идентификатор объекта")]
        public Guid? ObjectId { get; set; }

        /// <summary>
        /// Идентификатор типа важного объекта
        /// </summary>
        [DisplayName("Идентификатор типа важного объекта")]
        public Guid ImportantObjectTypeId { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        [DisplayName("Тип")]
        public short? Type { get; set; }

        /// <summary>
        /// Широта
        /// </summary>
        [DisplayName("Широта")]
        public double Latitude { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        [DisplayName("Долгота")]
        public double Longitude { get; set; }

        /// <summary>
        /// Расстояние до объекта от пожара
        /// </summary>
        [DisplayName("Расстояние до объекта")]
        public double Distance { get; set; }

        /// <summary>
        /// Азимут для пожара по направлению к настоящему объекту
        /// </summary>
        [DisplayName("Азимут для пожара")]
        public double Azimuth { get; set; }

        /// <summary>
        /// Тип важных объектов
        /// </summary>
        [ForeignKey("ImportantObjectTypeId")]
        public virtual ImportantObjectType ImportantObjectType { get; set; } = null!;
    }
}
