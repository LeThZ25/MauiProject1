namespace MauiProject1
{
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
