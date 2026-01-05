namespace MauiProject1
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			UpdateThemeIcon();
		}

		private void OnThemeSwitchTapped(object sender, TappedEventArgs e)
		{
			var currentTheme = Application.Current.UserAppTheme;

			if (currentTheme == AppTheme.Unspecified)
				currentTheme = AppTheme.Light;

			// Đổi Theme
			Application.Current.UserAppTheme = (currentTheme == AppTheme.Light)
				? AppTheme.Dark
				: AppTheme.Light;

			UpdateThemeIcon();
		}

		private void UpdateThemeIcon()
		{
			if (Application.Current.UserAppTheme == AppTheme.Dark)
			{
				ThemeIcon.Text = "☀️";
			}
			else
			{
				ThemeIcon.Text = "🌙";
			}
		}
	}
}