using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace ConnectFourSystem
{
    public class Game : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public enum GameStatusEnum { NotStarted, Playing, Winner, Tie }
        public enum TurnEnum { None, A, B }

        List<List<Spot>> lstcolumns = new();
        List<Spot> lsttopbuttons = new();
        List<List<Spot>> lstwinningsets = new();

        GameStatusEnum _gamestatus = GameStatusEnum.NotStarted;
        TurnEnum _currentturn = TurnEnum.None;

        public Game()
        {
            for (int i = 0; i < 36; i++)
            {
                ;
                this.Spots.Add(new Spot());
            }
            lsttopbuttons = new() { this.Spots[0], this.Spots[1], this.Spots[2], this.Spots[3], this.Spots[4], this.Spots[5] };
            lstcolumns = new()
            {
                new() { this.Spots[30], this.Spots[24], this.Spots[18], this.Spots[12], this.Spots[6],this.Spots[0] },
                new() { this.Spots[31], this.Spots[25], this.Spots[19], this.Spots[13], this.Spots[7],this.Spots[1] },
                new() { this.Spots[32], this.Spots[26], this.Spots[20], this.Spots[14], this.Spots[8],this.Spots[2] },
                new() { this.Spots[33], this.Spots[27], this.Spots[21], this.Spots[15], this.Spots[9],this.Spots[3] },
                new() { this.Spots[34], this.Spots[28], this.Spots[22], this.Spots[16], this.Spots[10],this.Spots[4] },
                new() { this.Spots[35], this.Spots[29], this.Spots[23], this.Spots[17], this.Spots[11],this.Spots[5] }
            };
            lstwinningsets = new()
            {
                new () {this.Spots[0], this.Spots[1], this.Spots[2], this.Spots[3] },
                new () {this.Spots[1], this.Spots[2], this.Spots[3], this.Spots[4] },
                new () {this.Spots[2], this.Spots[3], this.Spots[4], this.Spots[5] },
                new () {this.Spots[6], this.Spots[7], this.Spots[8], this.Spots[9] },
                new () {this.Spots[7], this.Spots[8], this.Spots[9], this.Spots[10] },
                new () {this.Spots[8], this.Spots[9], this.Spots[10], this.Spots[11] },
                new () {this.Spots[12], this.Spots[13], this.Spots[14], this.Spots[15] },
                new () {this.Spots[13], this.Spots[14], this.Spots[15], this.Spots[16] },
                new () {this.Spots[14], this.Spots[15], this.Spots[16], this.Spots[17] },
                new () {this.Spots[18], this.Spots[19], this.Spots[20], this.Spots[21] },
                new () {this.Spots[19], this.Spots[20], this.Spots[21], this.Spots[22] },
                new () {this.Spots[20], this.Spots[21], this.Spots[22], this.Spots[23] },
                new () {this.Spots[24], this.Spots[25], this.Spots[26], this.Spots[27] },
                new () {this.Spots[25], this.Spots[26], this.Spots[27], this.Spots[28] },
                new () {this.Spots[26], this.Spots[27], this.Spots[28], this.Spots[29] },
                new () {this.Spots[30], this.Spots[31], this.Spots[32], this.Spots[33] },
                new () {this.Spots[31], this.Spots[32], this.Spots[33], this.Spots[34] },
                new () {this.Spots[32], this.Spots[33], this.Spots[34], this.Spots[35] },
                new () {this.Spots[0], this.Spots[6], this.Spots[12], this.Spots[18] },
                new () {this.Spots[6], this.Spots[12], this.Spots[18], this.Spots[24] },
                new () {this.Spots[12], this.Spots[18], this.Spots[24], this.Spots[30] },
                new () {this.Spots[1], this.Spots[7], this.Spots[13], this.Spots[19] },
                new () {this.Spots[7], this.Spots[13], this.Spots[19], this.Spots[25] },
                new () {this.Spots[13], this.Spots[19], this.Spots[25], this.Spots[31] },
                new () {this.Spots[2], this.Spots[8], this.Spots[14], this.Spots[20] },
                new () {this.Spots[8], this.Spots[14], this.Spots[20], this.Spots[26] },
                new () {this.Spots[14], this.Spots[20], this.Spots[26], this.Spots[32] },
                new () {this.Spots[3], this.Spots[9], this.Spots[15], this.Spots[21] },
                new () {this.Spots[9], this.Spots[15], this.Spots[21], this.Spots[27] },
                new () {this.Spots[15], this.Spots[21], this.Spots[27], this.Spots[33] },
                new () {this.Spots[4], this.Spots[10], this.Spots[16], this.Spots[22] },
                new () {this.Spots[10], this.Spots[16], this.Spots[22], this.Spots[28] },
                new () {this.Spots[16], this.Spots[22], this.Spots[28], this.Spots[34] },
                new () {this.Spots[5], this.Spots[11], this.Spots[17], this.Spots[23] },
                new () {this.Spots[11], this.Spots[17], this.Spots[23], this.Spots[29] },
                new () {this.Spots[17], this.Spots[23], this.Spots[29], this.Spots[35] },
            };
            this.Spots.ForEach(b => b.BackColor = SpotNotStartedColor);
        }

        public List<Spot> Spots { get; private set; } = new();
        public GameStatusEnum GameStatus
        {
            get => _gamestatus;
            private set
            {
                _gamestatus = value;
                this.InvokePropertyChanged();
                this.InvokePropertyChanged("GameStatusDescription");
            }
        }

        public TurnEnum CurrentTurn
        {
            get => _currentturn;
            private set
            {
                _currentturn = value;
                this.InvokePropertyChanged("GameStatusDescription");
            }
        }

        public string GameStatusDescription { get => this.GameStatus.ToString() == GameStatusEnum.NotStarted.ToString() ? "Click Start to begin the game." : this.GameStatus.ToString() == GameStatusEnum.Playing.ToString() ? $"{this.GameStatus.ToString()} Current Turn:  {this.CurrentTurn.ToString()}" : this.GameStatus.ToString() == GameStatusEnum.Tie.ToString() ? "No winner. Start over." : $"Great Job! Player " + this.CurrentTurn.ToString() + " is the winner."; }
        public TurnEnum Winner { get; private set; }

        public System.Drawing.Color SpotNotStartedColor { get; set; } = System.Drawing.Color.DarkGray;
        public System.Drawing.Color GameStartColor { get; set; } = System.Drawing.Color.Ivory;
        public System.Drawing.Color SpotWinnerColor { get; set; } = System.Drawing.Color.Red;
        public System.Drawing.Color SpotTieColor { get; set; } = System.Drawing.Color.Gray;
        public System.Drawing.Color PlayerAColor { get; private set; }
        public System.Drawing.Color PlayerBColor { get; set; }
        private System.Drawing.Color PlayerColor { get => this.CurrentTurn.ToString() == TurnEnum.A.ToString() ? PlayerAColor : PlayerBColor; }

        public void StartGame(String a = default, string b = default)
        {
            if (a == default) a = "Navy"; 
            if (b == default) b = "HotPink";
            this.Spots.ForEach(spot => spot.BackColor = this.GameStartColor);
            this.GameStatus = GameStatusEnum.Playing;
            this.CurrentTurn = TurnEnum.A;
            PlayerAColor = Color.FromName(a);
            PlayerBColor = Color.FromName(b);
        }

        public void TakeSpot(int spotnum)
        {
            Spot spot = this.Spots[spotnum];

            if (this.GameStatus == GameStatusEnum.Playing && lsttopbuttons.Contains(spot) && spot.BackColor == GameStartColor)
            {
                var lst = lstcolumns.First(l => l.Contains(spot));
                Spot firstspot = lst.First(b => b.BackColor == GameStartColor);
                firstspot.BackColor = this.PlayerColor;

                lstwinningsets.ForEach(l => DetectWinner(l));

                if (this.GameStatus == GameStatusEnum.Playing)
                {
                    DetectTie();
                    if (this.CurrentTurn == TurnEnum.A)
                    {
                        this.CurrentTurn = TurnEnum.B;
                    }
                    else
                    {
                        this.CurrentTurn = TurnEnum.A;
                    }
                }
            }
        }

        private void DetectWinner(List<Spot> lst)
        {
            if (lst.Count(b => b.BackColor == (this.CurrentTurn == TurnEnum.A ? PlayerAColor : PlayerBColor)) == lst.Count())
            {
                this.GameStatus = GameStatusEnum.Winner;
                this.Winner = this.CurrentTurn;
                lst.ForEach(b => b.BackColor = this.SpotWinnerColor);
            }
        }
        private void DetectTie()
        {
            if (this.Spots.Count(b => b.BackColor == GameStartColor) == 0)
            {
                this.GameStatus = GameStatusEnum.Tie;
                this.Spots.ForEach(b => b.BackColor = this.SpotTieColor);
            }
        }

        private void InvokePropertyChanged([CallerMemberName] string propertyname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}