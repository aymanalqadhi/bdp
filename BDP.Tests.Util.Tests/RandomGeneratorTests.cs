using BDP.Tests.Utils;

using System.Linq;

using Xunit;

namespace BDP.Tests.Util.Tests;

public class RandomGeneratorTests
{
    #region Constants

    private const int _testStringSize = 0xff;

    #endregion Constants

    #region Tests

    [Fact]
    public void NumOnlyFact()
    {
        var str = RandomGenerator.NextString(_testStringSize, RandomStringOptions.Numeric);

        Assert.Equal(str.Length, _testStringSize);
        Assert.True(str.All(char.IsNumber));
    }

    [Fact]
    public void AlphaFact()
    {
        var str = RandomGenerator.NextString(_testStringSize, RandomStringOptions.Alpha);

        Assert.Equal(str.Length, _testStringSize);
        Assert.True(str.All(char.IsLetter));
    }

    [Fact]
    public void LowerAlphaFact()
    {
        var str = RandomGenerator.NextString(
            _testStringSize,
            RandomStringOptions.Alpha | RandomStringOptions.Lowercase);

        Assert.Equal(str.Length, _testStringSize);
        Assert.True(str.All(char.IsLower));
    }

    [Fact]
    public void UpperAlphaFact()
    {
        var str = RandomGenerator.NextString(
            _testStringSize,
            RandomStringOptions.Alpha | RandomStringOptions.Uppercase);

        Assert.Equal(str.Length, _testStringSize);
        Assert.True(str.All(char.IsUpper));
    }

    [Fact]
    public void MixedAlphaFact()
    {
        var str = RandomGenerator.NextString(
            _testStringSize,
            RandomStringOptions.Alpha | RandomStringOptions.Mixedcase);

        Assert.Equal(str.Length, _testStringSize);
        Assert.True(str.All(c => char.IsUpper(c) || char.IsLower(c)));
    }

    [Fact]
    public void AlphaNumFact()
    {
        var str = RandomGenerator.NextString(_testStringSize, RandomStringOptions.AlphaNumeric);

        Assert.Equal(str.Length, _testStringSize);
        Assert.True(str.All(c => char.IsLetterOrDigit(c)));
    }

    [Fact]
    public void SymbolFact()
    {
        var str = RandomGenerator.NextString(_testStringSize, RandomStringOptions.Symbol);

        Assert.Equal(str.Length, _testStringSize);
        Assert.True(str.All(c => !char.IsLetterOrDigit(c)));
    }

    [Fact]
    public void MixedFact()
    {
        var str = RandomGenerator.NextString(_testStringSize, RandomStringOptions.Mixed);

        Assert.Equal(str.Length, _testStringSize);
    }

    [Fact]
    public void IntFact()
    {
        const int min = 1;
        const int max = 100;

        var value = RandomGenerator.NextInt(min, max);

        Assert.True(value >= min);
        Assert.True(value <= max);
    }

    [Fact]
    public void LongFact()
    {
        const long min = 1L;
        const long max = 100L;

        var value = RandomGenerator.NextLong(min, max);

        Assert.True(value >= min);
        Assert.True(value <= max);
    }

    [Fact]
    public void DoubleFact()
    {
        const double min = 1.0;
        const double max = 100.0;

        var value = RandomGenerator.NextDouble(min, max);

        Assert.True(value >= min);
        Assert.True(value <= max);
    }

    #endregion Tests
}