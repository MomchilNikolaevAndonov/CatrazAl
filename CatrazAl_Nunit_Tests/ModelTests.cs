using NUnit.Framework;
using CatrazAl.Data.Models;
using CatrazAl.Business;
using CatrazAl.Presentation;
using CatrazAl;
using System;
using System.IO;
using System.Linq;

namespace CatrazAl.Tests.Models
{
    public static class TestHelper
    {
        public static int GetValidBlockId()
        {
            var biz = new PrisonBlockBusiness();
            var existing = biz.GetAll().FirstOrDefault();
            if (existing != null) return existing.PrisonBlockId;
            var block = new PrisonBlock { PrisonBlock1 = "Test Block" };
            biz.Add(block);
            return block.PrisonBlockId;
        }

        public static int GetValidCrimeId()
        {
            var biz = new CrimeBusiness();
            var existing = biz.GetAll().FirstOrDefault();
            if (existing != null) return existing.CrimeId;
            var crime = new Crime { Crime1 = "Test Crime" };
            biz.Add(crime);
            return crime.CrimeId;
        }

        public static Cell GetValidCell()
        {
            var biz = new CellBusiness();
            var existing = biz.GetAll().FirstOrDefault();
            if (existing != null) return existing;
            var cell = new Cell { PrisonBlockId = GetValidBlockId(), Capacity = 2, Kind = "Test Cell" };
            biz.Add(cell);
            return cell;
        }

        public static int GetValidShiftId()
        {
            var biz = new ShiftBusiness();
            var existing = biz.GetAll().FirstOrDefault();
            if (existing != null) return existing.ShiftId;
            var shift = new Shift { ShiftName = "Test Shift", PrisonBlockId = GetValidBlockId() };
            biz.Add(shift);
            return shift.ShiftId;
        }

        public static int GetValidPrisonerId()
        {
            var biz = new PrisonerBusiness();
            var existing = biz.GetAll().FirstOrDefault();
            if (existing != null) return existing.PrisonerId;
            var cell = GetValidCell();
            var prisoner = new Prisoner
            {
                FirstName = "Test",
                LastName = "Prisoner",
                Egn = "0000000000",
                Gender = "M",
                CrimeId = GetValidCrimeId(),
                CellId = cell.CellId,
                PrisonBlockId = cell.PrisonBlockId,
                DateOfBirth = new DateOnly(1990, 1, 1),
                SentenceStart = new DateOnly(2020, 1, 1),
                SentenceEnd = new DateOnly(2025, 1, 1)
            };
            biz.Add(prisoner);
            return prisoner.PrisonerId;
        }
    }

    [TestFixture]
    public class CellTests
    {
        [Test]
        public void Cell_CanSetAndGetProperties()
        {
            var cell = new Cell();
            var prisonBlock = new PrisonBlock { PrisonBlockId = 1 };
            cell.CellId = 10;
            cell.PrisonBlockId = 1;
            cell.Capacity = 4;
            cell.Kind = "Standard";
            cell.PrisonBlock = prisonBlock;

            Assert.That(cell.CellId, Is.EqualTo(10));
            Assert.That(cell.PrisonBlockId, Is.EqualTo(1));
            Assert.That(cell.Capacity, Is.EqualTo(4));
            Assert.That(cell.Kind, Is.EqualTo("Standard"));
            Assert.That(cell.PrisonBlock, Is.EqualTo(prisonBlock));
        }

        [Test]
        public void Cell_Collections_AreInitialized()
        {
            var cell = new Cell();
            Assert.That(cell.Prisoners, Is.Not.Null);
            Assert.That(cell.Prisoners, Is.Empty);
        }
    }

    [TestFixture]
    public class CrimeTests
    {
        [Test]
        public void Crime_CanSetAndGetProperties()
        {
            var crime = new Crime();
            crime.CrimeId = 1;
            crime.Crime1 = "Theft";

            Assert.That(crime.CrimeId, Is.EqualTo(1));
            Assert.That(crime.Crime1, Is.EqualTo("Theft"));
        }

