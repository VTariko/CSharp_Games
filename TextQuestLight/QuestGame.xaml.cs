using System.Text;
using System.Windows;

namespace TextQuestLight
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class QuestGame : Window, IViewQuest
	{
		#region Поля

		QuestClass quest;

		#endregion

		#region Свойства

		public StringBuilder IQuestion
		{
			set
			{
				if (value != null && value.Length > 0)
				{
					txtQuestion.Text = value.ToString();
				}
			}
		}

		public StringBuilder IAnswerA
		{
			set
			{
				if (value != null && value.Length > 0)
				{
					txtAnswerA.Text = value.ToString();
				}
			}
		}

		public StringBuilder IAnswerB
		{
			set
			{
				if (value != null && value.Length > 0)
				{
					txtAnswerB.Text = value.ToString();
				}
			}
		}

		#endregion

		#region Конструктор

		public QuestGame()
		{
			InitializeComponent();
			quest = new QuestClass(this);
		}

		#endregion

		#region Методы

		private void BtnAnswerA_OnClick(object sender, RoutedEventArgs e)
		{
			quest.Answer(0);
		}

		private void BtnAnswerB_OnClick(object sender, RoutedEventArgs e)
		{
			quest.Answer(1);
		}

		#endregion
	}
}
