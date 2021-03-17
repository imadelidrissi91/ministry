using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcExercise.Models.Repositories
{
    public class SqlEmployeeRepository : ICompanyRepository<Employee>
    {
        private readonly AppDbContext _context;
        public SqlEmployeeRepository(AppDbContext context)
        {
            this._context = context;
        }
       

        

        public Employee Get(int id)
        {
            var employee = this._context.MyEmployees.SingleOrDefault(emp => emp.Id ==id);
            return employee;
        }
        public IEnumerable<Employee> GetEntities()
        {
            return this._context.MyEmployees;
        }
       
        public void Add(Employee entity)
        {
            if (entity.Sexe == "man")
            {
                entity.Photo = "OIP.jpg";
            }
            else
            {
                entity.Photo = "women1.png";
            }
            this._context.MyEmployees.Add(entity);
            _context.SaveChanges();
        }

        public Employee Update(Employee entityChanges)
        {
            if (entityChanges.Sexe== "man")
            {
                entityChanges.Photo = "OIP.jpg";
            }
            else
            {
                entityChanges.Photo = "women1.png";
            }
            var emp = this._context.Attach(entityChanges);
            emp.State = EntityState.Modified;
            _context.SaveChanges();
            return entityChanges;
        }
        public Employee Delete(int id)
        {
            var employee = Get(id);
            if (employee != null)
            {
                this._context.MyEmployees.Remove(employee);
                this._context.SaveChanges();
            }
            return employee;
        }

    }
}