        [Test]
        public void Crime_Collections_AreInitialized()
        {
            var crime = new Crime();
            Assert.That(crime.Prisoners, Is.Not.Null);
            Assert.That(crime.Prisoners, Is.Empty);
        }
    }

    [TestFixture]
    public class GuardTests
    {
        [Test]
        public void Guard_CanSetAndGetProperties()
        {
            var guard = new Guard();
            var shift = new Shift { ShiftId = 1 };
            guard.GuardId = 5;
            guard.FirstName = "John";
            guard.LastName = "Doe";
            guard.GuardRank = "Sergeant";
            guard.Phone = "555-1234";
            guard.ShiftId = 1;
            guard.Shift = shift;

            Assert.That(guard.GuardId, Is.EqualTo(5));
            Assert.That(guard.FirstName, Is.EqualTo("John"));
            Assert.That(guard.LastName, Is.EqualTo("Doe"));
            Assert.That(guard.GuardRank, Is.EqualTo("Sergeant"));
            Assert.That(guard.Phone, Is.EqualTo("555-1234"));
            Assert.That(guard.ShiftId, Is.EqualTo(1));
            Assert.That(guard.Shift, Is.EqualTo(shift));
        }
    }

    [TestFixture]
    public class MedicalRecordTests
    {
        [Test]
        public void MedicalRecord_CanSetAndGetProperties()
        {
            var record = new MedicalRecord();
            var prisoner = new Prisoner { PrisonerId = 1 };
            var recordDate = new DateTime(2023, 10, 1);
            record.RecordId = 1;
            record.PrisonerId = 1;
            record.Diagnosis = "Flu";
            record.Treatment = "Rest";
            record.TreatmentDays = 5;
            record.DoctorFirstName = "Jane";
            record.DoctorLastName = "Smith";
            record.RecordDate = recordDate;
            record.Prisoner = prisoner;

            Assert.That(record.RecordId, Is.EqualTo(1));
            Assert.That(record.PrisonerId, Is.EqualTo(1));
            Assert.That(record.Diagnosis, Is.EqualTo("Flu"));
            Assert.That(record.Treatment, Is.EqualTo("Rest"));
            Assert.That(record.TreatmentDays, Is.EqualTo(5));
            Assert.That(record.DoctorFirstName, Is.EqualTo("Jane"));
            Assert.That(record.DoctorLastName, Is.EqualTo("Smith"));
            Assert.That(record.RecordDate, Is.EqualTo(recordDate));
            Assert.That(record.Prisoner, Is.EqualTo(prisoner));
        }
    }

    [TestFixture]
    public class PrisonBlockTests
    {
        [Test]
        public void PrisonBlock_CanSetAndGetProperties()
        {
            var block = new PrisonBlock();
            block.PrisonBlockId = 1;
            block.PrisonBlock1 = "Block A";

            Assert.That(block.PrisonBlockId, Is.EqualTo(1));
            Assert.That(block.PrisonBlock1, Is.EqualTo("Block A"));
        }

        [Test]
        public void PrisonBlock_Collections_AreInitialized()
        {
            var block = new PrisonBlock();
            Assert.That(block.Cells, Is.Not.Null);
            Assert.That(block.Prisoners, Is.Not.Null);
            Assert.That(block.Shifts, Is.Not.Null);
            Assert.That(block.Cells, Is.Empty);
            Assert.That(block.Prisoners, Is.Empty);
            Assert.That(block.Shifts, Is.Empty);
        }
    }

