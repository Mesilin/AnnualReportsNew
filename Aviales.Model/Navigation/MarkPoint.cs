using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;

namespace Aviales.Model.Navigation
{
    [Table("MarkPoint", Schema = "Navigation")]
    [DisplayName("Отметка")]
	[Serializable]
    public class MarkPoint : ModelBase
    {
        [Key]
        public Guid MarkPointId { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Name { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        public Guid? AbonentId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? Created { get; set; }
        [MaxLength(200)]
        public string? CreatedBy { get; set; }
        public DateTime? Modified { get; set; }
        [MaxLength(200)]
        public string? ModifiedBy { get; set; }
        [MaxLength(200)]
        public string? Owner { get; set; }

        [ForeignKey("AbonentId")]
        public virtual Abonent? Abonent { get; set; }
    }
}
