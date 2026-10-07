/****************************************************************************
*  Copyright (C) 2023 Инком. Все права защищены.
*
*  Файл: CulpritFire.cs
*  Автор: Анохин А.Ю.
*  Дата создания: 2023-09-12
*  Назначение: определение класса CulpritFire
****************************************************************************/

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;
using Incom.Production.Attributes;
using Incom.Production.Interfaces;

namespace Aviales.Model.ForestFire
{

    [Table("CulpritFire", Schema = "ForestFire")]
    [DisplayName("Виновник пожара")]
    [Serializable]
    [LockProduction(ProductionLockType.LockAddEmpty)]
	[AssociativeToString("Виновник пожара {AboutCulprit} по акту {DeedNumber}")]
	public partial class CulpritFire : AuditModelBase, IProductionDependent
    {
        private Guid culpritFireId;
        private string? deedNumber;
        private DateTime? deedDate;
        private string? criminalProceedingsNumber;
        private DateTime? criminalProceedingsDate;
        private string? criminalProceedingsParagraph;
        private string? appealDocumentNumber;
        private DateTime? appealDocumentDate;
        private decimal? recoveryOfDamages;
        private string? criminalCaseNumber;
        private DateTime? criminalCaseDate;
        private decimal? paymentOfDamages;
        private string? aboutCulprit;
        private string? protocolAdminResponsibilityNumber;
        private DateTime? protocolAdminResponsibilityDate;
        private bool isCriminalProceedings;
        private decimal? adminResponsibilityRecoveryOfDamages;
        private decimal? adminResponsibilityPaymentOfDamages;
        private string? adminResponsibilityAboutCulprit;

        /// <summary>
        /// Идентификатор виновника пожара
        /// </summary>
        [Key]
        [DisplayName("Идентификатор виновника пожара")]
        public Guid CulpritFireId
        {
            get => culpritFireId;
            set
            {
                culpritFireId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Идентификатор пожара
        /// </summary>
        [DisplayName("Идентификатор пожара")]
        public Guid FireId { get; set; }

        /// <summary>
        /// Номер акта о пожаре направляемый в правоохранительные органы
        /// </summary>
        [DisplayName("Номер акта о пожаре направляемый в правоохранительные органы")]
        public string? DeedNumber
        {
            get => deedNumber;
            set
            {
                deedNumber = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Дата акта
        /// </summary>
        [DisplayName("Дата акта")]
        public DateTime? DeedDate
        {
            get => deedDate;
            set
            {
                deedDate = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// № постановления об возбуждении уголовного дела
        /// </summary>
        [DisplayName("№ постановления об возбуждении уголовного дела")]
        public string? CriminalProceedingsNumber
        {
            get => criminalProceedingsNumber;
            set
            {
                criminalProceedingsNumber = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Дата постановления об возбуждении уголовного дела
        /// </summary>
        [DisplayName("Дата постановления об возбуждении уголовного дела")]
        public DateTime? CriminalProceedingsDate
        {
            get => criminalProceedingsDate;
            set
            {
                criminalProceedingsDate = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Статья/пункт постановления об возбуждении уголовного дела
        /// </summary>
        [DisplayName("Статья/пункт постановления об возбуждении уголовного дела")]
        public string? CriminalProceedingsParagraph
        {
            get => criminalProceedingsParagraph;
            set
            {
                criminalProceedingsParagraph = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Номер исходящего документа об обжаловании
        /// </summary>
        [DisplayName("Номер исходящего документа об обжаловании")]
        public string? AppealDocumentNumber
        {
            get => appealDocumentNumber;
            set
            {
                appealDocumentNumber = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Дата исходящего документа об обжаловании
        /// </summary>
        [DisplayName("Дата исходящего документа об обжаловании")]
        public DateTime? AppealDocumentDate
        {
            get => appealDocumentDate;
            set
            {
                appealDocumentDate = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Сумма взысканного ущерба (по решению суда) (в рублях)
        /// </summary>
        [DisplayName("Сумма взысканного ущерба (по решению суда) (в рублях)")]
        public decimal? RecoveryOfDamages
        {
            get => recoveryOfDamages;
            set
            {
                recoveryOfDamages = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// № уголовного дела
        /// </summary>
        [DisplayName("№ уголовного дела")]
        public string? CriminalCaseNumber
        {
            get => criminalCaseNumber;
            set
            {
                criminalCaseNumber = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Дата уголовного дела
        /// </summary>
        [DisplayName("Дата уголовного дела")]
        public DateTime? CriminalCaseDate
        {
            get => criminalCaseDate;
            set
            {
                criminalCaseDate = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Сумма оплаты ущерба (в рублях)
        /// </summary>
        [DisplayName("Сумма оплаты ущерба (в рублях)")]
        public decimal? PaymentOfDamages
        {
            get => paymentOfDamages;
            set
            {
                paymentOfDamages = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Данные виновника (реквизиты юрлица, физлица)
        /// </summary>
        [DisplayName("Данные виновника (реквизиты юрлица, физлица)")]
        public string? AboutCulprit
        {
            get => aboutCulprit;
            set
            {
                aboutCulprit = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// № протокола привлечения к админ.ответственности
        /// </summary>
        [DisplayName("№ протокола привлечения к админ.ответственности")]
        public string? ProtocolAdminResponsibilityNumber
        {
            get => protocolAdminResponsibilityNumber;
            set
            {
                protocolAdminResponsibilityNumber = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Дата протокола привлечения к админ.ответственности
        /// </summary>
        [DisplayName("Дата протокола привлечения к админ.ответственности")]
        public DateTime? ProtocolAdminResponsibilityDate
        {
            get => protocolAdminResponsibilityDate;
            set
            {
                protocolAdminResponsibilityDate = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Признак возбуждении уголовного дела
        /// </summary>
        [DisplayName("Признак возбуждении уголовного дела")]
        public bool IsCriminalProceedings
        {
            get => isCriminalProceedings;
            set
            {
                isCriminalProceedings = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Сумма взысканного ущерба (привлечение к админ. ответственности) (в рублях)
        /// </summary>
        [DisplayName("Сумма взысканного ущерба (привлечение к админ. ответственности) (в рублях)")]
        public decimal? AdminResponsibilityRecoveryOfDamages
        {
            get => adminResponsibilityRecoveryOfDamages;
            set
            {
                adminResponsibilityRecoveryOfDamages = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Сумма оплаты ущерба (привлечение к админ. ответственности) (в рублях)
        /// </summary>
        [DisplayName("Сумма оплаты ущерба (привлечение к админ. ответственности) (в рублях)")]
        public decimal? AdminResponsibilityPaymentOfDamages
        {
            get => adminResponsibilityPaymentOfDamages;
            set
            {
                adminResponsibilityPaymentOfDamages = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Данные виновника (привлечение к админ.ответственности) (реквизиты юрлица, физлица)
        /// </summary>
        [DisplayName("Данные виновника (привлечение к админ.ответственности) (реквизиты юрлица, физлица)")]
        public string? AdminResponsibilityAboutCulprit
        {
            get => adminResponsibilityAboutCulprit;
            set
            {
                adminResponsibilityAboutCulprit = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Лесной пожар
        /// </summary>
        [DisplayName("Лесной пожар")]
        [ForeignKey("FireId")]
        public virtual Fire Fire { get; set; } = null!;

        /// <summary>
        /// Идентификатор внедрения
        /// </summary>
        [DisplayName("Идентификатор внедрения")]
        public Guid? ProductionId { get; set; }

        public override string ToAuditString()
        {
            return "Виновник пожара";
        }
    }
}