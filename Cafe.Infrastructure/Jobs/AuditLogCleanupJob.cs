using Cafe.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Infrastructure.Jobs
{
    public class AuditLogCleanupJob
    {
        private readonly DataContext _context;

        public AuditLogCleanupJob(DataContext context)
        {
            _context = context;
        }


        public async Task CleanupOldLogs()
        {
            var threshold = DateTime.UtcNow.AddDays(-1);
            var oldLogs = _context.AuditLogs.Where(log => log.Timestamp < threshold);
            _context.AuditLogs.RemoveRange(oldLogs);
            await _context.SaveChangesAsync();
        }
    }
}
