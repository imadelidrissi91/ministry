﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcExercise.ViewModels
{
    public class AdministrationEditRoleViewModel
    {

        public string Id { get; set; }
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        public List<String> Users { get; set; }
    }
}
