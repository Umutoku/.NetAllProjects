
namespace BenefitsXAMLMAUI
{
    public partial class MainPage : ContentPage
    {
        public const int FontSize = 42;
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

    }

    public class GlobalFontSizeExtension : IMarkupExtension
    {
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return MainPage.FontSize;
        }
    }
    [ContentProperty("Member")]
    public class StaticExtension : IMarkupExtension
    {
        public string Member { get; set; }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }
    }
}
