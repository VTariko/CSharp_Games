using System.Text;

namespace BullsAndCows
{
	/// <summary>
	/// Интерфейс для "прокидывания" результатов проврки в основное окно
	/// </summary>
	interface IViewCaB
	{
		/// <summary>
		/// Строка для хранения результатов проверок
		/// </summary>
		StringBuilder Result { set; }
	}
}
