using System.ComponentModel;
using System.Drawing;
using System.Windows.Input;

using Xamarin.Forms;

namespace Painter.Models
{
    public class PainterModel : INotifyPropertyChanged
    {
        public Views.PainterView view;
        public Bitmap image;
        public Bitmap Image
        {
            get
            {
                return this.image;
            }
            set
            {
                this.image = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Image"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        // Menus
        public ICommand FileMenuCommand { get; set; }
        public ICommand ToolMenuCommand { get; set; }
        public ICommand BrushMenuCommand { get; set; }
        // File Menu
        public ICommand NewImageCommand { get; set; }
        public ICommand LoadImageCommand { get; set; }
        public ICommand SaveImageCommand { get; set; }
        // Tool Menu
        public ICommand SelectPaintBrushCommand { get; set; }
        public ICommand SelectEraserCommand { get; set; }
        // Brush Menu
        // TODO: BRUSH MENU COMMANDS

        public PainterModel(Views.PainterView view)
        {
            this.view = view;
            this.FileMenuCommand = new Command(this.OnFileMenu);
            this.ToolMenuCommand = new Command(this.OnToolMenu);
            this.BrushMenuCommand = new Command(this.OnBrushMenu);
            this.NewImageCommand = new Command(this.OnNewImage);
            this.LoadImageCommand = new Command(this.OnLoadImage);
            this.SaveImageCommand = new Command(this.OnSaveImage);
            this.SelectPaintBrushCommand = new Command(this.OnSelectPaintBrush);
            this.SelectEraserCommand = new Command(this.OnSelectEraser);
        }

        public void OnFileMenu()
        {
            this.view.AnimateMenu(this.view.Menus[Views.PainterView.Menu.MenuType.File], true);
        }

        public void OnToolMenu()
        {
            this.view.AnimateMenu(this.view.Menus[Views.PainterView.Menu.MenuType.Tool], true);
        }

        public void OnBrushMenu()
        {
            this.view.AnimateMenu(this.view.Menus[Views.PainterView.Menu.MenuType.Brush], true);
        }

        public void OnSelectPaintBrush()
        {
            // Change brush to paint brush
        }

        public void OnSelectEraser()
        {
            // Change brush to eraser
        }

        public void OnNewImage()
        {
            this.SendError("New Image Not Implemented Yet...");
        }

        public void OnLoadImage()
        {
            this.SendError("Load Image Not Implemented Yet...");
        }

        public void OnSaveImage()
        {
            this.SendError("Save Image Not Implemented Yet...");
        }

        public void SendError(string errorMessage)
        {
            MessagingCenter.Send(this, "Error", errorMessage);
        }
    }
}
