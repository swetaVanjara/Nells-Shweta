using Microsoft.Maui.Converters;
using System.Windows.Input;

namespace NellsPay.Send.CustomControls;

public partial class NewCustomTextField : ContentView
{
    #region Bindable Properties

    // Label Property
    public static readonly BindableProperty LabelProperty =
        BindableProperty.Create(nameof(Label), typeof(string), typeof(NewCustomTextField), string.Empty);

    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    // Text Property (for Entry)
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(NewCustomTextField), string.Empty, BindingMode.TwoWay);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    // Placeholder Property
    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(NewCustomTextField), default(string));

    // EntryTextColor Property
    public static readonly BindableProperty EntryTextColorProperty =
    BindableProperty.Create(
        nameof(EntryTextColor),
        typeof(Color),
        typeof(NewCustomTextField),
        // Safely get the resource or fall back to a default color
        GetResourceOrDefault("TextDefault", Colors.Black));
    private static Color GetResourceOrDefault(string resourceKey, Color defaultColor)
    {
        if (Application.Current?.Resources != null &&
            Application.Current.Resources.TryGetValue(resourceKey, out var resourceValue) &&
            resourceValue is Color color)
        {
            return color;
        }
        return defaultColor;
    }
    public Color EntryTextColor
    {
        get => (Color)GetValue(EntryTextColorProperty);
        set => SetValue(EntryTextColorProperty, value);
    }

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    // FontFamily Property
    public static readonly BindableProperty FontFamilyProperty =
        BindableProperty.Create(
            nameof(FontFamily),
            typeof(string),
            typeof(NewCustomTextField),
            default(string), // or set a default value if desired
            propertyChanged: OnFontFamilyChanged);

    public string FontFamily
    {
        get => (string)GetValue(FontFamilyProperty);
        set => SetValue(FontFamilyProperty, value);
    }

    private static void OnFontFamilyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is NewCustomTextField control && newValue is string newFont)
        {
            control.entry.FontFamily = newFont;
        }
    }

    // FontSize Property
    public static readonly BindableProperty FontSizeProperty =
        BindableProperty.Create(
            nameof(FontSize),
            typeof(double),
            typeof(NewCustomTextField),
            14.0, // default font size; adjust as needed
            propertyChanged: OnFontSizeChanged);

    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    private static void OnFontSizeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is NewCustomTextField control && newValue is double newFontSize)
        {
            control.entry.FontSize = newFontSize;
        }
    }

    // Supporting Text Property
    public static readonly BindableProperty SupportingTextProperty =
        BindableProperty.Create(
            nameof(SupportingText),
            typeof(string),
            typeof(NewCustomTextField),
            string.Empty,
            defaultBindingMode: BindingMode.TwoWay);

    public string SupportingText
    {
        get => (string)GetValue(SupportingTextProperty);
        set => SetValue(SupportingTextProperty, value);
    }

    public static readonly BindableProperty ErrorLabelProperty =
        BindableProperty.Create(nameof(ErrorLabel), typeof(string), typeof(NewCustomTextField), default(string));

    public string ErrorLabel
    {
        get => (string)GetValue(ErrorLabelProperty);
        set => SetValue(ErrorLabelProperty, value);
    }

    // Leading Icon Property
    public static readonly BindableProperty LeadingIconProperty =
        BindableProperty.Create(nameof(LeadingIcon), typeof(string), typeof(NewCustomTextField), string.Empty);

    // PlaceholderColor Property
    public static readonly BindableProperty PlaceholderColorProperty =
    BindableProperty.Create(
        nameof(PlaceholderColor),
        typeof(Color),
        typeof(NewCustomTextField),
        // Safely get the resource or fall back to a default color
        GetResourceOrDefault("TextPlaceholder", Colors.Gray),
        propertyChanged: OnPlaceholderColorChanged);

    private static void OnPlaceholderColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is NewCustomTextField control && newValue is Color newColor)
        {
            control.entry.PlaceholderColor = newColor;
        }
    }

    public Color PlaceholderColor
    {
        get => (Color)GetValue(PlaceholderColorProperty);
        set => SetValue(PlaceholderColorProperty, value);
    }

    public string LeadingIcon
    {
        get => (string)GetValue(LeadingIconProperty);
        set => SetValue(LeadingIconProperty, value);
    }

    // Trailing Icon Property
    public static readonly BindableProperty TrailingIconProperty =
        BindableProperty.Create(nameof(TrailingIcon), typeof(string), typeof(NewCustomTextField), string.Empty);

    public string TrailingIcon
    {
        get => (string)GetValue(TrailingIconProperty);
        set => SetValue(TrailingIconProperty, value);
    }

    // NEW PROPERTIES FOR ENHANCED FUNCTIONALITY

    // IsPickerMode Property - determines if this is a picker (no entry, just text)
    public static readonly BindableProperty IsPickerModeProperty =
        BindableProperty.Create(nameof(IsPickerMode), typeof(bool), typeof(NewCustomTextField), false,
            propertyChanged: OnDisplayModeChanged);

    public bool IsPickerMode
    {
        get => (bool)GetValue(IsPickerModeProperty);
        set => SetValue(IsPickerModeProperty, value);
    }

    // PickerText Property - the text to display when in picker mode
    public static readonly BindableProperty PickerTextProperty =
        BindableProperty.Create(nameof(PickerText), typeof(string), typeof(NewCustomTextField), string.Empty);

    public string PickerText
    {
        get => (string)GetValue(PickerTextProperty);
        set => SetValue(PickerTextProperty, value);
    }

    // IsEntryVisible Property - calculated property based on mode
    public bool IsEntryVisible => !IsPickerMode;

    // LeadingImageSource Property - for when you want to use an image instead of an icon
    public static readonly BindableProperty LeadingImageSourceProperty =
        BindableProperty.Create(nameof(LeadingImageSource), typeof(ImageSource), typeof(NewCustomTextField), null);

    public ImageSource LeadingImageSource
    {
        get => (ImageSource)GetValue(LeadingImageSourceProperty);
        set => SetValue(LeadingImageSourceProperty, value);
    }

    // IsLeadingImageVisible Property - determines if we show an image instead of an icon
    public bool IsLeadingImageVisible => LeadingImageSource != null;

    // IsLeadingIconVisible Property - determines if we show the icon
    public bool IsLeadingIconVisible => !string.IsNullOrEmpty(LeadingIcon) && LeadingImageSource == null;

    // SecondaryText Property - for displaying currency code, etc.
    public static readonly BindableProperty SecondaryTextProperty =
        BindableProperty.Create(nameof(SecondaryText), typeof(string), typeof(NewCustomTextField), null);

    public string SecondaryText
    {
        get => (string)GetValue(SecondaryTextProperty);
        set => SetValue(SecondaryTextProperty, value);
    }

    // InfoIcon Property - for displaying an info icon
    public static readonly BindableProperty InfoIconProperty =
        BindableProperty.Create(nameof(InfoIcon), typeof(string), typeof(NewCustomTextField), "\uf05a"); // Default to info icon

    public string InfoIcon
    {
        get => (string)GetValue(InfoIconProperty);
        set => SetValue(InfoIconProperty, value);
    }

    // ShowInfoIcon Property - determines if we show the info icon
    public static readonly BindableProperty ShowInfoIconProperty =
        BindableProperty.Create(nameof(ShowInfoIcon), typeof(bool), typeof(NewCustomTextField), false);

    public bool ShowInfoIcon
    {
        get => (bool)GetValue(ShowInfoIconProperty);
        set => SetValue(ShowInfoIconProperty, value);
    }

    // InfoIconCommand Property - command to execute when info icon is tapped
    public static readonly BindableProperty InfoIconCommandProperty =
        BindableProperty.Create(nameof(InfoIconCommand), typeof(ICommand), typeof(NewCustomTextField));

    public ICommand InfoIconCommand
    {
        get => (ICommand)GetValue(InfoIconCommandProperty);
        set => SetValue(InfoIconCommandProperty, value);
    }

    private static void OnDisplayModeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is NewCustomTextField control)
        {
            control.OnPropertyChanged(nameof(IsEntryVisible));
        }
    }

    public event EventHandler TrailingIconTapped;

    // This method is invoked when the trailing icon is tapped.
    private void OnTrailingIconTappedHandler(object sender, EventArgs e)
    {
        TrailingIconTapped?.Invoke(this, EventArgs.Empty);
    }

    // Border Color Property (normal state)
    public static readonly BindableProperty BorderColorProperty =
        BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(NewCustomTextField), Colors.Gray,
                                propertyChanged: OnBorderColorChanged);

    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }

    private static void OnBorderColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is NewCustomTextField control && !control.entry.IsFocused)
        {
            // Only update the border if HasError is false.
            if (!control.HasError)
                control.borderContainer.Stroke = (Color)newValue;
        }
    }

    // Focused Border Color Property (when Entry is focused)
    public static readonly BindableProperty FocusedBorderColorProperty =
    BindableProperty.Create(
        nameof(FocusedBorderColor),
        typeof(Color),
        typeof(NewCustomTextField),
        GetResourceOrDefault("BorderFilled", Colors.Blue));

    public Color FocusedBorderColor
    {
        get => (Color)GetValue(FocusedBorderColorProperty);
        set => SetValue(FocusedBorderColorProperty, value);
    }

    // Keyboard Property
    public static readonly BindableProperty KeyboardProperty =
        BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(NewCustomTextField), Keyboard.Default,
                                propertyChanged: OnKeyboardChanged);

    [TypeConverter(typeof(KeyboardTypeConverter))]
    public Keyboard Keyboard
    {
        get => (Keyboard)GetValue(KeyboardProperty);
        set => SetValue(KeyboardProperty, value);
    }

    private static void OnKeyboardChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is NewCustomTextField control && newValue is Keyboard newKeyboard)
        {
            control.entry.Keyboard = newKeyboard;
        }
    }

    // HasError Property
    public static readonly BindableProperty HasErrorProperty =
        BindableProperty.Create(nameof(HasError), typeof(bool), typeof(NewCustomTextField), false,
                                propertyChanged: OnHasErrorChanged);

    public bool HasError
    {
        get => (bool)GetValue(HasErrorProperty);
        set => SetValue(HasErrorProperty, value);
    }

    private static void OnHasErrorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is NewCustomTextField control && newValue is bool hasError)
        {
            if (hasError)
            {
                // Set error colors.
                Color errorColor = GetResourceOrDefault("TextError", Colors.Red);
                control.borderContainer.Stroke = errorColor;
                control.supportingTextLabel.TextColor = errorColor;
                control.EntryTextColor = errorColor;
                control.borderContainer.BackgroundColor = GetResourceOrDefault("SurfaceErrorDefault", Colors.LightPink);
                control.PlaceholderColor = errorColor;

                // If an error label is defined, use it; otherwise, fall back to SupportingText.
                if (!string.IsNullOrEmpty(control.ErrorLabel))
                {
                    control.supportingTextLabel.Text = control.ErrorLabel;
                }
                else
                {
                    control.supportingTextLabel.Text = control.SupportingText;
                }
                if (control.leadingIconSource != null)
                {
                    control.leadingIconSource.Color = errorColor;
                }
                // Force the supporting label to be visible when there's an error.
                control.supportingTextLabel.IsVisible = true;
            }
            else
            {
                // Revert the border to BorderColor.
                control.borderContainer.Stroke = control.entry.IsFocused ? control.FocusedBorderColor : control.BorderColor;

                // Set the supporting text label's color back to the placeholder color.
                Color placeholderColor = GetResourceOrDefault("TextPlaceholder", Colors.Gray);
                control.supportingTextLabel.TextColor = placeholderColor;
                control.PlaceholderColor = placeholderColor;

                // Revert the supporting label's text to the normal SupportingText value.
                control.supportingTextLabel.Text = control.SupportingText;

                // Reset the entry text color to the default
                control.EntryTextColor = GetResourceOrDefault("TextDefault", Colors.Black);

                if (control.leadingIconSource != null)
                {
                    control.leadingIconSource.Color = GetResourceOrDefault("Gray800", Colors.DarkGray);
                }
                control.borderContainer.BackgroundColor = GetResourceOrDefault("SurfaceContainerLowest", Colors.White);
                // Set the visibility based on whether SupportingText has a value.
                control.supportingTextLabel.IsVisible = !string.IsNullOrEmpty(control.SupportingText);
            }
        }
    }

    #endregion

    #region TextChanged Event

    // Expose a TextChanged event from the underlying BorderlessEntry.
    public event EventHandler<TextChangedEventArgs> TextChanged;

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        TextChanged?.Invoke(this, e);

        if (!HasError)
        {
            // If there's text, set border to "BorderFilled"
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                borderContainer.Stroke = GetResourceOrDefault("BorderFilled", Colors.Blue);
            }
            else
            {
                // No text: revert based on focus.
                borderContainer.Stroke = entry.IsFocused ? FocusedBorderColor : BorderColor;
            }
        }
    }

    #endregion

    // EntryStyle Property
    public static readonly BindableProperty EntryStyleProperty =
        BindableProperty.Create(
            nameof(EntryStyle),
            typeof(Style),
            typeof(NewCustomTextField),
            default(Style),
            propertyChanged: OnEntryStyleChanged);

    public Style EntryStyle
    {
        get => (Style)GetValue(EntryStyleProperty);
        set => SetValue(EntryStyleProperty, value);
    }

    private static void OnEntryStyleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is NewCustomTextField control)
        {
            control.entry.Style = newValue as Style;
        }
    }
    public static readonly BindableProperty IsReadOnlyProperty =
    BindableProperty.Create(
        nameof(IsReadOnly),
        typeof(bool),
        typeof(NewCustomTextField),
        false,
        propertyChanged: OnIsReadOnlyChanged);
    public bool IsReadOnly
    {
        get => (bool)GetValue(IsReadOnlyProperty);
        set => SetValue(IsReadOnlyProperty, value);
    }
    private static void OnIsReadOnlyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is NewCustomTextField control && newValue is bool isReadOnly)
        {
            control.entry.IsReadOnly = isReadOnly;
            control.entry.IsEnabled = !isReadOnly;

        }
    }
    // IsPassword Property
    public static readonly BindableProperty IsPasswordProperty =
        BindableProperty.Create(
            nameof(IsPassword),
            typeof(bool),
            typeof(NewCustomTextField),
            false);

    public bool IsPassword
    {
        get => (bool)GetValue(IsPasswordProperty);
        set => SetValue(IsPasswordProperty, value);
    }

    // TrailingIconCommand Property
    public static readonly BindableProperty TrailingIconCommandProperty =
        BindableProperty.Create(nameof(TrailingIconCommand), typeof(ICommand), typeof(NewCustomTextField));

    public ICommand TrailingIconCommand
    {
        get => (ICommand)GetValue(TrailingIconCommandProperty);
        set => SetValue(TrailingIconCommandProperty, value);
    }

    public NewCustomTextField()
    {
        InitializeComponent();

        // Set the initial border color.
        borderContainer.Stroke = BorderColor;

        // Attach event handlers to the Entry's focus and text changed events.
        entry.Focused += Entry_Focused;
        entry.Unfocused += Entry_Unfocused;
        entry.TextChanged += Entry_TextChanged;

        // Add tap gesture for the entire control when in picker mode
        var tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += OnControlTapped;
        borderContainer.GestureRecognizers.Add(tapGesture);
    }

    private void OnControlTapped(object sender, EventArgs e)
    {
        if (IsPickerMode)
        {
            // Execute the trailing icon command if it exists
            TrailingIconCommand?.Execute(null);
            // Or raise the event
            TrailingIconTapped?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Entry_Focused(object sender, FocusEventArgs e)
    {
        // Only change to FocusedBorderColor if there is no error.
        if (!HasError)
        {
            // If there is any text, use the "BorderFilled" color.
            if (!string.IsNullOrEmpty(Text))
            {
                borderContainer.Stroke = GetResourceOrDefault("BorderFilled", Colors.Blue);
            }
            else
            {
                // No text: use the focused border color.
                borderContainer.Stroke = FocusedBorderColor;
            }
        }
    }

    private void Entry_Unfocused(object sender, FocusEventArgs e)
    {
        // If there is no error, revert to the normal border color.
        if (!HasError)
        {
            // If there is any text, keep "BorderFilled"; otherwise, use the normal BorderColor.
            if (!string.IsNullOrEmpty(Text))
            {
                borderContainer.Stroke = GetResourceOrDefault("BorderFilled", Colors.Blue);
            }
            else
            {
                borderContainer.Stroke = BorderColor;
            }
        }
    }
}