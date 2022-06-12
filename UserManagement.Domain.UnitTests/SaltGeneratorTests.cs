using System.Collections.Generic;
using Xunit;

namespace UserManagement.Domain.UnitTests
{
    public class SaltGeneratorTests
    {
        [Fact]
        public void Generate_DoesntGoAboveLimit()
        {
            //Arrange
            List<byte[]> byteList = new List<byte[]>();

            //Act
            for (int i = 0; i < 10000; i++)
            {
                byteList.Add(SaltGenerator.Generate());
            }

            //Assert
            Assert.All(byteList, e => Assert.True(e.Length <= SaltGenerator.SaltSize));
        }
    }
}
