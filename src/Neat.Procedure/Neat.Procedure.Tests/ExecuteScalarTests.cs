using System;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpTestsEx;

namespace Neat.Procedure.Tests
{
    class ExecuteScalarTests
    {
        
        [Test]
        public void ReturnMultipleValuesInParallel()
        {
            Parallel.For(0, 10, x => ProcedureExecuter.ExecuteScalar("ExecuteScalarWithReturnValueAsInt").Should().Be(42));
        }


        [Test]
        public void ReturnValueAsInt()
        {
            ProcedureExecuter.ExecuteScalar("ExecuteScalarWithReturnValueAsInt").Should().Be(42);
        }

        [Test]
        public void SelectAsInt()
        {
            ProcedureExecuter.ExecuteScalar("ExecuteScalarWithSelectAsInt").Should().Be(42);
        }


        [Test]
        public void SelectAsVarChar()
        {
            ProcedureExecuter.ExecuteScalar("ExecuteScalarWithSelectAsVarChar").Should().Be("The answer is 42");
        }

        [Test]
        public void SelectAsBooleanTrue()
        {
            ProcedureExecuter.ExecuteScalar("ExecuteScalarWithSelectAsBooleanTrue").Should().Be(true);
        }

        [Test]
        public void SelectAsBooleanFalse()
        {
            ProcedureExecuter.ExecuteScalar("ExecuteScalarWithSelectAsBooleanFalse").Should().Be(false);
        }

        [Test]
        public void SelectAsDateTime()
        {
            ProcedureExecuter.ExecuteScalar("ExecuteScalarWithSelectAsDateTime").Should().Be(new DateTime(2012, 1, 1));
        }

        [Test]
        public void SelectWithMultipleColumnsAllAsInt()
        {
            ProcedureExecuter.ExecuteScalar("ExecuteScalarWithSelectMultipleColumnsAsInt").Should().Be(42);
        }

        [Test]
        public void SelectWithMutlipleColumnsAllAsVarchar()
        {
            ProcedureExecuter.CommandTimeout = 90;
            ProcedureExecuter.ExecuteScalar("ExecuteScalarWithSelectMultipleColumnsAsVarChar").Should().Be("The answer is 42");
        }
    }
}
