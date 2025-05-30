using Cafe.Application.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.DTOs.Users
{
    public class OperationClaimCreateDto : IDto
    {
        public string Name { get; set; }
    }
}
