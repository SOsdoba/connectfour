using ConnectFourSystem;

namespace ConnectFourApp
{
    public partial class frmConnectFour : Form
    {
        Game game = new();
        List<Button> lstbuttons;
        List<Button> lsttopbuttons;
       
        public frmConnectFour()
        {
            InitializeComponent();
            btnStart.Click += BtnStart_Click;
            lsttopbuttons = new() { btn0, btn1, btn2, btn3, btn4, btn5};
            foreach (Button btn in tblGrid.Controls) { btn.Enabled = false; }
            lstbuttons = new()
            {
                btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9,
                btn10, btn11, btn12, btn13, btn14, btn15, btn16, btn17, btn18, btn19,
                btn20, btn21, btn22, btn23, btn24, btn25, btn26, btn27, btn28, btn29,
                btn30, btn31, btn32, btn33, btn34, btn35
            };
            lstbuttons.ForEach(b => {
                Spot spot = game.Spots[lstbuttons.IndexOf(b)];
                b.Click += TopButtons_Click;
                b.DataBindings.Add("BackColor", spot, "BackColor");
            });
            lblMessage.DataBindings.Add("Text", game, "GameStatusDescription");
        }

        private void DoTurn(Button btn)
        {
            int num = lstbuttons.IndexOf(btn);
            game.TakeSpot(num);
        }

        private void StartUp()
        {
            foreach (Button btn in lsttopbuttons) { btn.Enabled = true; }
            string a = ddColorA.Text;
            string b = ddColorB.Text;
            game.StartGame(a, b);
        }

        private void BtnStart_Click(object? sender, EventArgs e)
        {
            StartUp();
        }

        private void TopButtons_Click(object? sender, EventArgs e)
        {
            if (sender is Button)
            {
                DoTurn((Button)sender);
            }
        }
    }
}
