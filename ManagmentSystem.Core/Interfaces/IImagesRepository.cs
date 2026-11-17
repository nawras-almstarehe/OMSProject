using ManagmentSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ManagmentSystem.Core.Interfaces
{
    public interface IImagesRepository : IBaseRepository<ImageFolder>
    {
        Task<IEnumerable<ImageFolder>> GetImagesByCategoryId(string categoryId);
    }
}
