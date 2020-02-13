using Xamarin.Forms;

namespace Painter
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
            this.MainPage = new Views.PainterView();
        }

        protected override void OnStart()
        {
            //
        }

        protected override void OnSleep()
        {
            //
        }

        protected override void OnResume()
        {
            //
        }
    }
}
