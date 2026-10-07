using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;

namespace Aviales.Model.Flight
{
    [Table("Route2Point", Schema = "Flight")]
    [DisplayName("Связка маршрута и точки маршрута")]
    [Serializable]
    public class Route2Point : ModelBase
    {
        [Key]
        public Guid Route2PointId { get; set; }

        public Guid RouteId { get; set; }
        public Guid RoutePointId { get; set; }
        public short Number { get; set; }

        [ForeignKey("RouteId")]
        public Route Route { get; set; }
        [ForeignKey("RoutePointId")]
        public RoutePoint RoutePoint { get; set; }
    }
}
