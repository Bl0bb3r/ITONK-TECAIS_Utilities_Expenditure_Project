using StatusReporting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatusReporting.Repositories
{
    public interface IStatusRepository
    {
        Task SaveStatusMessageAsync(StatusReportMessage statusReportMessage);
        Task SaveStatusMessageBatchAsync(IEnumerable<StatusReportMessage> statusReportMessages);
    }
}
