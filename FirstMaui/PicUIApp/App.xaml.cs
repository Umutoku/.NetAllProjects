using PicUIApp.Views;

namespace PicUIApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new DashBoardView();
        }
    }
}
