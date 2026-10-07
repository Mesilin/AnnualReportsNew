/****************************************************************************
* Copyright (C) 2023 Инком. Все права защищены.
*
* Файл: AnnualReportSetting.cs
* Автор: Анохин А.Ю.
* Дата создания: 2023-11-16
* Назначение: Определение класса AnnualReportSetting
****************************************************************************/

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.Aviales
{
    [Table("AnnualReportSetting", Schema = "Aviales")]
    [DisplayName("Настройки годовой отчётности")]
    [Serializable]
	[AssociativeToString("Настройки годовой отчетности от {ReportingDay} {ReportingMonth}")]
	public class AnnualReportSetting : AuditModelBase
    {
        [Key]
        [DisplayName("Идентификатор настройки годовой отчётности")]
        public Guid AnnualReportSettingId { get; set; }

        /// <summary>
        /// Дата подачи отчётности. День
        /// </summary>
        private byte submissionDay;
        
        /// <summary>
        /// Дата подачи отчётности. День
        /// </summary>
        [DisplayName("Дата подачи отчётности. День")]
        public byte SubmissionDay
        {
            get => submissionDay;
            set => submissionDay = value;
        }

        /// <summary>
        /// Дата подачи отчётности. Месяц
        /// </summary>
        private byte submissionMonth;

        /// <summary>
        /// Дата подачи отчётности. Месяц
        /// </summary>
        [DisplayName("Дата подачи отчётности. Месяц")]
        public byte SubmissionMonth
        {
            get => submissionMonth;
            set => submissionMonth = value;
        }

        /// <summary>
        /// Дата формирования отчётности. День
        /// </summary>
        private byte reportingDay;

        /// <summary>
        /// Дата формирования отчётности. День
        /// </summary>
        [DisplayName("Дата формирования отчётности. День")]
        public byte ReportingDay
        {
            get => reportingDay;
            set => reportingDay = value;
        }

        /// <summary>
        /// Дата формирования отчётности. Месяц
        /// </summary>
        private byte reportingMonth;

        /// <summary>
        /// Дата формирования отчётности. Месяц
        /// </summary>
        [DisplayName("Дата формирования отчётности. Месяц")]
        public byte ReportingMonth
        {
            get => reportingMonth;
            set => reportingMonth = value;
        }

    }
}
