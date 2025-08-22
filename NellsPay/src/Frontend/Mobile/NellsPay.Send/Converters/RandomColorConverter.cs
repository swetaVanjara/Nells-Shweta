using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics;
namespace NellsPay.Send.Converters
{
    public class RandomColorConverter : IValueConverter
    {
        private static readonly List<Color> Colors = new()
        {
            new Color(1f, 0.8f, 0.8f), // Light Red
            new Color(0.8f, 1f, 0.8f), // Light Green
            new Color(0.8f, 0.8f, 1f), // Light Blue
            new Color(1f, 1f, 0.8f),   // Light Yellow
            new Color(1f, 0.9f, 0.7f), // Light Orange
            new Color(0.95f, 0.8f, 1f),// Light Purple
            new Color(0.8f, 1f, 1f),   // Light Teal
            new Color(1f, 0.9f, 0.9f), // Light Pink
            new Color(0.9f, 0.95f, 1f),// Light Indigo
            new Color(0.9f, 1f, 0.9f), // Light Mint

        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Guid id)
            {
                int index = Math.Abs(id.GetHashCode()) % Colors.Count;
                return Colors[index];
            }

            // return new Color(0.5f, 0.5f, 0.5f);
            return new Color(0.9f, 0.9f, 0.9f); 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
