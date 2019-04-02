using System.Collections.Generic;
using System.Linq;

namespace Bowling
{
    public class ScoreCard : IScoreCard
    {
        public string PlayerName { get; set; }
        public GameStatus Status { get; set; } = GameStatus.InPlay;
        private readonly List<Frame> _scores;

        public ScoreCard(string playerName)
        {
            PlayerName = playerName;
            _scores = new List<Frame>( );
        }

        public void Update(int score)
        {
            if(Status == GameStatus.Ended)
                return;

            if (_scores.Count < 10 || _scores.Last().SecondAttempt == null)
            {
                CheckFrame();
                AddScore(score);
                UpdateBonus();
            }
            else
            {
                if (_scores.Last().FirstAttempt + _scores.Last().SecondAttempt >= 10)
                {
                    AddFrameIfNeeded(true);
                    AddScore(score);
                    UpdateBonus();
                }
            }

            UpdateStatus();
        }

        public int TotalScore()
        {
            var total = _scores.Sum(x => (x.FirstAttempt ?? 0) + (x.SecondAttempt == -1 ? 0 : x.SecondAttempt ?? 0) + x.Bonus);
            return total;
        }

        public int GetFrameTotal(int frameNumber)
        {
            var frame = _scores[frameNumber - 1];
            var first = frame.FirstAttempt ?? 0;
            var second = frame.SecondAttempt == -1 ? 0 : frame.SecondAttempt ?? 0;

            return first + second + frame.Bonus;
        }

        private void CheckFrame()
        {
            CheckForStrike();
            AddFrameIfNeeded();
        }

        private void CheckForStrike()
        {
            if (_scores.Count > 0 && _scores.Last().FirstAttempt == 10)
                _scores.Last().SecondAttempt = -1;
        }

        private void UpdateStatus()
        {
            if (_scores.Count > 10)
                Status = GameStatus.Ended;
            else if (_scores.Count == 10 & _scores.Last().FirstAttempt + _scores.Last().SecondAttempt < 10)
                Status = GameStatus.Ended;
        }

        private void AddScore(int score)
        {
            if (_scores.Last().FirstAttempt == null)
                _scores.Last().FirstAttempt = score;
            else
                _scores.Last().SecondAttempt = score;
        }

        private void UpdateBonus()
        {
            if (_scores.Count <= 1 || _scores.Count > 10) return;

            var prevFrame = _scores[_scores.Count - 2];
            if (prevFrame.FirstAttempt == 10)
            {
                prevFrame.Bonus = (_scores.Last().FirstAttempt ?? 0) + (_scores.Last().SecondAttempt ?? 0);
            }
            else if (prevFrame.FirstAttempt + prevFrame.SecondAttempt == 10)
            {
                prevFrame.Bonus = _scores.Last().FirstAttempt ?? 0;
            }
        }

        private void AddFrameIfNeeded(bool addExtraFrame = false)
        {
            if(_scores.Count == 10 && !addExtraFrame)
                return;

            if(_scores.Count == 0 || _scores.Last().SecondAttempt != null)
                _scores.Add(new Frame());
        }
    }

    public enum GameStatus
    {
        InPlay,
        Ended
    }
}
