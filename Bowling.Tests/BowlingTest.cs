using Xunit;
using Xunit.Abstractions;

namespace Bowling.Tests
{
    public class BowlingTest
    {
        private readonly IBowling _bowling;
        private readonly ITestOutputHelper _output;

        public BowlingTest(ITestOutputHelper output)
        {
            _bowling = new Bowling("Player 1");
            _output = output;
        }

        [Theory]
        [InlineData(1, 4, 5)]
        public void BasicFrameScoreIsSumOfTwoTries(int firstAttempt, int secondAttempt, int expected)
        {
            _bowling.Bowl(firstAttempt);
            _bowling.Bowl(secondAttempt);

            var score = _bowling.FrameTotal(1);
            Assert.True(score == expected);
        }

        [Theory]
        [InlineData(5, 5, 4, 14)]
        public void PreviousFrameScoreIsSumOfTwoTriesAndSpareBonus(int prevFrameFirstAttempt, int prevFrameSecondAttempt, int curFrameFirstAttempt, int expected)
        {
            _bowling.Bowl(prevFrameFirstAttempt);
            _bowling.Bowl(prevFrameSecondAttempt);
            _bowling.Bowl(curFrameFirstAttempt);

            var score = _bowling.FrameTotal(1);

            _output.WriteLine(score.ToString());
            Assert.True(score == expected);
        }

        [Theory]
        [InlineData(10, 4, 4, 18)]
        public void PreviousFrameScoreIsSumOfTwoTriesAndStrikeBonus(int prevFrameFirstAttempt, int curFrameFirstAttempt, int curFrameSecondAttempt, int expected)
        {
            _bowling.Bowl(prevFrameFirstAttempt);
            _bowling.Bowl(curFrameFirstAttempt);
            _bowling.Bowl(curFrameSecondAttempt);

            var score = _bowling.FrameTotal(1);

            _output.WriteLine(score.ToString());
            Assert.True(score == expected);
        }

        [Fact]
        public void GameEndsAt10thFrame()
        {
            for (int i = 0; i < 10; i++)
            {
                _bowling.Bowl(1);
                _bowling.Bowl(1);
            }

            _output.WriteLine(_bowling.TotalScore().ToString());
            _output.WriteLine(_bowling.Status().ToString());

            Assert.True(_bowling.Status() == GameStatus.Ended);
        }

        [Fact]
        public void GameEndsAt11thFrameWhenBOnusIn10th()
        {
            for (int i = 0; i < 9; i++)
            {
                _bowling.Bowl(1);
                _bowling.Bowl(1);
            }
            _bowling.Bowl(5);
            _bowling.Bowl(5);

            _bowling.Bowl(6);


            _output.WriteLine(_bowling.TotalScore().ToString());
            _output.WriteLine(_bowling.Status().ToString());

            Assert.True(_bowling.Status() == GameStatus.Ended);
        }

        [Fact]
        public void GameStopsScoringAfter10thFrame()
        {
            for (int i = 0; i < 20; i++)
            {
                _bowling.Bowl(1);
                _bowling.Bowl(1);
            }

            _output.WriteLine(_bowling.TotalScore().ToString());
            _output.WriteLine(_bowling.Status().ToString());

            Assert.True(_bowling.Status() == GameStatus.Ended);
            Assert.True(_bowling.TotalScore() == 20);
        }

        [Fact]
        public void GameStopsScoringAfter11thFrame()
        {

            for (int i = 0; i < 9; i++)
            {
                _bowling.Bowl(1);
                _bowling.Bowl(1);
            }
            _bowling.Bowl(5);
            _bowling.Bowl(5);

            _bowling.Bowl(6);

            for (int i = 0; i < 20; i++)
            {
                _bowling.Bowl(1);
                _bowling.Bowl(1);
            }

            _output.WriteLine(_bowling.TotalScore().ToString());
            _output.WriteLine(_bowling.Status().ToString());

            Assert.True(_bowling.Status() == GameStatus.Ended);
            Assert.True(_bowling.TotalScore() == 34);
        }

    }
}
