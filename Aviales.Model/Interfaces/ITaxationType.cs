#region Copyright
/****************************************************************************
*  Copyright (c) 2026 Инком. Все права защищены.
*
*  Файл: ITaxationType.cs
*  Автор: Е.В. Месилин (mesilin)
*  Дата создания: 6/2/2026 2:45:45 PM
*  Назначение: Определение интерфейса ITaxationType.cs
****************************************************************************/
#endregion Copyright

namespace Aviales.Model.Interfaces
{
    public interface ITaxationType
    {
        public int Code { get; set; }

        public string? Name { get; set; }

        public string? ShortName { get; set; }

        public int? Year { get; set; }

        public bool IsActive { get; set; }
    }
}
