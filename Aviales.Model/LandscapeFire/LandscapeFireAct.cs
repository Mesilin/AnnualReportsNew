/****************************************************************************
* Copyright (C) 2021 Инком. Все права защищены.
*
* Файл: LandscapeFireAct.cs
* Автор: Анохин А.Ю.
* Дата создания: 01.12.2021
* Назначение: Определение класса LandscapeFireAct
****************************************************************************/

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.LandscapeFire
{
    [Table("LandscapeFireAct", Schema = "LandscapeFire")]
    [DisplayName("Акт о природном пожаре")]
    [Serializable]
	[AssociativeToString("Акт о ландшафтном пожаре \u2116 {ProtocolNumber} от {ActDate}")]
	public partial class LandscapeFireAct : AuditModelBase
    {
        [Key]
        [DisplayName("Идентификатор акта о ландшафтном пожаре")]
        public Guid LandscapeFireActId { get; set; }
        
        /// <summary>
        /// Номер акта (протокола)
        /// </summary>
        private string? protocolNumber;

        [MaxLength(150)]
        [DisplayName("Номер акта (протокола)")]
        public string? ProtocolNumber
        {
            get => protocolNumber;
            set
            {
                protocolNumber = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Дата/Время акта
        /// </summary>
        private DateTime actDate;

        [DisplayName("Дата/Время акта")]
        public DateTime ActDate
        {
            get => actDate;
            set
            {
                actDate = value;
                OnPropertyChanged();
            }
        }
        
        /// <summary>
        /// Дата/Время совершения нарушения
        /// </summary>
        private DateTime? violationDate;

        [DisplayName("Дата/Время совершения нарушения")]
        public DateTime? ViolationDate
        {
            get => violationDate;
            set
            {
                violationDate = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Описание совершения нарушения
        /// </summary>
        private string? violationDescription;

        [DisplayName("Описание совершения нарушения")]
        [MaxLength(250)]
        public string? ViolationDescription
        {
            get => violationDescription;
            set
            {
                violationDescription = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Данные, необходимые для расследования в целях выявления виновников
        /// </summary>
        private string? investigationData;

        [DisplayName("Данные, необходимые для расследования в целях выявления виновников")]
        [MaxLength(250)]
        public string? InvestigationData
        {
            get => investigationData;
            set
            {
                investigationData = value;
                OnPropertyChanged();
            }
        }


        [DisplayName("Идентификатор способа тушения пожара")]
        public Guid? LandscapeFireActExtinguishingMethodId { get; set; }

        [ForeignKey("LandscapeFireActExtinguishingMethodId")]
        public virtual LandscapeFireActExtinguishingMethod? LandscapeFireActExtinguishingMethod { get; set; }


        /// <summary>
        /// Принятые меры к окарауливанию пожара
        /// </summary>
        private string? ambushMeasures;

        [DisplayName("Принятые меры к окарауливанию пожара")]
        [MaxLength(250)]
        public string? AmbushMeasures
        {
            get => ambushMeasures;
            set
            {
                ambushMeasures = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Потери имущества
        /// </summary>
        private string? propertyLosses;

        /// <summary>
        /// Потери имущества
        /// </summary>
        /// <remarks>зданий, сооружений, машин, оборудования и др. имущества (указать наименование, количество и стоимость)</remarks>
        [DisplayName("Потери имущества")]
        [MaxLength(250)]
        public string? PropertyLosses
        {
            get => propertyLosses;
            set
            {
                propertyLosses = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Человек сообщивший о пожаре
        /// </summary>
        private string? reportedFirePerson;

        [DisplayName("Человек сообщивший о пожаре")]
        [MaxLength(250)]
        public string? ReportedFirePerson
        {
            get => reportedFirePerson;
            set
            {
                reportedFirePerson = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// На месте возникновения пожара обнаружено
        /// </summary>
        private string? landscapeFireActEvidence;

        [DisplayName("На месте возникновения пожара обнаружено")]
        [MaxLength(250)]
        public string? LandscapeFireActEvidence
        {
            get => landscapeFireActEvidence;
            set
            {
                landscapeFireActEvidence = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Список виновников пожара")]
        public virtual ICollection<LandscapeFireActCulprit> LandscapeFireActCulprits { get; set; } = new List<LandscapeFireActCulprit>();

        [DisplayName("Должностные лица")]
        public virtual ICollection<LandscapeFireActPerson> LandscapeFireActPersons { get; set; } = new List<LandscapeFireActPerson>();

        public override string ToAuditString() =>
            "Акт о природном пожаре №" + protocolNumber + " от " + ActDate.ToShortDateString();
    }
}
