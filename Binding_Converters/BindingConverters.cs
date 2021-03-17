using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Example_App
{

    /// <summary>
    /// Used to show things that should be visible when something is true (ex. show when IsRunning is true)
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    class BoolVisibility_Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolToConvert = (bool)value;

            if (boolToConvert == true)
                return Visibility.Visible;
            else //false
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibilityToConvert = (Visibility)value;
            if (visibilityToConvert == Visibility.Visible)
                return true;
            else //Collapsed
                return false;
        }
    }

    /// <summary>
    /// Used to show things that should be visible when something is false (ex. show when debugging mode is false)
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    class BoolVisibilityBackwards_Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolToConvert = (bool)value;

            if (boolToConvert == false)
                return Visibility.Visible;
            else //true
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibilityToConvert = (Visibility)value;
            if (visibilityToConvert == Visibility.Visible)
                return false;
            else //Collapsed
                return true;
        }
    }


    /// <summary>
    /// Used to show things that should be visible when an int has value (ex. show when list.Count > 0)
    /// </summary>
    [ValueConversion(typeof(int), typeof(Visibility))]
    class IntVisibility_Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int intToConvert = (int)value;

            if (intToConvert > 0)
                return Visibility.Visible;
            else //false
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) //This really shouldn't ever be used?
        {
            Visibility visibilityToConvert = (Visibility)value;
            if (visibilityToConvert == Visibility.Visible)
                return 1;
            else //Collapsed
                return 0;
        }
    }

    /// <summary>
    /// Used to enable things when something is false (ex. enable when debugging mode is false)
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    class InverseBool_Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolToConvert = (bool)value;

            return !boolToConvert;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolToConvert = (bool)value;

            return !boolToConvert;
        }
    }

    /// <summary>
    /// Used to convert an int based on a ratio/percent, for example the height of the window * 0.5
    /// </summary>
    [ValueConversion(typeof(int), typeof(double))]
    class IntRatio_Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int toConvert = (int)value;
            double percent = Double.Parse((string)parameter);

            return toConvert * percent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int toConvert = (int)value;
            double percent = Double.Parse((string)parameter);

            return toConvert * (1 / percent);
        }
    }

}
