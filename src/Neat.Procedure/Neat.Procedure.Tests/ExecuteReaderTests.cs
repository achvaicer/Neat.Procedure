using System;
using System.Linq;
using NUnit.Framework;
using SharpTestsEx;

namespace Neat.Procedure.Tests
{
    class ExecuteReaderTests
    {
        private readonly Person _john = new Person
                               {
                                   Name = "John Doe",
                                   BirthDate = new DateTime(1990, 1, 1),
                                   Weight = 98.7M,
                                   IsMale = true,
                                   IQ = 210
                               };

        private readonly Person _mario = new Person
                                   {
                                       Name = "Mario Bros",
                                       BirthDate = new DateTime(1990, 2, 2),
                                       Weight = 94.7M,
                                       IsMale = true,
                                       IQ = 220
                                   };
        [Test]
        public void SelectMultipleLinesWithExactProperties()
        {
            var people = ProcedureExecuter.ExecuteReader<Person>("ExecuteReaderWithMultipleLinesExactProperties");
            people.First().Should().Be.Equals(_john);
            people.Last().Should().Be.Equals(_mario);
        }
    }

    class Person
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal  Weight { get; set; }
        public bool IsMale { get; set; }
        public int IQ { get; set; }
    }
}
