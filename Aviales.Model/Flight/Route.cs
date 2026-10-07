using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;

namespace Aviales.Model.Flight
{
    [Table("Route", Schema = "Flight")]
	[DisplayName("Маршрут")]
	[Serializable]
    public class Route : ModelBase
    {
	    private string name;
	    private decimal? lenght;
	    private int? durationTime;
        private short? speed;

        [Key]
        public Guid RouteId { get; set; }

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

	    public bool IsActive { get; set; }

	    public decimal? Lenght
	    {
		    get { return lenght; }
		    set
		    {
			    lenght = value; 
			    OnPropertyChanged();
		    }
	    }

	    public int? DurationTime
	    {
		    get { return durationTime; }
		    set
		    {
			    durationTime = value; 
			    OnPropertyChanged();
		    }
	    }

        public short? Speed
		{
            get { return speed; }
            set
            {
                speed = value;
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

        public virtual ICollection<Route2Point> Route2Point { get; set; } = new Collection<Route2Point>();
    }
}
