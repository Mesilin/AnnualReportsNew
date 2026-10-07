using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;

namespace Aviales.Model.Catalog
{

    [Table("SoilCover", Schema = "Catalog")]
	[DisplayName("Геометрия. Почвенный покров")]
	[Serializable]
	public class SoilCover : ModelBase
	{
		[Key]
		public int gid { get; set; }

		public string? soil_name { get; set;}
		
        public int river_area { get; set;}

		public string? geom { get; set; }

    }
}