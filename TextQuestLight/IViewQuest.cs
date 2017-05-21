using System.Text;

namespace TextQuestLight
{
	/// <summary>
	/// Интерфейс для "прокидывания" вопроса и вариантов ответов проверки в основное окно
	/// </summary>
	interface IViewQuest
	{
		/// <summary>
		/// Строка для хранения вопроса
		/// </summary>
		StringBuilder IQuestion { set; }

		/// <summary>
		/// Строка для первого варианта ответа
		/// </summary>
		StringBuilder IAnswerA { set; }

		/// <summary>
		/// Строка для второго варианта ответа
		/// </summary>
		StringBuilder IAnswerB { set; }
	}
}
