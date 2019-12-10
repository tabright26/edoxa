// Filename: DoxatagGrpcService.cs
// Date Created: 2019-12-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Grpc.Protos.Identity.Responses;
using eDoxa.Grpc.Protos.Shared.Dtos;
using eDoxa.Identity.Api.Application.Services;
using eDoxa.Identity.Domain.AggregateModels.DoxatagAggregate;

using Grpc.Core;

using DoxatagService = eDoxa.Grpc.Protos.Identity.Services.DoxatagService;

namespace eDoxa.Identity.Api.Services
{
    public sealed class DoxatagGrpcService : DoxatagService.DoxatagServiceBase
    {
        private readonly IDoxatagService _doxatagService;

        public DoxatagGrpcService(IDoxatagService doxatagService)
        {
            _doxatagService = doxatagService;
        }

        public override async Task<FetchDoxatagsResponse> FetchDoxatags(FetchDoxatagsRequest request, ServerCallContext context)
        {
            var doxatags = await _doxatagService.FetchDoxatagsAsync();

            return new FetchDoxatagsResponse
            {
                Status = new StatusDto
                {
                    Code = (int) context.Status.StatusCode,
                    Message = context.Status.Detail
                },
                Doxatags =
                {
                    doxatags.Select(MapDoxatag)
                }
            };
        }

        private static DoxatagDto MapDoxatag(Doxatag doxatag)
        {
            return new DoxatagDto
            {
                Code = doxatag.Code,
                Name = doxatag.Name
            };
        }
    }
}
