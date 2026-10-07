using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common.Extensions;
using Incom.Common2.Persistence;

namespace Aviales.Model.Flight
{
	[Table("FlightLogLandingPoint", Schema = "Flight")]
    [DisplayName("Посадочные пункты из бортжурнала")]
    [Serializable]
	public class FlightLogLandingPoint : ModelBase
	{
		[Key] 
        public Guid FlightLogLandingPointId { get; set; }

		public DateTime? SunRise
		{
			get { return sunRise; }
			set
			{
				if (sunRise.SafelyCompare(value))
					return;
				
				sunRise = value;
				OnPropertyChanged();
			}
		}

		public DateTime? SunSet
		{
			get { return sunSet; }
			set
			{
				if (sunSet.SafelyCompare(value))
					return;
				
				sunSet = value;
				OnPropertyChanged();
				
			}
		}

		public double? Temperature
		{
			get { return temperature; }
			set
			{
				if (!temperature.Equals(value))
				{
					temperature = value;
					OnPropertyChanged();
				}
			}
		}

		public int? Pressure
		{
			get { return pressure; }
			set
			{
				if (pressure != value)
				{
					pressure = value;
					OnPropertyChanged();
				}
			}
		}

		public int? WindDirection
		{
			get { return windDirection; }
			set
			{
				if (windDirection != value)
				{
					windDirection = value;
					OnPropertyChanged();
				}
			}
		}

		public decimal? WindSpeed
		{
			get { return windSpeed; }
			set
			{
				if (windSpeed != value)
				{
					windSpeed = value;
					OnPropertyChanged();
				}
			}
		}

		public Guid RoutePointId { get; set; }
		public Guid FlightLogId { get; set; }

		public short? Number
		{
			get { return number; }
			set
			{
				if (number != value)
				{
					number = value;
					OnPropertyChanged();
				}
			}
		}

		public bool IsDeleted { get; set; }
		public DateTime? Created { get; set; }
		[MaxLength(200)] public string? CreatedBy { get; set; }
		public DateTime? Modified { get; set; }
		[MaxLength(200)] public string? ModifiedBy { get; set; }
		[MaxLength(200)] public string? Owner { get; set; }

		[ForeignKey("RoutePointId")] public RoutePoint RoutePoint { get; set; }
		[ForeignKey("FlightLogId")] public FlightLog FlightLog { get; set; }

		#region private field

		private int? windDirection;
		private int? pressure;
		private decimal? windSpeed;
		private short? number;
		public double? temperature;
		private DateTime? sunRise { get; set; }
		private DateTime? sunSet { get; set; }

		#endregion
	}
}