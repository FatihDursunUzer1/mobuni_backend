﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.DTOs.Requests
{
    public class CreateDepartmentDTO: BaseCreateDTO<int>
    {
        public string Name { get; set; }
    }
}
