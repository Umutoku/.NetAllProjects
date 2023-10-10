using FirstMaui.ViewModel;
using Plugin.Maui.Audio;

namespace FirstMaui
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private readonly IAudioManager _audioManager;

        public MainPage(MainViewModel mvm, IAudioManager audioManager)
        {
            InitializeComponent();
            BindingContext= mvm;
            _audioManager = audioManager;
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            var player = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("rain.mp3"));
            player.Play();

           // count++;

            //if (count == 1)
            //    CounterBtn.Text = $"Clicked {count} time";
            //else
            //    CounterBtn.Text = $"Clicked {count} times";

            //SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private async void DrawingView_DrawingLineCompleted(object sender, CommunityToolkit.Maui.Core.DrawingLineCompletedEventArgs e)
        {
            var stream = await myDrawing.GetImageStream(150,150);
            rightImage.Source = ImageSource.FromStream(()=>stream);
        }
    }

}
