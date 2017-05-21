using System;
using System.Collections.Generic;

namespace TextQuestLight
{
	/// <summary>
	/// Класс для вопросов: объект содержит в себе сам вопрос и варианты ответов
	/// </summary>
	[Serializable]
	class Question
	{
		#region Свойства

		/// <summary>
		/// Вопрос
		/// </summary>
		public string Query { get; }

		/// <summary>
		/// Словарь ответов. Ключ - 1 или 0, Значение - вариант ответа.
		/// </summary>
		public Dictionary<byte, string> Answers { get; }

		/// <summary>
		/// Флаг, означающий, что игре пришел конец
		/// </summary>
		public bool IsEnd { get; }

		/// <summary>
		/// Флаг, означающий, что игра достигла конца и ее надо зациклить.
		/// </summary>
		public bool ToStart { get; }

		#endregion

		#region Конструкторы

		public Question(string query, string answerA, string answerB, bool isEnd = false, bool toStart = false)
		{
			Query = query;
			Answers = new Dictionary<byte, string>
			{
				{0, answerA},
				{1, answerB}
			};
			IsEnd = isEnd;
			ToStart = toStart;
		}

		#endregion
	}
}
