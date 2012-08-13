using NUnit.Framework;

namespace Neat.Procedure.Tests
{
    [SetUpFixture]
    public class ExecuteTests
    {
        [SetUp]
        public void CreateDataBase()
        {
            DataBaseHelper.CreateDataBase();
        }

        [TearDown]
        public void DropDataBase()
        {
            DataBaseHelper.DropDataBase();
        }
    }
}
