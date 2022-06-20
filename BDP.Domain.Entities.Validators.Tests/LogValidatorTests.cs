using BDP.Tests.Util;

using FluentValidation.TestHelper;

using Xunit;

namespace BDP.Domain.Entities.Validators.Tests;

public class LogValidatorTests
{
    #region Private fields

    private readonly LogValidator _validator = new();

    #endregion Private fields

    #region Tests

    [Fact]
    public void ValidateMessageShouldSucceed()
    {
        var log = new Log
        {
            Message = RandomGenerator.NextString(0xff)
        };

        Assert.True(_validator.Validate(log).IsValid);
    }

    [Fact]
    public void ValidateMessageShouldFail()
    {
        var log = new Log { Message = "" };

        var result = _validator.TestValidate(log);

        result.ShouldHaveValidationErrorFor(l => l.Message);
    }

    #endregion Tests
}