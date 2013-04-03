using System;
using System.Linq;
using System.Threading.Tasks;
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
            people.Count().Should().Be(2);
        }

        [Test]
        public void SelectMultipleLinesWithMoreProperties()
        {
            var people = ProcedureExecuter.ExecuteReader<Person>("ExecuteReaderWithMultipleLinesMoreProperties");
            people.First().Should().Be.Equals(_john);
            people.Last().Should().Be.Equals(_mario);
        }

        [Test]
        public void SelectMultipleLinesWithLessProperties()
        {
            var people = ProcedureExecuter.ExecuteReader<Person>("ExecuteReaderWithMultipleLinesLessProperties");
            var j = people.First();
            var m = people.Last();

            j.Name.Should().Be(_john.Name);
            j.BirthDate.Should().Be(_john.BirthDate);
            m.Name.Should().Be(_mario.Name);
            m.BirthDate.Should().Be(_mario.BirthDate);

            j.Weight.Should().Be(default(Decimal));
            m.Weight.Should().Be(default(Decimal));
        }

        [Test]
        public void SelectNoLinesWithExactProperties()
        {
            ProcedureExecuter.ExecuteReader<Person>("ExecuteReaderWithNoLinesExactProperties").Any().Should().Be(false);
        }

        [Test]
        public void ExecuteTwice()
        {
            var p1 = ProcedureExecuter.ExecuteReader<Person>("ExecuteReaderWithMultipleLinesMoreProperties");
            var p2 = ProcedureExecuter.ExecuteReader<Person>("ExecuteReaderWithMultipleLinesLessProperties");

            p1.Count().Should().Be(2);
            p2.Count().Should().Be(2);
        }

        [Test]
        public void ExecuteParallel()
        {
            Parallel.For(0, 10, x =>
            {
                var p = ProcedureExecuter.ExecuteReader<Person>("ExecuteReaderWithMultipleLinesMoreProperties");
                p.Count().Should().Be(2);
            });
        }

    }

    class Person
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal Weight { get; set; }
        public bool IsMale { get; set; }
        public int IQ { get; set; }
    }
}
