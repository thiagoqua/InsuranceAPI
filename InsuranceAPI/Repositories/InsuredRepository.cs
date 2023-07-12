using InsuranceAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Data.Common;

namespace InsuranceAPI.Repositories {
    public interface IInsuredRepository {
        public List<Insured> getAll();
        public IQueryable<Insured> getAllQueryables();
        public List<Insured> search(string query);
        public Insured? findById(long id);
        public void create(Insured insured);
        public void createMultiple(List<Insured> insures);
        public void update(Insured insured);
        public void delete(long id);
        public void commit();
    }

    public class InsuredComparer : IComparer<Insured> {
        public int Compare(Insured? x, Insured? y) {
            if(x == null || y == null) 
                return 1;

            return x.Lastname.CompareTo(y.Lastname);
        }
    }

    public class InsuredRepository : IInsuredRepository{
        private DbInsuranceContext _context;

        public InsuredRepository(DbInsuranceContext _dbcontext) {
            _context = _dbcontext;
        }

        public List<Insured> getAll() {
            return getAllQueryables().ToList();
        }

        public IQueryable<Insured> getAllQueryables() {
            return _context.Insureds
                .Include(ins => ins.AddressNavigation)
                .Include(ins => ins.ProducerNavigation)
                .Include(ins => ins.Phones)
                .Include(ins => ins.CompanyNavigation)
                .OrderBy(ins => ins.Lastname);
        }

        public List<Insured> search(string query) {
            return (from ins in _context.Insureds
                    where  ((ins.Firstname + " " + ins.Lastname).Contains(query) ||
                            (ins.Lastname + " " + ins.Firstname).Contains(query) ||
                             ins.Firstname.Contains(query) || 
                             ins.Lastname.Contains(query))
                    select ins)
                    .Include(ins => ins.AddressNavigation)
                    .Include(ins => ins.ProducerNavigation)
                    .Include(ins => ins.Phones)
                    .Include(ins => ins.CompanyNavigation)
                    .ToList();
        }

        public Insured? findById(long id) {
            return _context.Insureds
                    .Include(ins => ins.AddressNavigation)
                    .Include(ins => ins.ProducerNavigation)
                    .Include(ins => ins.CompanyNavigation)
                    .Include(ins => ins.Phones)
                    .FirstOrDefault(i => i.Id == id);
        }

        public void create(Insured newOne) {
            _context.Insureds.Add(newOne);
        }

        public void commit() {
            _context.SaveChanges();
        }

        public void update(Insured insured) {
            _context.Insureds.Update(insured);
        }

        public void delete(long id) {
            Insured? inCuestion = findById(id);
            if(inCuestion != null){
                inCuestion.Phones.Clear();
                _context.Insureds.Remove(inCuestion);
            }
        }

        public void createMultiple(List<Insured> insureds) {
            foreach(Insured insured in insureds) {
                create(insured);
            }
        }
    }
}
