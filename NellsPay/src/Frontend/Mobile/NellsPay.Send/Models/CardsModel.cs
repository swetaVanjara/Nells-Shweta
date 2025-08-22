using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NellsPay.Send.Models
{
    public class CardsModel : BaseViewModel
    {
        private string _cardNumber = string.Empty;
        public string CardNumber
        {
            get { return _cardNumber; }
            set
            {
                if (_cardNumber != value)
                {
                    _cardNumber = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(LastFourDigits));
                    OnPropertyChanged(nameof(Color));
                    OnPropertyChanged(nameof(CardType));
                }
            }
        }
        public string ExpiredYear { get; set; } = string.Empty;
        public string ExpiredMonth { get; set; } = string.Empty;
        public string CardHolderName { get; set; } = string.Empty;
        public string CardType => GetCardType(CardNumber);

        private string GetCardType(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber))
                return "Unknown";

            cardNumber = cardNumber.Replace(" ", "").Replace("-", "");

            if (Regex.IsMatch(cardNumber, @"^4[0-9]{12}(?:[0-9]{3})?$"))
                return "Visa Card";
            if (Regex.IsMatch(cardNumber, @"^5[1-5][0-9]{14}$"))
                return "MasterCard";
            if (Regex.IsMatch(cardNumber, @"^3[47][0-9]{13}$"))
                return "American Express";
            if (Regex.IsMatch(cardNumber, @"^6(?:011|5[0-9]{2})[0-9]{12}$"))
                return "Discover";

            return "Unknown";
        }


        public string Color => GenerateRandomDarkColor();
        private string GenerateRandomDarkColor()
        {
            Random rand = new Random();

            int hue = rand.Next(180, 300); // Blue, purple, and green tones
            int saturation = rand.Next(70, 100); // High saturation for vibrancy
            int brightness = rand.Next(50, 80); // Mid-to-high brightness to avoid black or brown

            return ColorFromHSB(hue, saturation, brightness);
        }
        private string ColorFromHSB(int hue, int saturation, int brightness)
        {
            float s = saturation / 100f;
            float v = brightness / 100f;
            int hi = (hue / 60) % 6;
            float f = (hue / 60f) - hi;
            float p = v * (1 - s);
            float q = v * (1 - f * s);
            float t = v * (1 - (1 - f) * s);

            float r = 0, g = 0, b = 0;

            switch (hi)
            {
                case 0: r = v; g = t; b = p; break;
                case 1: r = q; g = v; b = p; break;
                case 2: r = p; g = v; b = t; break;
                case 3: r = p; g = q; b = v; break;
                case 4: r = t; g = p; b = v; break;
                case 5: r = v; g = p; b = q; break;
            }

            int red = (int)(r * 255);
            int green = (int)(g * 255);
            int blue = (int)(b * 255);

            return $"#{red:X2}{green:X2}{blue:X2}";
        }
        public string LastFourDigits => CardNumber?.Length >= 4 ? CardNumber.Substring(CardNumber.Length - 4) : string.Empty;


    }
}
