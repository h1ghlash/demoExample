using demotest.AppDataFile;
namespace DEMOTEST.TEST
{
    [TestClass]
    public class HashPasswordTest
    {
        [TestMethod]
        public void hashPasswordTest()
        {
            //arrange 
            string password = "qwerty123";
            string expected = "3FC0A7ACF087F549AC2B266BAF94B8B1";

            //act
            string hash = HashPassword.hashPassword(password);

            //assert

            Assert.AreEqual(expected, hash);    
            
        }
    }
}