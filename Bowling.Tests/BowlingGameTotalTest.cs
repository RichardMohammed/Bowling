using Xunit;
using Xunit.Abstractions;

namespace Bowling.Tests
{
    public class BowlingGameTotalTest
    {
        private readonly IBowling _bowling;
        private readonly ITestOutputHelper _output;
        public BowlingGameTotalTest(ITestOutputHelper output)
        {
            _bowling = new Bowling("Player 1");
            _output = output;
        }

        [Fact]
        public void BowlingGameTotalWithBonus()
        {
            _bowling.Bowl(1);
            _bowling.Bowl(4);
            Assert.True(_bowling.TotalScore() == 5);

            _bowling.Bowl(4);
            _bowling.Bowl(5);
            Assert.True(_bowling.TotalScore() == 14);


            _bowling.Bowl(6);
            _bowling.Bowl(4);

            _bowling.Bowl(5);
            Assert.True(_bowling.TotalScore() - 5 == 29);
            _bowling.Bowl(5);

            _bowling.Bowl(10);
            //
            Assert.True(_bowling.TotalScore() - 10 == 49);

            _bowling.Bowl(0);
            _bowling.Bowl(1);
            Assert.True(_bowling.TotalScore() == 61);

            _bowling.Bowl(7);
            _bowling.Bowl(3);

            _bowling.Bowl(6);
            Assert.True(_bowling.TotalScore() - 6 == 77);
            _bowling.Bowl(4);

            _bowling.Bowl(10);
            //
            _bowling.Bowl(2);
            _bowling.Bowl(8);
            Assert.True(_bowling.TotalScore() - 10 == 117);
            _bowling.Bowl(6);

            _output.WriteLine(_bowling.TotalScore().ToString());
            Assert.True(_bowling.TotalScore() == 133);
        }
    }
}
