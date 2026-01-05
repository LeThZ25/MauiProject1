namespace MauiProject1
{
 lethuong
	public partial class MainPage : ContentPage
	{
		private int _fanLevel = 0;

		public MainPage()
		{
			InitializeComponent();
			UpdateThemeIcon();
		}

		private void OnThemeSwitchTapped(object sender, TappedEventArgs e)
		{
			var currentTheme = Application.Current.UserAppTheme;
			if (currentTheme == AppTheme.Unspecified) currentTheme = AppTheme.Light;

			Application.Current.UserAppTheme = (currentTheme == AppTheme.Light) ? AppTheme.Dark : AppTheme.Light;
			UpdateThemeIcon();
		}

		private void UpdateThemeIcon()
		{
			if (Application.Current.UserAppTheme == AppTheme.Dark)
				ThemeIcon.Text = "☀️";
			else
				ThemeIcon.Text = "🌙";
		}

		private async void OnLightToggled(object sender, ToggledEventArgs e)
		{
			if (e.Value)
			{
				await AnimateStatusChange(LightStatusLabel, "ON", Color.FromArgb("#FF9800"));
				await AnimateCard(LightCard, 1.05f);
			}
			else
			{
				await AnimateStatusChange(LightStatusLabel, "OFF", Colors.Gray);
				await AnimateCard(LightCard, 1.0f);
			}
		}

		private async void OnAirCondToggled(object sender, ToggledEventArgs e)
		{
			if (e.Value)
			{
				await AnimateStatusChange(AirCondStatusLabel, "18°C", Color.FromArgb("#2196F3"));
				await AnimateCard(AirCondCard, 1.05f);
			}
			else
			{
				await AnimateStatusChange(AirCondStatusLabel, "OFF", Colors.Gray);
				await AnimateCard(AirCondCard, 1.0f);
			}
		}

		private async void OnFanToggled(object sender, ToggledEventArgs e)
		{
			if (e.Value)
			{
				_fanLevel = _fanLevel >= 3 ? 1 : _fanLevel + 1;
				string levelText = $"Level {_fanLevel}";

				await AnimateStatusChange(FanStatusLabel, levelText, Color.FromArgb("#2196F3"));
				await AnimateCard(FanCard, 1.05f);
				await RotateFanIcon();
			}
			else
			{
				_fanLevel = 0;
				await AnimateStatusChange(FanStatusLabel, "OFF", Colors.Gray);
				await AnimateCard(FanCard, 1.0f);
			}
		}

		private async void OnSecurityToggled(object sender, ToggledEventArgs e)
		{
			if (e.Value)
			{
				await AnimateStatusChange(SecurityStatusLabel, "Armed", Color.FromArgb("#E91E63"));
				await AnimateCard(SecurityCard, 1.05f);
			}
			else
			{
				await AnimateStatusChange(SecurityStatusLabel, "Disarmed", Colors.Gray);
				await AnimateCard(SecurityCard, 1.0f);
			}
		}

		private async Task AnimateStatusChange(Label label, string newText, Color newColor)
		{
			await label.FadeTo(0, 150);
			label.Text = newText;
			label.TextColor = newColor;
			label.Scale = 0.8;

			await Task.WhenAll(
				label.FadeTo(1, 150),
				label.ScaleTo(1, 150, Easing.SpringOut)
			);
		}

		private async Task AnimateCard(Border card, float scale)
		{
			await card.ScaleTo(scale, 200, Easing.SpringOut);
			if (scale > 1.0f)
			{
				await Task.Delay(100);
				await card.ScaleTo(1.0f, 200, Easing.SpringIn);
			}
		}

		private async Task RotateFanIcon()
		{
			if (FanSwitch.IsToggled)
			{
				uint duration = (uint)(1000 / (_fanLevel == 0 ? 1 : _fanLevel));
				await FanIcon.RotateTo(360, duration, Easing.Linear);
				FanIcon.Rotation = 0;
				if (FanSwitch.IsToggled)
				{
					await RotateFanIcon();
				}
			}
		}
	}
}

    public partial class MainPage : ContentPage
    {
        private int _fanLevel = 0; // 0 = OFF, 1-3 = Level 1-3

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLightToggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                // ON state
                await AnimateStatusChange(LightStatusLabel, "ON", Color.FromArgb("#FFA500"));
                await AnimateCard(LightCard, 1.05f);
            }
            else
            {
                // OFF state
                await AnimateStatusChange(LightStatusLabel, "OFF", Color.FromArgb("#808080"));
                await AnimateCard(LightCard, 1.0f);
            }
        }

        private async void OnAirCondToggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                // ON state - show temperature
                await AnimateStatusChange(AirCondStatusLabel, "18°C", Color.FromArgb("#00E5FF"));
                await AnimateCard(AirCondCard, 1.05f);
            }
            else
            {
                // OFF state
                await AnimateStatusChange(AirCondStatusLabel, "OFF", Color.FromArgb("#808080"));
                await AnimateCard(AirCondCard, 1.0f);
            }
        }

        private async void OnFanToggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                // ON state - cycle through levels
                _fanLevel = _fanLevel >= 3 ? 1 : _fanLevel + 1;
                string levelText = $"Level {_fanLevel}";
                await AnimateStatusChange(FanStatusLabel, levelText, Color.FromArgb("#00E5FF"));
                await AnimateCard(FanCard, 1.05f);
                
                // Animate fan icon rotation
                await RotateFanIcon();
            }
            else
            {
                // OFF state
                _fanLevel = 0;
                await AnimateStatusChange(FanStatusLabel, "OFF", Color.FromArgb("#808080"));
                await AnimateCard(FanCard, 1.0f);
            }
        }

        private async void OnSecurityToggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                // Armed state
                await AnimateStatusChange(SecurityStatusLabel, "Armed", Color.FromArgb("#9B59B6"));
                await AnimateCard(SecurityCard, 1.05f);
            }
            else
            {
                // Disarmed state
                await AnimateStatusChange(SecurityStatusLabel, "Disarmed", Color.FromArgb("#808080"));
                await AnimateCard(SecurityCard, 1.0f);
            }
        }

        private async Task AnimateStatusChange(Label label, string newText, Color newColor)
        {
            // Fade out
            await label.FadeTo(0, 150);
            
            // Update text and color
            label.Text = newText;
            label.TextColor = newColor;
            
            // Fade in with scale animation
            label.Scale = 0.8;
            await Task.WhenAll(
                label.FadeTo(1, 150),
                label.ScaleTo(1, 150, Easing.SpringOut)
            );
        }

        private async Task AnimateCard(Border card, float scale)
        {
            await card.ScaleTo(scale, 200, Easing.SpringOut);
            if (scale > 1.0f)
            {
                await Task.Delay(100);
                await card.ScaleTo(1.0f, 200, Easing.SpringIn);
            }
        }

        private async Task RotateFanIcon()
        {
            if (FanSwitch.IsToggled)
            {
                uint duration = (uint)(1000 / _fanLevel); // Faster rotation for higher levels
                await FanIcon.RotateTo(360, duration, Easing.Linear);
                FanIcon.Rotation = 0;
                
                // Continue rotation if still on
                if (FanSwitch.IsToggled)
                {
                    await RotateFanIcon();
                }
            }
        }
    }
}
 main
