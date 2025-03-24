using Muse_Travel_Manager.Models;
using System.Globalization;
using System.Windows.Data;

namespace Muse_Travel_Manager.Converters;

public class TourToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Tour tour)
        {
            return $"{tour.Destination}, {tour.Price:F2} грн, {tour.StartDate:dd.MM.yyyy}, {tour.Duration} {(tour.Duration == 1 ? "день" : "дні")}";
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
