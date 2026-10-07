/****************************************************************************
* Copyright (C) 2021 Инком. Все права защищены.
*
* Файл: LandscapeFireActPersonType.cs
* Автор: Анохин А.Ю.
* Дата создания: 23.10.2023
* Назначение: Определение класса LandscapeFireActPersonType
****************************************************************************/

using Incom.Common.Collections;

namespace Aviales.Model.Enums
{
    /// <summary>
    /// Типы должностного лица
    /// </summary>
    public enum LandscapeFireActPersonType
    {
        /// <summary>
        /// Тип должностного лица не определён
        /// </summary>
        [EnumDisplayName("Тип должностного лица не определён")]
        Undefined,
        /// <summary>
        /// Представитель Депнедра или ДГЗН
        /// </summary>
        [EnumDisplayName("Представитель Депнедра или ДГЗН")]
        DepartmentRepresentative,
        /// <summary>
        /// Составитель акта
        /// </summary>
        [EnumDisplayName("Составитель акта")]
        ActCreator,
        /// <summary>
        /// Человек обнаруживший пожар
        /// </summary>
        [EnumDisplayName("Человек обнаруживший пожар")]
        DetectionFire,
        /// <summary>
        /// Руководитель тушением пожара
        /// </summary>
        [EnumDisplayName("Руководитель тушением пожара")]
        FireExtinguishingManager,
        /// <summary>
        /// Ответственное лицо за окарауливание
        /// </summary>
        [EnumDisplayName("Ответственное лицо за окарауливание")]
        Ambush,
        /// <summary>
        /// Присутствовавший при составлении акта
        /// </summary>
        [EnumDisplayName("Присутствовавший при составлении акта")]
        CompilationAct
    }
}
