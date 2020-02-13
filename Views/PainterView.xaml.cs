using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Painter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PainterView : ContentPage
    {
        public Models.PainterModel painterModel;

        public Dictionary<Menu.MenuType, Menu> Menus;

        public PainterView()
        {
            this.InitializeComponent();
            this.painterModel = new Models.PainterModel(this);
            this.BindingContext = this.painterModel;
            MessagingCenter.Subscribe<Models.PainterModel, string>(this, "Error", (sender, error) =>
            {
                this.DisplayAlert("Error", error, "Okay");
            });
            this.Initialize();
        }

        public void Initialize()
        {
            this.Menus = new Dictionary<Menu.MenuType, Menu>()
            {
                {Menu.MenuType.Standard, new Menu(Menu.MenuType.Standard, true, this.GridStandardMenu, Menu.SlideOutDirection.Left)},
                {Menu.MenuType.File, new Menu(Menu.MenuType.File, false, this.GridFileMenu, Menu.SlideOutDirection.Up)},
                {Menu.MenuType.Tool, new Menu(Menu.MenuType.Tool, false, this.GridToolMenu, Menu.SlideOutDirection.Left)},
                {Menu.MenuType.Brush, new Menu(Menu.MenuType.Brush, false, this.GridBrushMenu, Menu.SlideOutDirection.Left)}
            };
        }

        public async void AnimateMenu(Menu menu, bool recursive)
        {
            // If menu is open, close menu
            if(menu.IsOpen == true)
            {
                if(recursive == true)
                {
                    this.AnimateMenu(this.Menus[Menu.MenuType.Standard], false);
                }
                menu.IsOpen = false;
                switch(menu.Direction)
                {
                    case Menu.SlideOutDirection.Up:
                    {
                        await menu.MenuGrid.TranslateTo(menu.MenuGrid.X, menu.MenuGrid.TranslationY - menu.MenuGrid.Height, 100, Easing.Linear);
                        break;
                    }
                    case Menu.SlideOutDirection.Down:
                    {
                        await menu.MenuGrid.TranslateTo(menu.MenuGrid.X, menu.MenuGrid.TranslationY + menu.MenuGrid.Height, 100, Easing.Linear);
                        break;
                    }
                    case Menu.SlideOutDirection.Left:
                    {
                        await menu.MenuGrid.TranslateTo(menu.MenuGrid.TranslationX - menu.MenuGrid.Width, menu.MenuGrid.Y, 100, Easing.Linear);
                        break;
                    }
                    case Menu.SlideOutDirection.Right:
                    {
                        await menu.MenuGrid.TranslateTo(menu.MenuGrid.TranslationX + menu.MenuGrid.Width, menu.MenuGrid.Y, 100, Easing.Linear);
                        break;
                    }
                }
            }
            // If menu is closed, open menu
            else
            {
                if(recursive == true)
                {
                    foreach(KeyValuePair<Menu.MenuType, Menu> m in this.Menus)
                    {
                        if(m.Value.IsOpen == true)
                        {
                            this.AnimateMenu(m.Value, false);
                        }
                    }
                }
                menu.IsOpen = true;
                switch(menu.Direction)
                {
                    case Menu.SlideOutDirection.Up:
                    {
                        await menu.MenuGrid.TranslateTo(menu.MenuGrid.X, menu.MenuGrid.TranslationY + menu.MenuGrid.Height, 100, Easing.Linear);
                        break;
                    }
                    case Menu.SlideOutDirection.Down:
                    {
                        await menu.MenuGrid.TranslateTo(menu.MenuGrid.X, menu.MenuGrid.TranslationY - menu.MenuGrid.Height, 100, Easing.Linear);
                        break;
                    }
                    case Menu.SlideOutDirection.Left:
                    {
                        await menu.MenuGrid.TranslateTo(menu.MenuGrid.TranslationX + menu.MenuGrid.Width, menu.MenuGrid.Y, 100, Easing.Linear);
                        break;
                    }
                    case Menu.SlideOutDirection.Right:
                    {
                        await menu.MenuGrid.TranslateTo(menu.MenuGrid.TranslationX - menu.MenuGrid.Width, menu.MenuGrid.Y, 100, Easing.Linear);
                        break;
                    }
                }
            }
        }

        public class Menu
        {
            public enum MenuType
            {
                Standard,
                File,
                Tool,
                Brush
            }

            public enum SlideOutDirection
            {
                Up,
                Down,
                Left,
                Right
            }

            public MenuType Type;
            public bool IsOpen = false;
            public Grid MenuGrid;
            public SlideOutDirection Direction;

            public Menu(MenuType type, bool open, Grid grid, SlideOutDirection direction)
            {
                this.Type = type;
                this.IsOpen = open;
                this.MenuGrid = grid;
                this.Direction = direction;
            }
        }
    }
}
