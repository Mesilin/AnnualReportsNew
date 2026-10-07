using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;

namespace Aviales.Model.Flight
{
    [Table("RoutePoint", Schema = "Flight")]
    [DisplayName("Точка маршрута")]
	[Serializable]
    public class RoutePoint : ModelBase
    {
        private string name;
        private decimal? latitude;
        private decimal? longitude;
        private decimal? magneticDeclination;
        private bool isAirField;
        private decimal? excessKta;
        private decimal? airstripLenght;
        private string? airstripCover;
        private string? callDprm;
        private short? frequencyDprm;
        private decimal? startCourse;
        private string latitudePartial;
        private string longitutePartial;

        [Key]
        public Guid RoutePointId { get; set; }

        [Required]
        [MaxLength(300)]
        public string? Name
        {
            get { return name; }
            set
            {
                name = value; 
                OnPropertyChanged();
            }
        }

        public decimal? Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                OnPropertyChanged();
            }
        }

        public decimal? Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                OnPropertyChanged();
            }
        }

        public decimal? MagneticDeclination
        {
            get { return magneticDeclination; }
            set
            {
                magneticDeclination = value;
                OnPropertyChanged();
            }
        }

        public bool IsAirField
        {
            get { return isAirField; }
            set
            {
                isAirField = value;
                OnPropertyChanged();
            }
        }

        public decimal? ExcessKTA
        {
            get { return excessKta; }
            set
            {
                excessKta = value;
                OnPropertyChanged();
            }
        }

        public decimal? AirstripLenght
        {
            get { return airstripLenght; }
            set
            {
                airstripLenght = value;
                OnPropertyChanged();
            }
        }

        [MaxLength(100)]
        public string? AirstripCover
        {
            get { return airstripCover; }
            set
            {
                airstripCover = value;
                OnPropertyChanged();
            }
        }

        [MaxLength(10)]
        public string? CallDPRM
        {
            get { return callDprm; }
            set
            {
                callDprm = value;
                OnPropertyChanged();
            }
        }

        public short? FrequencyDPRM
        {
            get { return frequencyDprm; }
            set
            {
                frequencyDprm = value;
                OnPropertyChanged();
            }
        }

        public decimal? StartCourse
        {
            get { return startCourse; }
            set
            {
                startCourse = value;
                OnPropertyChanged();
            }
        }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime? Created { get; set; }
        [MaxLength(200)]
        public string? CreatedBy { get; set; }
        public System.DateTime? Modified { get; set; }
        [MaxLength(200)]
        public string? ModifiedBy { get; set; }
        [MaxLength(200)]
        public string? Owner { get; set; }

        [NotMapped]
        public string? LatitudePartial
        {
            get => latitudePartial;
            set
            {
                latitudePartial = value;
                OnPropertyChanged();
            }
        }

        [NotMapped]
        public string? LongitutePartial
        {
            get => longitutePartial;
            set
            {
                longitutePartial = value;
                OnPropertyChanged();
            }
        }
    }
}
