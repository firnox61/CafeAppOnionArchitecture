﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Interfaces.Services.Contracts
{
    public interface IFileService
    {
        Task<string?> UploadImageAsync(IFormFile? file, string folder);
        Task DeleteImageAsync(string folder, string fileName);
    }
}
