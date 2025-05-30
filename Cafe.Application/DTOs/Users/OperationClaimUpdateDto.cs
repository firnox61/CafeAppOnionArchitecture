﻿using Cafe.Application.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.DTOs.Users
{
    public class OperationClaimUpdateDto : IDto
    {
        public int Id { get; set; } // 🟢 güncellenecek ID burada gelmeli
        public string Name { get; set; }
    }
}