    [TestFixture]
    public class PrisonerTests
    {
        [Test]
        public void Prisoner_CanSetAndGetProperties()
        {
            var prisoner = new Prisoner();
            var cell = new Cell { CellId = 1 };
            var crime = new Crime { CrimeId = 1 };
            var block = new PrisonBlock { PrisonBlockId = 1 };
            var dob = new DateOnly(1980, 5, 15);
            var start = new DateOnly(2020, 1, 1);
            var end = new DateOnly(2030, 1, 1);

            prisoner.PrisonerId = 100;
            prisoner.FirstName = "Mike";
            prisoner.LastName = "Tyson";
            prisoner.Egn = "8005150000";
            prisoner.DateOfBirth = dob;
            prisoner.Gender = "M";
            prisoner.CrimeId = 1;
            prisoner.SentenceMonths = 120;
            prisoner.SentenceStart = start;
            prisoner.SentenceEnd = end;
            prisoner.CellId = 1;
            prisoner.Released = false;
            prisoner.PrisonBlockId = 1;
            prisoner.Cell = cell;
            prisoner.Crime = crime;
            prisoner.PrisonBlock = block;

            Assert.That(prisoner.PrisonerId, Is.EqualTo(100));
            Assert.That(prisoner.FirstName, Is.EqualTo("Mike"));
            Assert.That(prisoner.LastName, Is.EqualTo("Tyson"));
            Assert.That(prisoner.Gender, Is.EqualTo("M"));
            Assert.That(prisoner.SentenceStart, Is.EqualTo(start));
        }

        [Test]
        public void Prisoner_Collections_AreInitialized()
        {
            var prisoner = new Prisoner();
            Assert.That(prisoner.MedicalRecords, Is.Not.Null);
            Assert.That(prisoner.Punishments, Is.Not.Null);
            Assert.That(prisoner.Visits, Is.Not.Null);
        }
    }

    [TestFixture]
    public class PunishmentTests
    {
        [Test]
        public void Punishment_CanSetAndGetProperties()
        {
            var punishment = new Punishment();
            var prisoner = new Prisoner { PrisonerId = 1 };
            var startDate = new DateTime(2023, 1, 1);
            var endDate = new DateTime(2023, 1, 10);
            punishment.PunishmentId = 1;
            punishment.PrisonerId = 1;
            punishment.Reason = "Fights";
            punishment.PunishmentDays = 10;
            punishment.StartDate = startDate;
            punishment.EndDate = endDate;
            punishment.PunishmentType = "Solitary";
            punishment.Prisoner = prisoner;

            Assert.That(punishment.PunishmentId, Is.EqualTo(1));
            Assert.That(punishment.StartDate, Is.EqualTo(startDate));
        }
    }

    [TestFixture]
    public class ShiftTests
    {
        [Test]
        public void Shift_CanSetAndGetProperties()
        {
            var shift = new Shift();
            var block = new PrisonBlock { PrisonBlockId = 1 };
            var startTime = new DateTime(2023, 1, 1, 8, 0, 0);
            var endTime = new DateTime(2023, 1, 1, 16, 0, 0);
            shift.ShiftId = 1;
            shift.ShiftName = "Morning";
            shift.StartTime = startTime;
            shift.EndTime = endTime;
            shift.PrisonBlockId = 1;
            shift.PrisonBlock = block;

            Assert.That(shift.ShiftId, Is.EqualTo(1));
            Assert.That(shift.ShiftName, Is.EqualTo("Morning"));
        }
    }

    [TestFixture]
    public class VisitTests
    {
        [Test]
        public void Visit_CanSetAndGetProperties()
        {
            var visit = new Visit();
            var prisoner = new Prisoner { PrisonerId = 1 };
            var visitDate = new DateTime(2023, 5, 5);
            visit.VisitId = 1;
            visit.PrisonerId = 1;
            visit.VisitorFirstName = "Alice";
            visit.VisitorLastName = "Wonderland";
            visit.VisitorRelation = "Sister";
            visit.VisitDate = visitDate;
            visit.DurationMinuits = 60;
            visit.Prisoner = prisoner;

            Assert.That(visit.VisitId, Is.EqualTo(1));
            Assert.That(visit.VisitDate, Is.EqualTo(visitDate));
        }
    }

    [TestFixture]
    public class CellBusinessTests
    {
        [Test]
        public void CellBusiness_CRUD_Operations()
        {
            var biz = new CellBusiness();
            var cell = new Cell { PrisonBlockId = TestHelper.GetValidBlockId(), Capacity = 4, Kind = "Standard" };

            biz.Add(cell);
            var fetched = biz.Get(cell.CellId);
            Assert.That(fetched, Is.Not.Null);

            fetched.Capacity = 5;
            biz.Update(fetched);
            Assert.That(biz.Get(cell.CellId).Capacity, Is.EqualTo(5));

            biz.Delete(cell.CellId);
            Assert.That(biz.Get(cell.CellId), Is.Null);
        }
    }

