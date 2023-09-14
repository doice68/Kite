using Avalonia;
using Avalonia.Controls;
using Material.Icons;

namespace Kite.Controls
{
    public partial class MenuItemPro : MenuItem
    {
        public static readonly StyledProperty<MaterialIconKind> IIconProperty =
            AvaloniaProperty.Register<MenuItemPro, MaterialIconKind>(
            nameof(IIconProperty));

        public MaterialIconKind IIcon
        {
            get => GetValue(IIconProperty);
            set
            {
                SetValue(IIconProperty, value);
            }
        }

        public static readonly StyledProperty<string> TextProperty =
            AvaloniaProperty.Register<MenuItemPro, string>(
            nameof(TextProperty));

        public string Text
        {
            get => GetValue(TextProperty);
            set
            {
                SetValue(TextProperty, value);
            }
        }

        public MenuItemPro()
        {
            InitializeComponent();
        }
    }
}
