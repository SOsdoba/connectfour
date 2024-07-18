using ConnectFourSystem;

namespace ConnectFourMaui
{
    public partial class MainPage : ContentPage
    {
        Game game = new();
        List<Button> lstbuttons;

        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = game;
            lstbuttons = new()
            {
                btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9,
                btn10, btn11, btn12, btn13, btn14, btn15, btn16, btn17, btn18, btn19,
                btn20, btn21, btn22, btn23, btn24, btn25, btn26, btn27, btn28, btn29,
                btn30, btn31, btn32, btn33, btn34, btn35
            };
        }
       
        private void StartBtn_Clicked(object sender, EventArgs e)
        {
            string a = listview1.SelectedItem.ToString();
            string b = listview2.SelectedItem.ToString();
            game.StartGame(a,b);
        }

        private void btn_Clicked(object sender, EventArgs e)
        {
            game.TakeSpot(lstbuttons.IndexOf((Button)sender));
        }

        private void listview_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            game.ColorSelected();
        }
    }
}