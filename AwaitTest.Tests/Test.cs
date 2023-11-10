namespace AwaitTest.Tests;

using FluentAssertions;
using Xunit;

public class Test
{
    [Fact]
    public async Task FourSecondsTest()
    {
        var startTs = DateTime.UtcNow;
        await Console.Out.WriteAsync($"init: {DateTime.UtcNow}.");
        var onePlusTwoTask = this.OnePlusTwo();
        var twoPlusTwoTask = this.TwoPlusTwo();
        await Console.Out.WriteAsync($"tasks started: {DateTime.UtcNow}.");

        var result = await onePlusTwoTask + await twoPlusTwoTask;
        var endTs = DateTime.UtcNow;
        await Console.Out.WriteAsync($"summed: {DateTime.UtcNow}.");

        result.Should().Be(7);

        var totalMillis = (endTs - startTs).TotalMilliseconds;

        totalMillis.Should().BeApproximately(4000, 100);
    }

    private async Task<int> GetOne()
    {
        await Task.Delay(1000);
        return 1;
    }

    private async Task<int> GetTwo()
    {
        await Task.Delay(2000);
        return 2;
    }

    private async Task<int> AddTwo(
        int a)
    {
        await Task.Delay(2000);
        return await Task.FromResult(a + 2);
    }

    private async Task<int> OnePlusTwo()
    {
        return await AddTwo(await this.GetOne());
    }

    private async Task<int> TwoPlusTwo()
    {
        return await AddTwo(await this.GetTwo());
    }
}
