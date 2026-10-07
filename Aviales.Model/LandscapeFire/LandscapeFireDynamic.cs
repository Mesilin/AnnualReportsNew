/****************************************************************************
* Copyright (C) 2021 Инком. Все права защищены.
*
* Файл: LandscapeFireDynamic.cs
* Автор: Паньков М.А.
* Дата создания: 25.10.2021
* Назначение: Определение класса LandscapeFireDynamic
****************************************************************************/

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aviales.Model.Catalog;
using Incom.Common2.Attributes;
using Incom.Common2.Persistence;
using Incom.Entity.Abstractions.Attributes;

namespace Aviales.Model.LandscapeFire
{
    [Table("LandscapeFireDynamic", Schema = "LandscapeFire")]
    [DisplayName("Динамика ландшафтного пожара")]
    [Serializable]
	[AssociativeToString("Динамика от {DynamicDate}")]
	public partial class LandscapeFireDynamic : AuditModelBase
    {
        /// <summary>
        /// Дата/Время динамики
        /// </summary>
        private DateTime dynamicDate;

        /// <summary>
        /// Состояние пожара
        /// </summary>
        private Guid dynamicStateKindId;

        /// <summary>
        /// Площадь общая
        /// </summary>
        private decimal? totalArea;

        /// <summary>
        /// Площадь ландшафтного пожара, нелесная
        /// </summary>
        private decimal? nonForestArea;

        /// <summary>
        /// Интенсивность
        /// </summary>
        private Guid? fireIntensityKindId;

        /// <summary>
        /// Причина непринятия мер
        /// </summary>
        private Guid? notLandingReasonKindId;

        /// <summary>
        /// Прыжки
        /// </summary>
        private int? jumpCount;

        /// <summary>
        /// Спуски
        /// </summary>
        private int? descentCount;

        /// <summary>
        /// Масса грузов, доставленных к месту пожара
        /// </summary>
        private decimal? cargoWeight;

        /// <summary>
        /// Масса грузов, доставленных к месту пожара авиационным транспортом
        /// </summary>
        private decimal? cargoAviaWeight;

        /// <summary>
        /// Кромка
        /// </summary>
        private decimal? fireEdgeLength;

        /// <summary>
        /// Руководитель тушения пожара
        /// </summary>
        private string? fireFightManager;

        /// <summary>
        /// Примечание
        /// </summary>
        private string? description;
        
        [Key]
        [DisplayName("Идентификатор динамики ландшафтного пожара")]
        public Guid LandscapeFireDynamicId { get; set; }

        [DisplayName("Идентификатор ландшафтного пожара")]
        public Guid LandscapeFireId { get; set; }

        [DisplayName("Дата/Время динамики")]
        public DateTime DynamicDate
        {
            get => dynamicDate;
            set
            {
                dynamicDate = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Состояние пожара")]
        public Guid DynamicStateKindId
        {
            get => dynamicStateKindId;
            set
            {
                dynamicStateKindId = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Площадь общая")]
        public decimal? TotalArea
        {
            get => totalArea;
            set
            {
                totalArea = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Площадь нелесная")]
        public decimal? NonForestArea
        {
            get => nonForestArea;
            set
            {
                nonForestArea = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Интенсивность")]
        public Guid? FireIntensityKindId
        {
            get { return fireIntensityKindId; }
            set
            {
                fireIntensityKindId = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Причина непринятия мер")]
        public Guid? NotLandingReasonKindId
        {
            get { return notLandingReasonKindId; }
            set
            {
                notLandingReasonKindId = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Прыжки")]
        public int? JumpCount
        {
            get { return jumpCount; }
            set
            {
                jumpCount = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Спуски")]
        public int? DescentCount
        {
            get { return descentCount; }
            set
            {
                descentCount = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Масса грузов, доставленных к месту пожара")]
        public decimal? CargoWeight
        {
            get { return cargoWeight; }
            set
            {
                cargoWeight = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Масса грузов, доставленных к месту пожара авиационным транспортом")]
        public decimal? CargoAviaWeight
        {
            get { return cargoAviaWeight; }
            set
            {
                cargoAviaWeight = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Кромка")]
        public decimal? FireEdgeLength
        {
            get { return fireEdgeLength; }
            set
            {
                fireEdgeLength = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Руководитель тушения пожара")]
        public string? FireFightManager
        {
            get { return fireFightManager; }
            set
            {
                fireFightManager = value;
                OnPropertyChanged();
            }
        }
        [DisplayName("Примечание")]
        public string? Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }



        [ForeignKey("DynamicStateKindId")]
        public virtual FireDynamicStateKind? DynamicStateKind { get; set; }

        [ForeignKey("LandscapeFireId")]
        public virtual LandscapeFire? LandscapeFire { get; set; }

        [ForeignKey("FireIntensityKindId")]
        public virtual FireIntensityKind? FireIntensityKind { get; set; }

        [ForeignKey("NotLandingReasonKindId")]
        public virtual NotLandingReasonKind? NotLandingReasonKind { get; set; }

        [DisplayName("Список техники")]
        [Aggregation]
        public virtual List<FightLandscapeFireResource> FightLandscapeFireResources { get; set; } = new List<FightLandscapeFireResource>();

        [DisplayName("Список людей")]
        [Aggregation]
        public virtual List<FightLandscapeFireTeam> FightLandscapeFireTeams { get; set; } = new List<FightLandscapeFireTeam>();
        public override string ToAuditString()
        {
            return $"Динамика от {DynamicDate.ToString("dd.MM.yyyy HH:mm")}";
        }
    }
}
