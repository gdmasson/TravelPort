using TravelPort.Core.Services;

namespace TravelPort.UnitTests;

public class PassportNumberValidation
{
    [Theory]
    [InlineData("PA1234567")]
    [InlineData("LA1234567")]
    public void PassportShouldBeValid(string passportNumber)
    {
        // Act
        var isValid = UserService.ValidatePassportNumber(passportNumber);
        
        // Assert
        Assert.True(isValid);
    }
    
    [Theory]
    [InlineData("XA1234567")]
    [InlineData("LA123457")]
    public void PassportShouldNotBeValid(string passportNumber)
    {
        // Act
        var isValid = UserService.ValidatePassportNumber(passportNumber);
        
        // Assert
        Assert.False(isValid);
    }
}