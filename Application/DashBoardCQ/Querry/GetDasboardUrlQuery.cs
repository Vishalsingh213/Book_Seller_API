using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.QuickSight;
using Amazon.QuickSight.Model;
using Application.Common.Interfaces;
using AutoMapper.Configuration;
using Application.DashBoardCQ.ViewModel;

namespace Application.DashBoardCQ.Querry
{
    public class GetDasboardUrlQuery : IRequest<QuickSightURLDto>
    {
        public string dashName { get; set; }
    }
    public class GetDashBoardUrlHandler : IRequestHandler<GetDasboardUrlQuery, QuickSightURLDto>
    {
        private readonly IApplicationDbContext _context;
        public GetDashBoardUrlHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<QuickSightURLDto> Handle(GetDasboardUrlQuery request, CancellationToken cancellationToken)
        {
            var quickSightURLDto = new QuickSightURLDto()
            {
                URL = await GetUrl(request)
            };
            
            return quickSightURLDto;
        }


        private async Task<string> GetUrl(GetDasboardUrlQuery request)
        {
            var quickSightDashboard = _context.QuicksightDashboardKey.Where(X => X.Key == request.dashName).FirstOrDefault();
            var client = new AmazonQuickSightClient(
                             quickSightDashboard.AccessKey,
                             quickSightDashboard.SecretAcessKey,
                             Amazon.RegionEndpoint.USEast1
                     );

            //Generate Embed URL
            var urlReponse = await client.GetDashboardEmbedUrlAsync(new GetDashboardEmbedUrlRequest
            {
                AwsAccountId = quickSightDashboard.AwsAccountId,
                DashboardId = quickSightDashboard.DashboardId,
                IdentityType = EmbeddingIdentityType.IAM,
                ResetDisabled = true,
                SessionLifetimeInMinutes = 600,
                UndoRedoDisabled = true,
            });

            return urlReponse.EmbedUrl;
        }

    }
}

