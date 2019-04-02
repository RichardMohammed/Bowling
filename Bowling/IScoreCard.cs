namespace Bowling
{
    public interface IScoreCard
    {
        string PlayerName { get; set; }
        GameStatus Status { get; set; }
        void Update(int score);
        int TotalScore();
        int GetFrameTotal(int frameNumber);
    }
}