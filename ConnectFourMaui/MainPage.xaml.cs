using ConnectFourSystem;

namespace ConnectFourMaui
{
    public partial class MainPage : ContentPage
    {
        Game activegame;
        List<Game> lstgame = new() { new Game(), new Game(), new Game() };
        List<Button> lstbuttons;

        public MainPage()
        {
            InitializeComponent();
            lstgame.ForEach(g => g.ScoreChanged += G_ScoreChanged);
            Game1Rb.BindingContext = lstgame[0];
            Game2Rb.BindingContext = lstgame[1];
            Game3Rb.BindingContext = lstgame[2];
            activegame = lstgame[0];
            this.BindingContext = activegame;
            lstbuttons = new()
            {
                btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9,
                btn10, btn11, btn12, btn13, btn14, btn15, btn16, btn17, btn18, btn19,
                btn20, btn21, btn22, btn23, btn24, btn25, btn26, btn27, btn28, btn29,
                btn30, btn31, btn32, btn33, btn34, btn35
            };
        }

        private void G_ScoreChanged(object sender, EventArgs e)
        {
            ScoreLbl.Text = Game.Score;
        }

        private void StartBtn_Clicked(object sender, EventArgs e)
        {
            if(activegame.GameStatus == Game.GameStatusEnum.NotStarted)
            {
                if (Picker1.SelectedItem != null && Picker2.SelectedItem != null)
                {
                    string a = Picker1.SelectedItem.ToString();
                    string b = Picker2.SelectedItem.ToString();
                    activegame.StartGame(a, b);
                }
            }
            else
            {
                activegame.StopGame();
            }
            
        }

        private void btn_Clicked(object sender, EventArgs e)
        {
            activegame.TakeSpot(lstbuttons.IndexOf((Button)sender));
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            activegame.ColorSelected();
        }

        private void Game_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.IsChecked && rb.BindingContext != null)
            {
                activegame = (Game)rb.BindingContext;
                this.BindingContext = activegame;
            }
        }
    }
}