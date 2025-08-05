using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EasyTask4.DAL
{
    public interface IStatusRepository
    {
        Task<List<Status>> GetAllStatusesAsync();
    }
}