using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace TextQuestLight
{
	/// <summary>
	/// Класс игры
	/// </summary>
	class QuestClass
	{
		#region Поля

		/// <summary>
		/// Битовая последовательность выбранных ответов
		/// </summary>
		private StringBuilder _answers;

		/// <summary>
		/// Переменная для передачи результатов из класса игры в основную форму
		/// </summary>
		private IViewQuest _view;

		#endregion

		#region Свойства

		/// <summary>
		/// Список вопросов-ответов для игры
		/// </summary>
		public Dictionary<int, Question> Questions { get; }

		private Question Quest => Questions[Convert.ToInt32(_answers.ToString(), 2)];

		#endregion

		#region Конструкторы

		public QuestClass(IViewQuest view)
		{
			_view = view;
			_answers = new StringBuilder("1");
			Questions = new Dictionary<int, Question>();
			//string path = "scenario.vt";
			//InitGame(path);
			InitGame();
			
			FillView();
		}

		#endregion

		#region Методы

		/// <summary>
		/// Метод заполнения игры вопросами и их решениями из файла
		/// </summary>
		/// <param name="path"></param>
		private void InitGame(string path)
		{
			if (File.Exists(path))
			{
				using (StreamReader sr = new StreamReader(path))
				{
					while (!sr.EndOfStream)
					{
						string readLine = sr.ReadLine();
						if (!string.IsNullOrEmpty(readLine))
						{
							//Делим строку по разделительному символу. 0 - ключ к вопросу, 1 - сам вопрос,
							//2 и 3 - ответы или 2 - флаг конца игры,
							//4 - если есть, флаг зацикливания
							string[] str = readLine.Split('|');
							string q = Regex.Unescape(str[1]);
							if (str.Length < 4)
							{
								Questions.Add(int.Parse(str[0]), new Question(q, "Начать сначала", "Начать сначала", true));
							}
							else if (str.Length == 4)
							{
								Questions.Add(int.Parse(str[0]), new Question(q, str[2], str[3]));
							}
							else
							{
								Questions.Add(int.Parse(str[0]), new Question(q, str[2], str[3], false, true));
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Метод заполнения игры вопросами и их решениями, используя файл-хардкод
		/// </summary>
		private void InitGame()
		{
			List<string> strList = new QuestionsText().QuestionStrings;
			foreach (string item in strList)
			{
				//Делим строку по разделительному символу. 0 - ключ к вопросу, 1 - сам вопрос,
				//2 и 3 - ответы или 2 - флаг конца игры,
				//4 - если есть, флаг зацикливания
				string[] str = item.Split('|');
				string q = Regex.Unescape(str[1]);
				if (str.Length < 4)
				{
					Questions.Add(int.Parse(str[0]), new Question(q, "Начать сначала", "Начать сначала", true));
				}
				else if (str.Length == 4)
				{
					Questions.Add(int.Parse(str[0]), new Question(q, str[2], str[3]));
				}
				else
				{
					Questions.Add(int.Parse(str[0]), new Question(q, str[2], str[3], false, true));
				}
			}
		}

		/// <summary>
		/// Заполнение полей текущими значениями
		/// </summary>
		private void FillView()
		{
			_view.IQuestion = new StringBuilder(Quest.Query);
			_view.IAnswerA = new StringBuilder(Quest.Answers[0]);
			_view.IAnswerB = new StringBuilder(Quest.Answers[1]);
		}

		/// <summary>
		/// Метод обработки ответа
		/// </summary>
		/// <param name="n"></param>
		public void Answer(int n)
		{
			if (!Quest.IsEnd && !Quest.ToStart)
			{
				_answers.Append(n.ToString());
			}
			else
			{
				_answers = new StringBuilder("1");
			}
			FillView();
		}

		#endregion
	}
}
