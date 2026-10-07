using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;

namespace Aviales.Model.Navigation
{
    [Table("MarkEventType", Schema = "Navigation")]
    [DisplayName("Тип события отметки")]
	[Serializable]
    public class MarkEventType : ModelBase
    {
        private string name;
        private string? hotKey;
        private bool isEditable;

        [Key]
        public Guid MarkEventTypeId { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Name
        {
            get { return name; }
            set
            {
                name = value; 
                OnPropertyChanged();
            }
        }

        [MaxLength(30)]
        public string? HotKey
        {
            get { return hotKey; }
            set
            {
                hotKey = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Признак, определяющий сразу ли будет добавляться метка или будет предлагать её отредактировать
        /// </summary>
        public bool IsEditable
        {
            get { return isEditable; }
            set
            {
                isEditable = value;
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
    }
}
