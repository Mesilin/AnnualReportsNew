using Incom.Common.Collections;
using System.ComponentModel;

namespace Aviales.Model.Enums
{
	public enum ThreatType : byte
	{
		// С Description СВЯЗАНЫ ИМЕНА СПРАВОЧНИКА "НАПРАВЛЕНИЯ УВЕДОМЛЕНИЙ". ПРОСТО ТАК НЕ МЕНЯТЬ!

		/// <summary>
		/// Муниципального характера
		/// </summary>
		[Description("Угроза ЧС муниципального характера")]
		[EnumDisplayName("Угроза ЧС муниципального характера")]
		Municipal = 0,
		/// <summary>
		/// Регионального характера
		/// </summary>
		[Description("Угроза ЧС регионального характера")]
		[EnumDisplayName("Угроза ЧС регионального характера")]
		Regional = 1,

		///// <summary>
		///// Межрегионального характера
		///// </summary>
		//[EnumDisplayName("Межрегиональный характер")]
		//Interregional = 2,
		///// <summary>
		///// Федерального характера
		///// </summary>
		//[EnumDisplayName("Федеральный характер")]
		//Federal = 3

		/// <summary>
		/// Угроза уязвимым объектам
		/// </summary>
		[Description("Угроза уязвимым объектам")]
		[EnumDisplayName("Угроза уязвимым объектам")]
		VulnerableObject = 4,
	}
}
