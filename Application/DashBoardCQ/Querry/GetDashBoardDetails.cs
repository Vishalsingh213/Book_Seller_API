using Application.Common.Interfaces;
using Application.DashBoardCQ.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DashBoardCQ.Querry
{
    public class GetDashBoardDetails : IRequest<List<QuickSightDashboardDetailDto>>
    {

    }

    public class GetDashBoardDetailsHandler : IRequestHandler<GetDashBoardDetails, List<QuickSightDashboardDetailDto>>
    {
        private IApplicationDbContext _context;

        public GetDashBoardDetailsHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<QuickSightDashboardDetailDto>> Handle(GetDashBoardDetails request, CancellationToken cancellationToken)
        {
            List<QuickSightDashboardDetailDto> detail = new List<QuickSightDashboardDetailDto>();
            var dashDetail = await _context.Header.ToListAsync();
            foreach(var item in dashDetail)
            {
                detail.Add(new QuickSightDashboardDetailDto { DashBoardName = item.Name, Description = item.Description });
            }

            return detail;
        }
    }
}
