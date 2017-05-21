using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BullsAndCows
{
	/// <summary>
	/// Interaction logic for BullsAndCowsGame.xaml
	/// </summary>
	public partial class BullsAndCowsGame : Window, IViewCaB
	{
		public StringBuilder Result
		{
			set
			{
				if (value != null && value.Length>0)
				{
					txtResult.Text = value.ToString();
				}
				if (value != null && value.Length == 0)
				{
					txtResult.Text = string.Empty;
				}
			}
		}

		BullsAndCowsClass bac;

		public BullsAndCowsGame()
		{
			InitializeComponent();
			bac = new BullsAndCowsClass(this);
			bac.CheckEvent += BullsOnCheckEvent;
			DataContext = bac;
		}

		private void BullsOnCheckEvent(string s)
		{
			MessageBox.Show(s);
			bac = new BullsAndCowsClass(this);
			DataContext = bac;
		}

		private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			e.Handled = !string.IsNullOrEmpty(((TextBox)sender).Text) || !IsTextAllowed(e.Text);
		}

		private static bool IsTextAllowed(string text)
		{
			Regex regex = new Regex("[^0-9]");
			return !regex.IsMatch(text);
		}

		private void OnPreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Space)
			{
				e.Handled = true;
			}
			base.OnPreviewKeyDown(e);
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			bac.CheckCurrent();
		}

	}
}
