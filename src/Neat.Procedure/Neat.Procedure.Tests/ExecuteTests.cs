using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpTestsEx;

namespace Neat.Procedure.Tests
{
    [TestFixture]
    public abstract class ExecuteTests
    {
        [TestFixtureSetUp]
        public void CreateDataBase()
        {
            DataBaseHelper.CreateDataBase();
        }

        [TestFixtureTearDown]
        public void DropDataBase()
        {
            DataBaseHelper.DropDataBase();
        }
    }
}
