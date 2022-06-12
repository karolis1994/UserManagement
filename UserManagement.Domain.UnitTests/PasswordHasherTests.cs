using Xunit;

namespace UserManagement.Domain.UnitTests
{
    public class PasswordHasherTests
    {
        [Theory]
        [InlineData("test")]
        [InlineData("12345697898")]
        [InlineData("slapciausiasSlaptazodis")]
        [InlineData("65656565656")]
        [InlineData("123Testas1234656")]
        public void Hash_IsConsistent(string password)
        {
            //Arrange
            var salt = SaltGenerator.Generate();

            //Act
            var hashedPassword = PasswordHasher.Hash(password, salt);
            var hashedPassword2 = PasswordHasher.Hash(password, salt);

            //Assert
            Assert.Equal(hashedPassword, hashedPassword2);
        }

        [Theory]
        [InlineData("test")]
        [InlineData("12345697898")]
        [InlineData("slapciausiasSlaptazodis")]
        [InlineData("65656565656")]
        [InlineData("123Testas1234656")]
        public void Verify(string password)
        {
            //Arrange
            var salt = SaltGenerator.Generate();
            var hashedPassword = PasswordHasher.Hash(password, salt);

            //Act
            var isValid = PasswordHasher.Verify(password, salt, hashedPassword);

            //Assert
            Assert.True(isValid);
        }
    }
}
