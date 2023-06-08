using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectFourApp
{
    public partial class frmConnectFour : Form
    {
        List<Button> lsttopbuttons;
        List<List<Button>> lstwinningsets;
        List<List<Button>> lstcolumns;
        List<Button> lstbuttons;

        enum GameStatusEnum { NotStarted, Playing, Tie, Winner}
        GameStatusEnum gamestatus = GameStatusEnum.NotStarted;

        enum TurnEnum { A, B};
        TurnEnum currentturn = TurnEnum.A;

        public frmConnectFour()
        {
            InitializeComponent();
            btnStart.Click += BtnStart_Click;
            ddColorA.Click += DdColor_Click;
            ddColorB.Click += DdColor_Click;
            lsttopbuttons = new() { button1, button2, button3, button4, button5, button6};
            lsttopbuttons.ForEach(b => b.Click += TopButtons_Click);
            foreach (Button btn in tblGrid.Controls) { btn.Enabled = false; }
            lstbuttons = new()
            {
                button1, button2, button3, button4, button5, button6, button7, button8, button9, button10,
                button11, button12, button13, button14, button15, button16, button17, button18, button19, button20,
                button21, button22, button23, button24, button25, button26, button27, button28, button29, button30,
                button31, button32, button33, button34, button35, button36};
            lstwinningsets = new()
            {
                new() { button1, button2, button3, button4},
                new() {button2, button3, button4, button5},
                new() {button3, button4, button5, button6},
                new() {button7, button8, button9, button10},
                new() {button8, button9, button10, button11},
                new() {button9, button10, button11, button12},
                new() {button13, button14, button15, button16},
                new() {button14, button15, button16, button17},
                new() {button15, button16, button17, button18},
                new() {button19, button20, button21, button22},
                new() {button20, button21, button22, button23},
                new() {button21, button22, button23, button24},
                new() {button25, button26, button27, button28},
                new() {button26, button27, button28, button29},
                new() {button27, button28, button29, button30},
                new() {button31, button32, button33, button34},
                new() {button32, button33, button34, button35},
                new() {button33, button34, button35, button36},

                new() {button1, button7, button13, button19},
                new() {button7, button13, button19, button25},
                new() {button13, button19, button25, button31},
                new() {button2, button8, button14, button20},
                new() {button8, button14, button20, button26},
                new() {button14, button20, button26, button32},
                new() {button3, button9, button15, button21},
                new() {button9, button15, button21, button27},
                new() {button15, button21, button27, button33},
                new() {button4, button10, button16, button22},
                new() {button10, button16, button22, button28},
                new() {button16, button22, button28, button34},
                new() {button5, button11, button17, button23},
                new() {button11, button17, button23, button29},
                new() {button17, button23, button29, button35},
                new() {button6, button12, button18, button24},
                new() {button12, button18, button24, button30},
                new() {button18, button24, button30, button36}
            };
            lstcolumns = new()
            {
                new() {button31, button25, button19, button13, button7, button1},
                new() {button32, button26, button20, button14, button8, button2},
                new() {button33, button27, button21, button15, button9, button3},
                new() {button34, button28, button22, button16, button10, button4},
                new() {button35, button29, button23, button17, button11, button5},
                new() {button36, button30, button24, button18, button12, button6}
            };
            DisplayGameStatus();
        }

        private void ButtonColor(Button btn)
        {
            Color c;
            if (currentturn == TurnEnum.A) 
            {
                c = Color.FromName(ddColorA.Text);
            } 
            else 
            {
                c = Color.FromName(ddColorB.Text);
            };
            btn.BackColor = c;
        }

        private void ColorofButtons(List<Button> lst)
        {
            Color c = Color.Ivory;
            switch (gamestatus)
            {
                case GameStatusEnum.Tie:
                    c = Color.Gray;
                    break;
                case GameStatusEnum.Winner:
                    c = Color.Yellow;
                    break;
            }
            lst.ForEach(b => b.BackColor = c);
        }

        private void DoTurn(Button btn)
        {
            if(btn.BackColor == Color.Ivory && gamestatus == GameStatusEnum.Playing)
            {
                var lst = lstcolumns.First(l => l.Contains(btn));
                Button firstbutton = lst.First(b => b.BackColor == Color.Ivory);
                ButtonColor(firstbutton);

                lstwinningsets.ForEach(l => DetectWinner(l));
                
                if(gamestatus == GameStatusEnum.Playing)
                {
                    DetectTie();
                    if (currentturn == TurnEnum.A)
                    {
                        currentturn = TurnEnum.B;
                    }
                    else
                    {
                        currentturn = TurnEnum.A;
                    }
                }
            }
            DisplayGameStatus();
        }
        private void DetectWinner(List<Button> lst)
        {
            if (lst.Count(b => b.BackColor == (currentturn == TurnEnum.A ? Color.FromName(ddColorA.Text) : Color.FromName(ddColorB.Text))) == lst.Count())
            {
                foreach (Button btn in lsttopbuttons) { btn.Enabled = false; }
                gamestatus = GameStatusEnum.Winner;
                ColorofButtons(lst);
            }
        }

        private void DetectTie()
        {
            if (lstbuttons.Count(b => b.BackColor == Color.Ivory) == 0)
            {
                gamestatus = GameStatusEnum.Tie;
                ColorofButtons(lstbuttons);
            }
        }

        private void DisplayGameStatus()
        {
            string msg = "Click Start to begin the game.";

            switch (gamestatus)
            {
                case GameStatusEnum.Playing:
                    msg = "Current Turn: " + currentturn.ToString();
                    break;
                case GameStatusEnum.Tie:
                    msg = "No winner. Start over.";
                    break;
                case GameStatusEnum.Winner:
                    msg = "Great Job! Player " + currentturn.ToString() + " is the winner.";
                    break;
            }
            lblMessage.Text = msg;
        }

        
        private void StartUp()
        {
            gamestatus = GameStatusEnum.Playing;
            foreach (Button btn in tblGrid.Controls) { btn.BackColor = Color.Ivory; }
            foreach (Button btn in lsttopbuttons) { btn.Enabled = true; }
            currentturn = TurnEnum.A;
            DisplayGameStatus();
        }

        private void DdColor_Click(object? sender, EventArgs e)
        {
            gamestatus = GameStatusEnum.NotStarted;
            foreach (Button btn in tblGrid.Controls) { btn.BackColor = Color.Ivory; }
            DisplayGameStatus();
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
