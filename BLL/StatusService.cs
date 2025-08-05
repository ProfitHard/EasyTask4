using EasyTask4.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EasyTask4.BLL
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<IEnumerable<Status>> GetAllStatusesAsync()
        {
            return await _statusRepository.GetAllStatusesAsync();
        }
    }
}