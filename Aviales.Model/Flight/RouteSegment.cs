using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;

namespace Aviales.Model.Flight
{
    [Table("RouteSegment", Schema = "Flight")]
    [DisplayName("Сегмент маршрута")]
	[Serializable]
		public partial class RouteSegment : ModelBase
    {
			//partial сделан для yasen-mobile
	    private decimal? terrainHeight;

        [Key]
        public Guid RouteSegmentId { get; set; }
        public Guid BeginRoutePointId { get; set; }
        public Guid EndRoutePointId { get; set; }
        public decimal? MagneticAngle { get; set; }
        public decimal? Distance { get; set; }

        public decimal? TerrainHeight
        {
            get { return terrainHeight; }
            set
            {
                terrainHeight = value;
                OnPropertyChanged();
            }
        }

        public bool IsDeleted { get; set; }
        public System.DateTime? Created { get; set; }
        [MaxLength(200)]
        public string? CreatedBy { get; set; }
        public System.DateTime? Modified { get; set; }
        [MaxLength(200)]
        public string? ModifiedBy { get; set; }
        [MaxLength(200)]
        public string? Owner { get; set; }

        [ForeignKey("BeginRoutePointId")]
        public RoutePoint BeginRoutePoint { get; set; }
        [ForeignKey("EndRoutePointId")]
        public RoutePoint EndRoutePoint { get; set; }
    }
}
