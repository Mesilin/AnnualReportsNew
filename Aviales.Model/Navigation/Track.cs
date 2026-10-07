using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;

namespace Aviales.Model.Navigation
{
    [Table("Track", Schema = "Navigation")]
    [DisplayName("Трек")]
	[Serializable]
    public class Track : ModelBase
    {
        private double? length;

        [Key]
        public Guid TrackId { get; set; }
        public DateTime DateTime { get; set; }
        [MaxLength(1000)]
        public string? Comment { get; set; }
        public bool IsActive { get; set; }
        public Guid StartPointId { get; set; }
        public DateTime StartPointDate { get; set; }
        public Guid? StopPointId { get; set; }
        public DateTime? StopPointDate { get; set; }
        public Guid AbonentId { get; set; }
        public int PointsCount { get; set; }
        public double? Length
        {
            get => length;
            set
            {
                length = value;
                OnPropertyChanged();
            }
        }

        [ForeignKey("AbonentId")]
        public virtual Abonent Abonent { get; set; }

        [ForeignKey("StartPointId")]
        public virtual GpsPoint StartPoint { get; set; }

        [ForeignKey("StopPointId")]
        public virtual GpsPoint? StopPoint { get; set; }
    }
}
