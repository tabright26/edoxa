// Filename: PromotionController.cs
// Date Created: 2020-01-20
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.PromotionAggregate;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Requests;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Extensions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Cashier.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/promotions")]
    [ApiExplorerSettings(GroupName = "Promotions")]
    public sealed class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;
        private readonly IMapper _mapper;

        public PromotionController(IPromotionService promotionService, IMapper mapper)
        {
            _promotionService = promotionService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation("Fetch promotions.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PromotionDto[]))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> FetchPromotionsAsync()
        {
            var promotions = await _promotionService.FetchPromotionsAsync();

            if (!promotions.Any())
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<PromotionDto[]>(promotions));
        }

        [HttpPost]
        [SwaggerOperation("Create a promotion.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PromotionDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> CreatePromotionAsync([FromBody] CreatePromotionRequest request)
        {
            var result = await _promotionService.CreatePromotionAsync(
                request.PromotionalCode,
                request.Currency.ToEnumeration<Currency>().From(request.Amount),
                request.Duration.ToTimeSpan(),
                request.ExpiredAt.ToDateTime());

            if (result.IsValid)
            {
                return this.Ok(_mapper.Map<PromotionDto>(result.GetEntityFromMetadata<Promotion>()));
            }

            result.AddToModelState(ModelState);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPost("{promotionalCode}")]
        [SwaggerOperation("Redeem a promotion.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PromotionDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> RedeemPromotionAsync([FromRoute] string promotionalCode)
        {
            var userId = HttpContext.GetUserId();

            var promotion = await _promotionService.FindPromotionOrNullAsync(promotionalCode);

            if (promotion == null)
            {
                return this.NotFound("Promotion not found.");
            }

            var result = await _promotionService.RedeemPromotionAsync(promotion, userId, new UtcNowDateTimeProvider());

            if (result.IsValid)
            {
                return this.Ok(_mapper.Map<PromotionDto>(result.GetEntityFromMetadata<Promotion>()));
            }

            result.AddToModelState(ModelState);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpDelete("{promotionalCode}")]
        [SwaggerOperation("Cancel a promotion.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PromotionDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> CancelPromotionAsync([FromRoute] string promotionalCode)
        {
            var promotion = await _promotionService.FindPromotionOrNullAsync(promotionalCode);

            if (promotion == null)
            {
                return this.NotFound("Promotion not found.");
            }

            var result = await _promotionService.CancelPromotionAsync(promotion, new UtcNowDateTimeProvider());

            if (result.IsValid)
            {
                return this.Ok(_mapper.Map<PromotionDto>(result.GetEntityFromMetadata<Promotion>()));
            }

            result.AddToModelState(ModelState);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }
    }
}
