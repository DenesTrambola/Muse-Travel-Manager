using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Muse_Travel_Manager.Converters;

public class ImagePathToImageSourceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string imagePath && !string.IsNullOrEmpty(imagePath))
        {
            return new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
        }
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
