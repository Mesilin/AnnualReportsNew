/****************************************************************************
*  Copyright (C) 2019 Инком. Все права защищены.
*
*  Файл: AirPatrolRoutes.cs
*  Автор: Данилов А.В.
*  Дата создания: 13.01.2020
*  Назначение: Определение класса AirPatrolRoutes
****************************************************************************/

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Incom.Common2.Persistence;

namespace Aviales.Model.Flight
{
    /// <summary>
    /// Старая модель хранения информации о маршрутах.
    /// </summary>
    [Table("AirPatrolRoutes", Schema = "Route")]
    [Serializable]
    public class AirPatrolRoutes : ModelBase
    {

        #region Field

        private string route;
        private string? breakdown;

        #endregion Field

        /// <summary>
        /// Первичный ключ
        /// </summary>
        [Key]
        public Guid AirPatrolRoutesId { get; set; }

        /// <summary>
        /// Цепочка названий точек разделенных символом '-' (о да! Это был ужасный выбор! Теперь концов не найдёшь в автоматическом режиме).
        /// </summary>
        [Required]
        [MaxLength(2048)]
        public string? Route
        {
            get { return route; }
            set
            {
                route = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Наименование стартовой точки (наверное)
        /// </summary>
        [MaxLength(512)]
        public string? Breakdown
        {
            get { return breakdown; }
            set
            {
                breakdown = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Дата и время создания
        /// </summary>
        public DateTime? Created { get; set; }

        /// <summary>
        /// Геометрия в формате WKT
        /// </summary>
        public string WktGeometry { get; set; }

        ///// <summary>
        ///// В БД есть поле с геометрией 
        ///// </summary>
        //public byte[] geom { get; set; }

        /// <summary>
        /// Идентификатор внедрения
        /// </summary>
        public Guid? ProductionId
        {
            get { return productionId; }
            set
            {
                if (productionId == value)
                    return;

                productionId = value;
                OnPropertyChanged();
            }
        }
        private Guid? productionId;

        /// <summary>
        /// Год (на какой год версия маршрута актуальна).
        /// </summary>
        /// <remarks>В 2023-м на Ямале изменились маршруты. Поэтому старые помечаем как маршруты 2022 года. И заводим новые.</remarks>
        public int? Year
        {
            get => year;
            set
            {
                if (year == value)
                    return;

                year = value;
                OnPropertyChanged();
            }
        }
        private int? year;
    }
}
