using ConnectFourSystem;
using System.Drawing;

namespace ConnectFourTest
{
    public class Tests
    {
        [Test]
        public void TestStartGame()
        {
            Game game = new();
            game.StartGame();
            string msg = $"Game status = {game.GameStatus.ToString()} and the current turn = {game.CurrentTurn.ToString()} and the number of spots = {game.Spots.Count}";
            Assert.IsTrue(game.GameStatus == Game.GameStatusEnum.Playing && game.CurrentTurn == Game.TurnEnum.A && game.Spots.Count == 36, msg);
            TestContext.WriteLine(msg);
        }

        [Test]
        public void TestTakeSpot()
        {
            Game game = new();
            game.StartGame();
            game.TakeSpot(4);
            string msg = $"Spot 34 = {game.Spots[34].BackColor} and current turn = {game.CurrentTurn.ToString()}";
            Assert.IsTrue(game.Spots[34].BackColor == game.PlayerAColor && game.CurrentTurn == Game.TurnEnum.B, msg);
            TestContext.WriteLine(msg);
        }

        [TestCase(0,1,0,1,0,4,0)]
        [TestCase(0, 3, 0, 1, 0, 4, 0)]
        [TestCase(4, 3, 4, 1, 4, 3, 4)]
        [TestCase(0,0, 1, 1, 2, 4, 3)]
        public void TestWinner(int a0, int b0, int a1, int b1, int a2, int b2, int a3)
        {
            Game game = new();
            game.StartGame();
            game.TakeSpot(a0);
            game.TakeSpot(b0);
            game.TakeSpot(a1);
            game.TakeSpot(b1);
            game.TakeSpot(a2);
            game.TakeSpot(b2);
            game.TakeSpot(a3);
            string msg = $"Game status = {game.GameStatus}. The winnder is {game.Winner.ToString()}!";
            Assert.IsTrue(game.GameStatus == Game.GameStatusEnum.Winner && game.Winner == Game.TurnEnum.A, msg);
            TestContext.WriteLine(msg);
        }

        [Test]
        public void TestTie()
        {
            Game game = new();
            game.StartGame();
            game.TakeSpot(0);
            game.TakeSpot(0);
            game.TakeSpot(0);
            game.TakeSpot(0);
            game.TakeSpot(0);
            game.TakeSpot(0);
            game.TakeSpot(2);
            game.TakeSpot(2);
            game.TakeSpot(2);
            game.TakeSpot(2);
            game.TakeSpot(2);
            game.TakeSpot(2);
            game.TakeSpot(3);
            game.TakeSpot(1);
            game.TakeSpot(1);
            game.TakeSpot(1);
            game.TakeSpot(1);
            game.TakeSpot(1);
            game.TakeSpot(1);
            game.TakeSpot(4);
            game.TakeSpot(4);
            game.TakeSpot(4);
            game.TakeSpot(4);
            game.TakeSpot(4);
            game.TakeSpot(4);
            game.TakeSpot(3);
            game.TakeSpot(3);
            game.TakeSpot(3);
            game.TakeSpot(3);
            game.TakeSpot(3);
            game.TakeSpot(5);
            game.TakeSpot(5);
            game.TakeSpot(5);
            game.TakeSpot(5);
            game.TakeSpot(5);
            game.TakeSpot(5);
            string msg = $"Game Status = {game.GameStatus}. Winner = {game.Winner.ToString()}";
            Assert.IsTrue(game.GameStatus == Game.GameStatusEnum.Tie, msg);
            TestContext.WriteLine(msg);
        }
    }
}