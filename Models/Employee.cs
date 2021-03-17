using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcExercise.Models
{
    public class Employee
    {

        public int Id { get; set; }
        [Required]
        public string Sexe { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string Photo { get; set; }
        public Departement Departement { get; set; }
    }
}
