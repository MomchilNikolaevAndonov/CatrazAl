using CatrazAl.Data;
using CatrazAl.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CatrazAl.Business
{
    public class MedicalRecordBusiness
    {
        private PrisonDbContext prisonContext;

        public List<MedicalRecord> GetAll()
        {
            using (prisonContext = new PrisonDbContext())
            {
                return prisonContext.MedicalRecords.ToList();
            }
        }

        public MedicalRecord Get(int id)
        {
            using (prisonContext = new PrisonDbContext())
            {
                return prisonContext.MedicalRecords.Find(id);
            }
        }

        public void Add(MedicalRecord medicalRecord)
        {
            using (prisonContext = new PrisonDbContext())
            {
                prisonContext.MedicalRecords.Add(medicalRecord);
                prisonContext.SaveChanges();
            }
        }

        public void Update(MedicalRecord medicalRecord)
        {
            using (prisonContext = new PrisonDbContext())
            {
                var item = prisonContext.MedicalRecords.Find(medicalRecord.RecordId);
                if (item != null)
                {
                    prisonContext.Entry(item).CurrentValues.SetValues(medicalRecord);
                    prisonContext.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (prisonContext = new PrisonDbContext())
            {
                var medicalRecord = prisonContext.MedicalRecords.Find(id);
                if (medicalRecord != null)
                {
                    prisonContext.MedicalRecords.Remove(medicalRecord);
                    prisonContext.SaveChanges();
                }
            }
        }
    }
}