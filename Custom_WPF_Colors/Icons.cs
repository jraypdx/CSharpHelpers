using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    public class Icons
    {
        public static string DarkBase = "/Resources/Negatives/";
        public static string LightBase = "/Resources/";
       

        public static void UpdateIcons()
        {
            try
            {
                string baseColor = Properties.Settings.Default.BaseColor;
                string baseString = "";

                if (baseColor == "Dark")
                    baseString = DarkBase;
                else if (baseColor == "Light")
                    baseString = LightBase;

				//This assumes the icons are in a separate resuorce dict, which they aren't for this example.  Instead you can change them indidually if there are only a few
                var iconResourceDict = System.Windows.Application.Current.Resources.MergedDictionaries.Where(x => x.Source != null && x.Source.ToString().Contains("Icon")).First();

				//Assuming you set the key to be the file name (without extension, assuming all icons are .png), then this will swap dark/light mode icons (dark mode are stored in /Negatives/ folder in the Resources folder used for icons)
                foreach (var key in iconResourceDict.Keys)
                {
                    string iconKey = (string)key;

                    System.Windows.Application.Current.Resources[key] = new System.Windows.Media.Imaging.BitmapImage(new Uri($"pack://application:,,,{baseString}{iconKey}.png"));
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error updating icons: {ex.Message}");
            }
        }

    }

}
