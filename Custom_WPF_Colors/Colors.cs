using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Example
{
    public static class Colors
    {
        public static void SetBaseColor()
        {
            try
            {
                string color = Properties.Settings.Default.BaseColor;

                if (color == "Light")
                {
                    App.Current.Resources["COLOR_Background"] = (Color)ColorConverter.ConvertFromString("#FFFFFF");

                    App.Current.Resources["Background"] = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFFFF");
                }
                else if (color == "Dark")
                {
                    App.Current.Resources["COLOR_Background"] = (Color)ColorConverter.ConvertFromString("#1C1C1C");

                    App.Current.Resources["Background"] = (SolidColorBrush)new BrushConverter().ConvertFromString("#1C1C1C");
                }

                Icons.UpdateIcons();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error updating base colors: {ex.Message}");
            }
        }

        public static void SetAccentColor()
        {
            try
            {
                string color = Properties.Settings.Default.AccentColor;
                string accent = "";

                if (color == "Cyan")
                {
                    accent = "#1BA8D4";
                }
                else if (color == "Red")
                {
                    accent = "#E03B3B";
                }
                else //Custom color
                {
                    string accentHex = color.Remove(0, 1);
                    accent = color;
                    Color ac = (Color)ColorConverter.ConvertFromString(accent);
                    //Make a light and dark accent version that are a bit lighter or darker - I removed the AccentLight and AccentDark from this example,
					//but leaving commented code in case I want to use this later
                    //accentLight = "#" + System.Windows.Forms.ControlPaint.Light(System.Drawing.Color.FromArgb(ac.A, ac.R, ac.G, ac.B), 0.1f).ToArgb().ToString("X");
                    //accentDark = "#" + System.Windows.Forms.ControlPaint.Dark(System.Drawing.Color.FromArgb(ac.A, ac.R, ac.G, ac.B), 0.0001f).ToArgb().ToString("X");
                }

                if (!String.IsNullOrEmpty(accent) && !String.IsNullOrEmpty(accentLight) && !String.IsNullOrEmpty(accentDark))
                {
                    App.Current.Resources["COLOR_Accent"] = (Color)ColorConverter.ConvertFromString(accent);

                    App.Current.Resources["Accent"] = (SolidColorBrush)new BrushConverter().ConvertFromString(accent);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error updating accent color: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
