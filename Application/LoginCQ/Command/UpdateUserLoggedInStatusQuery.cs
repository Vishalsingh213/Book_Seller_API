using Application.Common;
using Application.Common.Interfaces;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace Application.LoginCQ.Command
{
    public class UpdateUserLoggedInStatusQuery
    {
        public class UpdateUserLoggedInStatusCommand : IRequest<int>
        {
            public string email { get; set; }
        }

        /*public class UpdateUserBusyStatusCommandHandler : IRequestHandler<UpdateUserLoggedInStatusCommand, int>
        {
            private readonly IApplicationDbContext _context;
            private readonly IDapperContext _dapperContext;
            private readonly IConfiguration _config;
            private readonly IMediator _mediator;
            public UpdateUserBusyStatusCommandHandler(IApplicationDbContext context, IConfiguration config, IMediator mediator, IDapperContext dapperContext)
            {
                _context = context;
                _dapperContext = dapperContext;
                _mediator = mediator;
                _config = config;
            }
            public Task<IActionResult> Handle(UpdateUserLoggedInStatusCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var connectionString = _config.GetConnectionString("AppCon");
                    var queryParameters = new DynamicParameters();
                    queryParameters.Add("@email", command.email);
                    using IDbConnection connection = _dapperContext.CreateConnection();
                    var result = await connection.QuerryAsync<string> (Dap)
                }
                return 0;
            }
        }*/

    }
}
