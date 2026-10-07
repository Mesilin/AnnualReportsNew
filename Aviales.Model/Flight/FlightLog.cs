using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common.Extensions;
using Incom.Common2.Persistence;

namespace Aviales.Model.Flight
{
	[Table("FlightLog", Schema = "Flight")]
	[DisplayName("Журнал полёта")]
	[Serializable]
	public class FlightLog : ModelBase
	{
		[Key] public Guid FlightLogId { get; set; }

		public DateTime DateTime
		{
			get { return dateTime; }
			set
			{
				if (dateTime.SafelyCompare(value))
					return;
				
				dateTime = value;
				OnPropertyChanged();
				
			}
		}

		[MaxLength(100)]
		public string? PlaneNumber
		{
			get { return planeNumber; }
			set
			{
				if (planeNumber != value)
				{
					planeNumber = value;
					OnPropertyChanged();
				}
			}
		}

		[MaxLength(20)]
		public string? RequestNumber
		{
			get { return requestNumber; }
			set
			{
				if (requestNumber != value)
				{
					requestNumber = value;
					OnPropertyChanged();
				}
			}
		}

		[MaxLength(100)]
		public string? PlaneCommander
		{
			get { return planeCommander; }
			set
			{
				if (planeCommander != value)
				{
					planeCommander = value;
					OnPropertyChanged();
				}
			}
		}

		[MaxLength(100)]
		public string? PilotObserver
		{
			get { return pilotObserver; }
			set
			{
				if (pilotObserver != value)
				{
					pilotObserver = value;
					OnPropertyChanged();
				}
			}
		}

		[MaxLength(100)]
		public string? CheckingMan
		{
			get { return checkingMan; }
			set
			{
				if (checkingMan != value)
				{
					checkingMan = value;
					OnPropertyChanged();
				}
			}
		}

		public int? NextFireNumber
		{
			get { return nextFireNumber; }
			set
			{
				if (nextFireNumber != value)
				{
					nextFireNumber = value;
					OnPropertyChanged();
				}
			}
		}

		public int? NextReportNumber
		{
			get { return nextReportNumber; }
			set
			{
				if (nextReportNumber != value)
				{
					nextReportNumber = value;
					OnPropertyChanged();
				}
			}
		}

		public short? Kpo
		{
			get { return kpo; }
			set
			{
				if (kpo != value)
				{
					kpo = value;
					OnPropertyChanged();
				}
			}
		}

		public decimal? Altitude
		{
			get { return altitude; }
			set
			{
				if (altitude != value)
				{
					altitude = value;
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

		public decimal? SafeAltitude
		{
			get { return safeAltitude; }
			set
			{
				if (safeAltitude != value)
				{
					safeAltitude = value;
					OnPropertyChanged();
				}
			}
		}

		public Guid? AirbaseId
		{
			get { return airbaseId; }
			set
			{
				if (airbaseId != value)
				{
					airbaseId = value;
					OnPropertyChanged();
				}
			}
		}

		public Guid? AirbaseDepartmentId
		{
			get { return airbaseDepartmentId; }
			set
			{
				if (airbaseDepartmentId != value)
				{
					airbaseDepartmentId = value;
					OnPropertyChanged();
				}
			}
		}

		public Guid? AircraftModelId
		{
			get { return aircraftModelId; }
			set
			{
				if (aircraftModelId != value)
				{
					aircraftModelId = value;
					OnPropertyChanged();
				}
			}
		}

		public Guid RouteId
		{
			get { return routeId; }
			set
			{
				if (routeId != value)
				{
					routeId = value;
					OnPropertyChanged();
				}
			}
		}

		public Guid? FireFlightId
		{
			get { return fireFlightId; }
			set
			{
				if (fireFlightId != value)
				{
					fireFlightId = value;
					OnPropertyChanged();
				}
			}
		}

		public bool IsDeleted { get; set; }
		public System.DateTime? Created { get; set; }
		[MaxLength(200)] public string? CreatedBy { get; set; }
		public System.DateTime? Modified { get; set; }
		[MaxLength(200)] public string? ModifiedBy { get; set; }
		[MaxLength(200)] public string? Owner { get; set; }

		[ForeignKey("AirbaseId")] public Airbase Airbase { get; set; }
		[ForeignKey("AirbaseDepartmentId")] public AirbaseDepartment AirbaseDepartment { get; set; }
		[ForeignKey("AircraftModelId")] public AircraftModel AircraftModel { get; set; }
		[ForeignKey("RouteId")] public Route Route { get; set; }
		[ForeignKey("FireFlightId")] public FireFlight FireFlight { get; set; }

		#region private field

		private int? windDirection;
		private int? pressure;
		private decimal? windSpeed;
		private DateTime dateTime;
		private string? planeNumber;
		private string? requestNumber;
		private string? planeCommander;
		private string? pilotObserver;
		private string? checkingMan;
		private int? nextFireNumber;
		private int? nextReportNumber;
		private short? kpo;
		private decimal? altitude;
		public decimal? safeAltitude;
		public Guid? airbaseId;
		public Guid? airbaseDepartmentId;
		public Guid? aircraftModelId;
		public Guid routeId;
		public Guid? fireFlightId;


		#endregion
	}
}