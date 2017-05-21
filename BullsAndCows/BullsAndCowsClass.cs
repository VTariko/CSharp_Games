using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace BullsAndCows
{
	class BullsAndCowsClass : INotifyPropertyChanged
	{
		#region События

		/// <summary>
		/// Событие изменения свойства
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Событие проверки результатов
		/// </summary>
		public event Action<string> CheckEvent;

		#endregion

		#region Поля

		/// <summary>
		/// Текущее число, введенное пользователем
		/// </summary>
		private byte?[] _current;

		/// <summary>
		/// загаданное компьютером число
		/// </summary>
		private List<byte> _finish;

		/// <summary>
		/// Переменная для передачи результатов из класса игры в основную форму
		/// </summary>
		private IViewCaB _view;

		#endregion

		#region Свойства

		/// <summary>
		/// Введенное число: первое
		/// </summary>
		public string CurrentFirst
		{
			set
			{
				if (!string.IsNullOrEmpty(value) && !_current[0].ToString().Equals(value))
				{
					_current[0] = byte.Parse(value);
				}
				else
				{
					_current[0] = null;
				}
				OnPropertyChanged("CurrentFirst");
			}
		}

		/// <summary>
		/// Введенное число: второе
		/// </summary>
		public string CurrentSecond
		{
			set
			{
				if (!string.IsNullOrEmpty(value) && !_current[1].ToString().Equals(value))
				{
					_current[1] = byte.Parse(value);
				}
				else
				{
					_current[1] = null;
				}
				OnPropertyChanged("CurrentSecond");
			}
		}

		/// <summary>
		/// Введенное число: третье
		/// </summary>
		public string CurrentThird
		{
			set
			{
				if (!string.IsNullOrEmpty(value) && !_current[2].ToString().Equals(value))
				{
					_current[2] = byte.Parse(value);
				}
				else
				{
					_current[2] = null;
				}
				OnPropertyChanged("CurrentThird");
			}
		}

		/// <summary>
		/// Введенное число: четвертое
		/// </summary>
		public string CurrentFourth
		{
			set
			{
				if (!string.IsNullOrEmpty(value) && !_current[3].ToString().Equals(value))
				{
					_current[3] = byte.Parse(value);
				}
				else
				{
					_current[3] = null;
				}
				OnPropertyChanged("CurrentFourth");
			}
		}

		/// <summary>
		/// Результаты проверок
		/// </summary>
		public StringBuilder ResultOfCheck { get; set; }

		#endregion

		#region Конструкторы

		/// <summary>
		/// Создание объекта игры: генерация искомого числа,
		/// связка проверки результатов с текстовым полем, очистка введенных данных
		/// </summary>
		/// <param name="viewCaB"></param>
		public BullsAndCowsClass(IViewCaB viewCaB)
		{
			ResultOfCheck = new StringBuilder();
			_view = viewCaB;
			_view.Result = ResultOfCheck;
			_current = new byte?  [4];
			ResetCurrent();
			_finish = new List<byte>();
			Random rand = new Random();
			for (int i = 0; i < 4; i++)
			{
				_finish.Add(Convert.ToByte(rand.Next(10)));
			}
		}

		#endregion

		#region Методы

		/// <summary>
		/// Проверка введннных данных
		/// </summary>
		public void CheckCurrent()
		{
			//если введены все значения
			if (_current.All(item => item != null))
			{
				//запоминаем введенное число в виде строки
				string number = string.Join("", _current);
				//создаем временный массим, аналогичный сгенерированному, чтоыбю удалять из него найденные значения
				byte?[] finishAlter = new byte?[_finish.Count];
				for (int i = 0; i < _finish.Count; i++)
				{
					finishAlter[i] = _finish[i];
				}
				//узнаем колчиество быков
				int bulls = CheckCurrentBulls(ref finishAlter);
				//узнаем количество коров
				int cows = CheckCurrentCows(ref finishAlter);
				const string template = "Число {0} - быки: {1}, коровы: {2}\n";
				//вставляем в нулевую позицию новый результат проверки
				ResultOfCheck.Insert(0, string.Format(template, number, bulls, cows));
				_view.Result = ResultOfCheck;
				
				//сбрасываем значения полей
				ResetCurrent();

				//если количество быков равно количеству чисел в сгенерированном числе, то генерируем событие победы
				if (bulls==_finish.Count && CheckEvent != null)
				{
					CheckEvent("Вы победили!");
				}
			}
		}

		/// <summary>
		/// Проверка коичества быков
		/// </summary>
		/// <param name="finish"></param>
		/// <returns></returns>
		private int CheckCurrentBulls(ref byte?[] finish)
		{
			int bulls = 0;
			for (int i = 0; i < _finish.Count; i++)
			{
				//если значения совпадают по позициям - прибавляем количество быков,
				//удаляем значение из массивов введенного числа и сгенерированного числа
				if (_current[i].HasValue && _current[i].Value == _finish[i])
				{
					bulls++;
					_current[i] = null;
					finish[i] = null;
				}
			}
			return bulls;
		}

		/// <summary>
		/// Проверка количества коров
		/// </summary>
		/// <param name="finish"></param>
		/// <returns></returns>
		private int CheckCurrentCows(ref byte?[] finish)
		{
			int cows = 0;
			//оставляем от введенного числа только уникальные значения
			byte?[] array = _current.Distinct().ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				for (int k = 0; k < _finish.Count; k++)
				{
					if (i != k && array[i].HasValue && array[i].Value == finish[k])
					{
						cows++;
					}
				}
			}
			return cows;
		}

		/// <summary>
		/// Сброс введенных значений
		/// </summary>
		private void ResetCurrent()
		{
			CurrentFirst = string.Empty;
			CurrentSecond = string.Empty;
			CurrentThird = string.Empty;
			CurrentFourth = string.Empty;
		}

		/// <summary>
		/// Метод обработки события изменения свойства 
		/// </summary>
		/// <param name="name"></param>
		protected void OnPropertyChanged(string name)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(name));
			}
		}

		#endregion
	}
}
