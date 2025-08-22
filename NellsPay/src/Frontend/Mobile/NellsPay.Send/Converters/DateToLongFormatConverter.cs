namespace NellsPay.Send.Converters;

public class DateToLongFormatConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime date)
            return date.ToString("d MMMM yyyy", CultureInfo.InvariantCulture); // "5 June 2025"

        if (DateTime.TryParse(value?.ToString(), out date))
            return date.ToString("d MMMM yyyy", CultureInfo.InvariantCulture);

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DateTime.TryParse(value?.ToString(), out var result) ? result : value;
    }
}