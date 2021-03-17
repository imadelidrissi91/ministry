using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcExercise.Models.Repositories
{
    public class EmployeeRepository:ICompanyRepository<Employee>
    {
        List<Employee> employees;
        public EmployeeRepository()
        {
            employees = new List<Employee>();
            employees.Add(new Employee() {Id=1,Name="Imad",Sexe="man",Email="imad@gmail.com", Photo = "OIP.jpg", Departement =Departement.IT});
            employees.Add(new Employee() { Id = 2, Name = "hind", Sexe = "woman", Email = "hind@gmail.com",Photo="women1.png", Departement = Departement.Networking });
            employees.Add(new Employee() { Id = 3, Name = "mehdi", Sexe = "man", Email = "fahd2@gmail.com", Photo = "OIP.jpg", Departement = Departement.HR});
            employees.Add(new Employee() { Id = 4, Name = "fouad", Sexe = "man", Email = "fahd3@gmail.com", Photo = "OIP.jpg", Departement = Departement.Finnance });
        }

        public Employee Get(int id)
        {
            return employees.SingleOrDefault(emp => emp.Id == id);
        }

        public IEnumerable<Employee> GetEntities()
        {
            return employees;
        }

        public void Add(Employee employee)
        {
            int id = employees.Max(emp => emp.Id) ;
            employee.Id = id + 1;
            employee.Departement = Departement.IT;
            if (employee.Sexe=="man")
            {
                employee.Photo = "OIP.jpg";
            }
            else
            {
                employee.Photo = "women1.png";
            }
            
            employees.Add(employee);
        }



        public Employee Update(Employee entityChanges)
        {
            var emp = employees.Find(emp => emp.Id == entityChanges.Id);
            if (emp != null)
            {
                emp.Name = entityChanges.Name;
                emp.Sexe = entityChanges.Sexe;
                emp.Email = entityChanges.Email;
                emp.Photo = entityChanges.Photo;
                emp.Departement = entityChanges.Departement;
            }
            return emp;
        }
        public Employee Delete(int id)
        {
            var emp =employees.Find(emp => emp.Id == id);
            if(emp!= null)
            {
                employees.Remove(emp);
            }
            return emp;
        }

        //List  View

    }
}
