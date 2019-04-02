namespace Bowling
{
    public interface IBowling
    {
        void Bowl(int score);
        int FrameTotal(int frameNumber);
        int TotalScore();
        GameStatus Status();
    }
}