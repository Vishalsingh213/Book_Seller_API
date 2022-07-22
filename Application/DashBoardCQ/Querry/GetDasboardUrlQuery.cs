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

namespace Application.DashBoardCQ.Querry
{
    public class GetDasboardUrlQuery : IRequest<string>
    {
    }
    public class GetDashBoardUrlHandler : IRequestHandler<GetDasboardUrlQuery, string>
    {
        public async Task<string> Handle(GetDasboardUrlQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var client = getUrl();
                var urlResponse = await client.GetDashboardEmbedUrlAsync(new GetDashboardEmbedUrlRequest
                {
                    AwsAccountId= "953070155272",
                    DashboardId = "9ea06167-6fc9-439a-abb9-e41cbddf5b38",
                    IdentityType = EmbeddingIdentityType.IAM,
                    ResetDisabled = true,
                    SessionLifetimeInMinutes = 600,
                    UndoRedoDisabled = true,
                });
                return urlResponse.EmbedUrl;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

           

        }
        public AmazonQuickSightClient getUrl()
        {
             var url= new AmazonQuickSightClient(
                 "AKIA53Z3IOYENNGJSOM3",
                 "UJWj9KAnXuMQI7AiNQmlCPZ2YAU3gDtgTwuUaHga",
                 Amazon.RegionEndpoint.USEast1
                 );
            return url;
        }
    }
}