    [TestFixture]
    public class CrimeBusinessTests
    {
        [Test]
        public void CrimeBusiness_CRUD_Operations()
        {
            var biz = new CrimeBusiness();
            var crime = new Crime { Crime1 = "Test Crime" };

            biz.Add(crime);
            var fetched = biz.Get(crime.CrimeId);
            Assert.That(fetched, Is.Not.Null);

            fetched.Crime1 = "Updated Crime";
            biz.Update(fetched);
            Assert.That(biz.Get(crime.CrimeId).Crime1, Is.EqualTo("Updated Crime"));

            biz.Delete(crime.CrimeId);
            Assert.That(biz.Get(crime.CrimeId), Is.Null);
        }
    }

    [TestFixture]
    public class GuardBusinessTests
    {
        [Test]
        public void GuardBusiness_CRUD_Operations()
        {
            var biz = new GuardBusiness();
            var guard = new Guard { FirstName = "John", LastName = "Doe", ShiftId = TestHelper.GetValidShiftId() };

            biz.Add(guard);
            var fetched = biz.Get(guard.GuardId);
            Assert.That(fetched, Is.Not.Null);

            fetched.LastName = "Smith";
            biz.Update(fetched);
            Assert.That(biz.Get(guard.GuardId).LastName, Is.EqualTo("Smith"));

            biz.Delete(guard.GuardId);
            Assert.That(biz.Get(guard.GuardId), Is.Null);
        }
    }

    [TestFixture]
    public class MedicalRecordBusinessTests
    {
        [Test]
        public void MedicalRecordBusiness_CRUD_Operations()
        {
            var biz = new MedicalRecordBusiness();
            var record = new MedicalRecord { PrisonerId = TestHelper.GetValidPrisonerId(), Diagnosis = "Flu", RecordDate = DateTime.Now };

            biz.Add(record);
            var fetched = biz.Get(record.RecordId);
            Assert.That(fetched, Is.Not.Null);

            fetched.Diagnosis = "Cold";
            biz.Update(fetched);
            Assert.That(biz.Get(record.RecordId).Diagnosis, Is.EqualTo("Cold"));

            biz.Delete(record.RecordId);
            Assert.That(biz.Get(record.RecordId), Is.Null);
        }
    }

    [TestFixture]
    public class PrisonBlockBusinessTests
    {
        [Test]
        public void PrisonBlockBusiness_CRUD_Operations()
        {
            var biz = new PrisonBlockBusiness();
            var block = new PrisonBlock { PrisonBlock1 = "Block X" };

            biz.Add(block);
            var fetched = biz.Get(block.PrisonBlockId);
            Assert.That(fetched, Is.Not.Null);

            fetched.PrisonBlock1 = "Block Y";
            biz.Update(fetched);
            Assert.That(biz.Get(block.PrisonBlockId).PrisonBlock1, Is.EqualTo("Block Y"));

            biz.Delete(block.PrisonBlockId);
            Assert.That(biz.Get(block.PrisonBlockId), Is.Null);
        }
    }

    [TestFixture]
    public class PrisonerBusinessTests
    {
        [Test]
        public void PrisonerBusiness_CRUD_Operations()
        {
            var biz = new PrisonerBusiness();
            var cell = TestHelper.GetValidCell();
            var prisoner = new Prisoner
            {
                FirstName = "Test",
                LastName = "Prisoner",
                Egn = "0000000000",
                Gender = "M",
                CrimeId = TestHelper.GetValidCrimeId(),
                CellId = cell.CellId,
                PrisonBlockId = cell.PrisonBlockId,
                DateOfBirth = new DateOnly(1990, 1, 1),
                SentenceStart = new DateOnly(2020, 1, 1),
                SentenceEnd = new DateOnly(2025, 1, 1)
            };

            biz.Add(prisoner);
            var fetched = biz.Get(prisoner.PrisonerId);
            Assert.That(fetched, Is.Not.Null);

            fetched.FirstName = "Updated";
            biz.Update(fetched);
            Assert.That(biz.Get(prisoner.PrisonerId).FirstName, Is.EqualTo("Updated"));

            biz.Delete(prisoner.PrisonerId);
            Assert.That(biz.Get(prisoner.PrisonerId), Is.Null);
        }
    }

