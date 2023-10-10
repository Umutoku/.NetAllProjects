using TodoApp.FirebaseUser;
using TodoApp.Views;

namespace TodoApp
{
    public partial class MainPage : ContentPage
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = this;
        }

        private void Button_ClickedSign(object sender, EventArgs e)
        {
            if (stackLIn.IsVisible)
            {
                stackLIn.IsVisible = false;
                stackLJoin.IsVisible = true;
            }
            else
            {
                stackLIn.IsVisible = true;
                stackLJoin.IsVisible = false;
            }
        }

        private async void Button_ClickedSignUpAsync(object sender, EventArgs e)
        {
           bool result = await FirebaseAuthData.SignUp(UserName,Email, Password);
            if(result)
            {
                DisplayAlert("Info", "Test Success", "Ok");
                stackLIn.IsVisible = true;
                stackLJoin.IsVisible = false;

            }
        }

        private async void Button_ClickedSignInAsync(object sender, EventArgs e)
        {
            bool result = await FirebaseAuthData.SignIn(Email, Password);
            if(result)
            {
                DisplayAlert("Info", "SignIn", "Ok");
                await Navigation.PushAsync(new TodoListPage());
            }
            else
            {
                DisplayAlert("Info", "Password or Email is Wrong", "Ok");

            }
        }
    }

}
