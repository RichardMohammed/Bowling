namespace Bowling
{
    public class Bowling : IBowling
    {
        private readonly IScoreCard _playerScores;

        public Bowling(string playerName)
        {
          _playerScores = new ScoreCard(playerName);
        }

        public void Bowl(int score)
        {
            _playerScores.Update(score);
        }

        public int FrameTotal(int frameNumber)
        {
            return _playerScores.GetFrameTotal(frameNumber);
        }

        public int TotalScore()
        {
            return _playerScores.TotalScore();
        }

        public GameStatus Status()
        {
            return _playerScores.Status;
        }
    }
}