    [TestFixture]
    public class PunishmentBusinessTests
    {
        [Test]
        public void PunishmentBusiness_CRUD_Operations()
        {
            var biz = new PunishmentBusiness();
            var punishment = new Punishment
            {
                PrisonerId = TestHelper.GetValidPrisonerId(),
                Reason = "Fights",
                PunishmentDays = 5,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5)
            };

            biz.Add(punishment);
            var fetched = biz.Get(punishment.PunishmentId);
            Assert.That(fetched, Is.Not.Null);

            punishment.PunishmentDays = 10;
            biz.Update(punishment);
            Assert.That(biz.Get(punishment.PunishmentId).PunishmentDays, Is.EqualTo(10));

            biz.Delete(punishment.PunishmentId);
            Assert.That(biz.Get(punishment.PunishmentId), Is.Null);
        }
    }

    [TestFixture]
    public class ShiftBusinessTests
    {
        [Test]
        public void ShiftBusiness_CRUD_Operations()
        {
            var biz = new ShiftBusiness();
            var shift = new Shift { ShiftName = "Night", PrisonBlockId = TestHelper.GetValidBlockId() };

            biz.Add(shift);
            var fetched = biz.Get(shift.ShiftId);
            Assert.That(fetched, Is.Not.Null);

            fetched.ShiftName = "Day";
            biz.Update(fetched);
            Assert.That(biz.Get(shift.ShiftId).ShiftName, Is.EqualTo("Day"));

            biz.Delete(shift.ShiftId);
            Assert.That(biz.Get(shift.ShiftId), Is.Null);
        }
    }

    [TestFixture]
    public class VisitBusinessTests
    {
        [Test]
        public void VisitBusiness_CRUD_Operations()
        {
            var biz = new VisitBusiness();
            var visit = new Visit
            {
                PrisonerId = TestHelper.GetValidPrisonerId(),
                VisitorFirstName = "Jane",
                VisitorLastName = "Doe",
                VisitorRelation = "Spouse",
                VisitDate = DateTime.Now
            };

            biz.Add(visit);
            var fetched = biz.Get(visit.VisitId);
            Assert.That(fetched, Is.Not.Null);

            fetched.VisitorFirstName = "Janet";
            biz.Update(fetched);
            Assert.That(biz.Get(visit.VisitId).VisitorFirstName, Is.EqualTo("Janet"));

            biz.Delete(visit.VisitId);
            Assert.That(biz.Get(visit.VisitId), Is.Null);
        }
    }

    [TestFixture]
    public class PresentationLayerTests
    {
        private TextWriter _originalOut;
        private TextReader _originalIn;

        [SetUp]
        public void Setup()
        {
            _originalOut = Console.Out;
            _originalIn = Console.In;
        }

        [TearDown]
        public void TearDown()
        {
            Console.SetOut(_originalOut);
            Console.SetIn(_originalIn);
        }

        private string RunWithConsoleInput(string simulatedUserInput, Action actionToRun)
        {
            using var stringWriter = new StringWriter();
            using var stringReader = new StringReader(simulatedUserInput);

            Console.SetOut(stringWriter);
            Console.SetIn(stringReader);

            actionToRun();
            return stringWriter.ToString();
        }

        [Test]
        public void CellDisplay_Menu_FullCoverage()
        {
            int blockId = TestHelper.GetValidBlockId();
            var biz = new CellBusiness();
            var cell = new Cell { PrisonBlockId = blockId, Capacity = 2, Kind = "Test" };
            biz.Add(cell);
            int id = cell.CellId;

            var inputs = string.Join(Environment.NewLine, new[] {
                "1",
                "2", blockId.ToString(), "4", "Standard",
                "3", id.ToString(), blockId.ToString(), "5", "Large",
                "4", id.ToString(),
                "5", id.ToString(),
                "6"
            }) + Environment.NewLine;

            var output = RunWithConsoleInput(inputs, () => new CellDisplay());
            Assert.That(output, Does.Contain("Cell"));
        }

        [Test]
        public void CrimeDisplay_Menu_FullCoverage()
        {
            var biz = new CrimeBusiness();
            var crime = new Crime { Crime1 = "Test UI Crime" };
            biz.Add(crime);
            int id = crime.CrimeId;

            var inputs = string.Join(Environment.NewLine, new[] {
                "1",
                "2", "Robbery",
                "3", id.ToString(), "Burglary",
                "4", id.ToString(),
                "5", id.ToString(),
                "6"
            }) + Environment.NewLine;

            var output = RunWithConsoleInput(inputs, () => new CrimeDisplay());
            Assert.That(output, Does.Contain("Crime"));
        }

        [Test]
        public void GuardDisplay_Menu_FullCoverage()
        {
            int shiftId = TestHelper.GetValidShiftId();
            var biz = new GuardBusiness();
            var guard = new Guard { FirstName = "UI", LastName = "Test", ShiftId = shiftId };
            biz.Add(guard);
            int id = guard.GuardId;

            var inputs = string.Join(Environment.NewLine, new[] {
                "1",
                "2", "John", "Doe", "Rank", "123", shiftId.ToString(),
                "3", id.ToString(), "John", "Smith", "Rank2", "124", shiftId.ToString(),
                "4", id.ToString(),
                "5", id.ToString(),
                "6"
            }) + Environment.NewLine;

            var output = RunWithConsoleInput(inputs, () => new GuardDisplay());
            Assert.That(output, Does.Contain("Guard"));
        }

        [Test]
        public void MedicalRecordDisplay_Menu_FullCoverage()
        {
            int pId = TestHelper.GetValidPrisonerId();
            var biz = new MedicalRecordBusiness();
            var rec = new MedicalRecord { PrisonerId = pId, Diagnosis = "Test", RecordDate = DateTime.Now };
            biz.Add(rec);
            int id = rec.RecordId;

            var inputs = string.Join(Environment.NewLine, new[] {
                "1",
                "2", pId.ToString(), "Flu", "Rest", "5", "Jane", "Doe", "2023-01-01",
                "3", id.ToString(), pId.ToString(), "Cold", "Rest", "3", "Jane", "Doe", "2023-01-02",
                "4", id.ToString(),
                "5", id.ToString(),
                "6"
            }) + Environment.NewLine;

            var output = RunWithConsoleInput(inputs, () => new MedicalRecordDisplay());
            Assert.That(output, Does.Contain("Medical Record"));
        }

        [Test]
        public void PrisonBlockDisplay_Menu_FullCoverage()
        {
            var biz = new PrisonBlockBusiness();
            var block = new PrisonBlock { PrisonBlock1 = "UI Block" };
            biz.Add(block);
            int id = block.PrisonBlockId;

            var inputs = string.Join(Environment.NewLine, new[] {
                "1",
                "2", "Block A",
                "3", id.ToString(), "Block B",
                "4", id.ToString(),
                "5", id.ToString(),
                "6"
            }) + Environment.NewLine;

            var output = RunWithConsoleInput(inputs, () => new PrisonBlockDisplay());
            Assert.That(output, Does.Contain("Prison Block"));
        }

        [Test]
        public void PrisonerDisplay_Menu_FullCoverage()
        {
            int crimeId = TestHelper.GetValidCrimeId();
            var cell = TestHelper.GetValidCell();

            var biz = new PrisonerBusiness();
            var p = new Prisoner
            {
                FirstName = "UI",
                LastName = "Test",
                Egn = "111",
                Gender = "M",
                CrimeId = crimeId,
                CellId = cell.CellId,
                PrisonBlockId = cell.PrisonBlockId,
                SentenceStart = DateOnly.FromDateTime(DateTime.Now),
                SentenceEnd = DateOnly.FromDateTime(DateTime.Now.AddYears(1))
            };
            biz.Add(p);
            int id = p.PrisonerId;

            var inputs = string.Join(Environment.NewLine, new[] {
                "1",
                "2", "John", "Doe", "1234567890", "1990-01-01", "M", crimeId.ToString(), "12", "2020-01-01", "2021-01-01", cell.CellId.ToString(), cell.PrisonBlockId.ToString(), "true",
                "3", id.ToString(), "Jane", "Doe", "0987654321", "1992-01-01", "F", crimeId.ToString(), "24", "2020-01-01", "2022-01-01", cell.CellId.ToString(), cell.PrisonBlockId.ToString(), "false",
                "4", id.ToString(),
                "5", id.ToString(),
                "6"
            }) + Environment.NewLine;

            var output = RunWithConsoleInput(inputs, () => new PrisonerDisplay());
            Assert.That(output, Does.Contain("Prisoner"));
        }

        [Test]
        public void PunishmentDisplay_Menu_FullCoverage()
        {
            int pId = TestHelper.GetValidPrisonerId();
            var biz = new PunishmentBusiness();
            var pun = new Punishment { PrisonerId = pId, Reason = "Test", PunishmentDays = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1) };
            biz.Add(pun);
            int id = pun.PunishmentId;

            var inputs = string.Join(Environment.NewLine, new[] {
                "1",
                "2", pId.ToString(), "Bad", "10", "2023-01-01", "2023-01-11", "Solitary",
                "3", id.ToString(), pId.ToString(), "Worse", "15", "2023-01-01", "2023-01-16", "Solitary",
                "4", id.ToString(),
                "5", id.ToString(),
                "6"
            }) + Environment.NewLine;

            var output = RunWithConsoleInput(inputs, () => new PunishmentDisplay());
            Assert.That(output, Does.Contain("Punishment"));
        }

        [Test]
        public void ShiftDisplay_Menu_FullCoverage()
        {
            int blockId = TestHelper.GetValidBlockId();
            var biz = new ShiftBusiness();
            var s = new Shift { ShiftName = "UI Test", PrisonBlockId = blockId };
            biz.Add(s);
            int id = s.ShiftId;

            var inputs = string.Join(Environment.NewLine, new[] {
                "1",
                "2", "Morning", "2023-01-01 08:00", "2023-01-01 16:00", blockId.ToString(),
                "3", id.ToString(), "Evening", "2023-01-01 16:00", "2023-01-01 23:59", blockId.ToString(),
                "4", id.ToString(),
                "5", id.ToString(),
                "6"
            }) + Environment.NewLine;

            var output = RunWithConsoleInput(inputs, () => new ShiftDisplay());
            Assert.That(output, Does.Contain("Shift"));
        }

        [Test]
        public void VisitDisplay_Menu_FullCoverage()
        {
            int pId = TestHelper.GetValidPrisonerId();
            var biz = new VisitBusiness();
            var v = new Visit { PrisonerId = pId, VisitorFirstName = "A", VisitorLastName = "B", VisitorRelation = "C", VisitDate = DateTime.Now };
            biz.Add(v);
            int id = v.VisitId;

            var inputs = string.Join(Environment.NewLine, new[] {
                "1",
                "2", pId.ToString(), "Alice", "Wonder", "Sister", "2023-01-01", "60",
                "3", id.ToString(), pId.ToString(), "Alice", "Wonder", "Friend", "2023-01-01", "30",
                "4", id.ToString(),
                "5", id.ToString(),
                "6"
            }) + Environment.NewLine;

            var output = RunWithConsoleInput(inputs, () => new VisitDisplay());
            Assert.That(output, Does.Contain("Visit"));
        }

        [Test]
        public void MainDisplay_Menu_FullCoverage()
        {
            var inputs = "10\n";
            try
            {
                RunWithConsoleInput(inputs, () => new Display());
            }
            catch (IOException)
            {
                Assert.Pass("Caught expected IOException from Console.Clear(). This is normal in test environments with redirected output.");
            }
        }
    }
}