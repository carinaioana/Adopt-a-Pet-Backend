using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptPets.Application.Persistence
{
    public interface IS3Service
    {
        Task<(bool Success, string Url)> UploadFileAsync(IFormFile file);
    }
}
