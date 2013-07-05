using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpTestsEx;

namespace Neat.Procedure.Tests
{
    class ConversionTests
    {
        [Test]
        public void IntegerShouldBeConvertedAsDecimal()
        {
            var x = ProcedureExecuter.ExecuteReader<Int32AsDecimal>("ExecuteReaderWithMultipleLinesExactProperties").First();
            x.IQ.Should().Be(210D);
        }

        [Test]
        public void DateTimeShouldBeConvertedAsString()
        {
            var x = ProcedureExecuter.ExecuteReader<DateTimeAsString>("ExecuteReaderWithMultipleLinesExactProperties").First();
            x.BirthDate.Should().Be("1/1/1990 12:00:00 AM");
        }

        [Test]
        public void DecimalShouldBeConvertedAsInteger()
        {
            var x = ProcedureExecuter.ExecuteReader<DecimalAsInt32>("ExecuteReaderWithMultipleLinesExactProperties").First();
            x.Weight.Should().Be(99);
        }

        [Test]
        public void DecimalShouldBeConvertedAsString()
        {
            var x = ProcedureExecuter.ExecuteReader<DecimalAsString>("ExecuteReaderWithMultipleLinesExactProperties").First();
            x.Weight.Should().Be("98.7");
        }

        [Test]
        public void Integer0ShouldBeConvertedAsFalseBoolean()
        {
            var x = ProcedureExecuter.ExecuteReader<Int32AsBoolean>("ExecuteReaderWithIntegerAsFalseBoolean").First();
            x.IQ.Should().Be(false);
        }

        [Test]
        public void Integer1ShouldBeConvertedAsTrueBoolean()
        {
            var x = ProcedureExecuter.ExecuteReader<Int32AsBoolean>("ExecuteReaderWithIntegerAsTrueBoolean").First();
            x.IQ.Should().Be(true);
        }

        class Int32AsBoolean
        {
            public bool IQ { get; set; }
        }

        class DecimalAsString
        {
            public String Weight { get; set; } 
        }

        class DecimalAsInt32
        {
            public int Weight { get; set; } 
        }

        class DateTimeAsString
        {
            public string BirthDate { get; set; } 
        }

        class Int32AsDecimal
        {
            public decimal IQ { get; set; }
        }
    }
}
