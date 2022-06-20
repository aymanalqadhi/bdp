using BDP.Tests.Util;

using FluentValidation.TestHelper;

using Xunit;

namespace BDP.Domain.Entities.Validators.Tests;

public class LogTagValidatorTests
{
    #region Private fields

    private readonly LogTagValidator _validator = new();

    #endregion Private fields

    #region Tests

    [Fact]
    public void ValidateLogTagShouldSucceed()
    {
        var logTag = new LogTag
        {
            Value = RandomGenerator.NextString(0xff)
        };

        Assert.True(_validator.Validate(logTag).IsValid);
    }

    [Fact]
    public void ValidateLogTagShouldFail()
    {
        var logTag = new LogTag { Value = "" };

        var result = _validator.TestValidate(logTag);

        result.ShouldHaveValidationErrorFor(l => l.Value);
    }

    #endregion Tests
}