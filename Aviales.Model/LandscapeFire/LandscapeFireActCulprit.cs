/****************************************************************************
* Copyright (C) 2021 Инком. Все права защищены.
*
* Файл: LandscapeFireActCulprit.cs
* Автор: Анохин А.Ю.
* Дата создания: 23.10.2023
* Назначение: Определение класса LandscapeFireActCulprit
****************************************************************************/

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.LandscapeFire
{
    [Table("LandscapeFireActCulprit", Schema = "LandscapeFire")]
    [DisplayName("Виновник пожара из акта о ландшафтном пожаре")]
    [Serializable]
	[AssociativeToString("{LastName} {MiddleName} {FirstName}")]
	public partial class LandscapeFireActCulprit : AuditModelBase
    {
        [Key]
        [DisplayName("Идентификатор виновника пожара")]
        public Guid LandscapeFireActCulpritId { get; set; }

        [DisplayName("Идентификатор акта о природном пожаре")]
        public Guid LandscapeFireActId { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [MaxLength(50)]
        [DisplayName("Имя")]
        public string? FirstName
        {
            get => firstName;
            set
            {
                if (firstName == value)
                    return;

                firstName = value;
                OnPropertyChanged();
            }
        }

        private string? firstName;

        /// <summary>
        /// Фамилия
        /// </summary>
        [MaxLength(50)]
        [DisplayName("Фамилия")]
        public string? LastName
        {
            get => lastName;
            set
            {
                if (lastName == value)
                    return;

                lastName = value;
                OnPropertyChanged();
            }
        }

        private string? lastName;

        /// <summary>
        /// Отчество
        /// </summary>
        [MaxLength(50)]
        [DisplayName("Отчество")]
        public string? MiddleName
        {
            get => middleName;
            set
            {
                if (middleName == value)
                    return;

                middleName = value;
                OnPropertyChanged();
            }
        }

        private string? middleName;

        /// <summary>
        /// Место работы
        /// </summary>
        [MaxLength(250)]
        [DisplayName("Место работы")]
        public string? WorkPlace
        {
            get => workPlace;
            set
            {
                if (workPlace == value)
                    return;

                workPlace = value;
                OnPropertyChanged();
            }
        }

        private string? workPlace;

        /// <summary>
        /// Должность
        /// </summary>
        [MaxLength(50)]
        [DisplayName("Должность")]
        public string? WorkPosition
        {
            get => workPosition;
            set
            {
                if (workPosition == value)
                    return;

                workPosition = value;
                OnPropertyChanged();
            }
        }

        private string? workPosition;

        /// <summary>
        /// Место жительства
        /// </summary>
        [MaxLength(250)]
        [DisplayName("Место жительства")]
        public string? ResidencePlace
        {
            get => residencePlace;
            set
            {
                if (residencePlace == value)
                    return;

                residencePlace = value;
                OnPropertyChanged();
            }
        }

        private string? residencePlace;

        /// <summary>
        /// Объяснение лесонарушителя, по вине которого произошел пожар
        /// </summary>
        [MaxLength(250)]
        [DisplayName("Объяснение лесонарушителя, по вине которого произошел пожар")]
        public string? ForestViolatorExplanation
        {
            get => forestViolatorExplanation;
            set
            {
                if (forestViolatorExplanation == value)
                    return;

                forestViolatorExplanation = value;
                OnPropertyChanged();
            }
        }

        private string? forestViolatorExplanation;




        ////[InverseProperty("ActCreatorPerson")]
        ////[DisplayName("Составитель акта")]
        ////public virtual ICollection<LandscapeFireAct> ActCreatorPerson { get; set; }

        ////[ForeignKey("CompilationAct")]
        ////[DisplayName("Присутствовавшие при составлении акта")]
        ////public Guid? CompilationActId { get; set; }

        ////public LandscapeFireAct CompilationAct { get; set; }

        ////[InverseProperty("DetectionFirePerson")]
        ////[DisplayName("Человек обнаруживший пожар")]
        ////public virtual ICollection<LandscapeFireAct> DetectionFirePerson { get; set; }

        ////[InverseProperty("ReportedFirePerson")]
        ////[DisplayName("Человек сообщивший о пожаре")]
        ////public virtual ICollection<LandscapeFireAct> ReportedFirePerson { get; set; }

        ////[ForeignKey("CulpritAct")]
        ////[DisplayName("Виновники пожара")]
        ////public Guid? CulpritActId { get; set; }
        ////public LandscapeFireAct CulpritAct { get; set; }

        ////[InverseProperty("AmbushPerson")]
        ////[DisplayName("Ответственное лицо за окарауливание")]
        ////public virtual ICollection<LandscapeFireAct> AmbushPerson { get; set; }

        ////[InverseProperty("FireExtinguishingManagerPerson")]
        ////[DisplayName("Руководитель тушением пожара")]
        ////public virtual ICollection<LandscapeFireAct> FireExtinguishingManagerPerson { get; set; }

        ////[InverseProperty("DepartmentRepresentativePerson")]
        ////[DisplayName("Представитель Депнедра или ДГЗН")]
        ////public virtual ICollection<LandscapeFireAct> DepartmentRepresentativePerson { get; set; }

        public override string ToAuditString() => string.Join(" ", LastName, MiddleName, FirstName);
    }
}
