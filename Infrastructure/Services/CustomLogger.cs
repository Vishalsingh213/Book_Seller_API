
using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    //public class CustomLoggers : ICustomLogger
    //{
    //    private readonly ApplicationDbContext _context;
    //    public CustomLoggers(ApplicationDbContext context)
    //    {
    //        _context = context;
    //    }
    //    public int insertLog(CallLogDto request)
    //    {
    //        var entity = new CallLog
    //        {
    //            id = request.id,
    //            empi = request.empi,
    //            call_direction = request.call_direction,
    //            caller_userid = request.caller_userid,
    //            call_start = request.call_start,
    //            call_end = request.call_end,
    //            agent_connect = request.agent_connect,

    //        };

    //        _context.CallLog.Add(entity);
    //        return  _context.SaveChanges();
    //    }
    //}
}
