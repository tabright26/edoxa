//// Filename: StripeBankAccountController.cs
//// Date Created: 2019-11-25
//// 
//// ================================================
//// Copyright © 2019, eDoxa. All rights reserved.

//using System.Threading.Tasks;

//using AutoMapper;

//using eDoxa.Grpc.Protos.Payment.Dtos;
//using eDoxa.Grpc.Protos.Payment.Requests;
//using eDoxa.Payment.Domain.Stripe.Services;
//using eDoxa.Seedwork.Application.Extensions;

//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//using Swashbuckle.AspNetCore.Annotations;

//namespace eDoxa.Payment.Api.Controllers.Stripe
//{
//    [Authorize]
//    [ApiVersion("1.0")]
//    [Route("api/stripe/bank-account")]
//    [ApiExplorerSettings(GroupName = "Stripe")]
//    public sealed class BankAccountController : ControllerBase
//    {
//        private readonly IStripeExternalAccountService _externalAccountService;
//        private readonly IStripeAccountService _stripeAccountService;
//        private readonly IStripeService _stripeService;
//        private readonly IMapper _mapper;

//        public BankAccountController(
//            IStripeExternalAccountService externalAccountService,
//            IStripeAccountService stripeAccountService,
//            IStripeService stripeService,
//            IMapper mapper
//        )
//        {
//            _externalAccountService = externalAccountService;
//            _stripeAccountService = stripeAccountService;
//            _stripeService = stripeService;
//            _mapper = mapper;
//        }

//        [HttpGet]
//        [SwaggerOperation("Find bank account.")]
//        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StripeBankAccountDto))]
//        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
//        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
//        public async Task<IActionResult> FetchBankAccountAsync()
//        {
//            var userId = HttpContext.GetUserId();

//            if (!await _stripeService.UserExistsAsync(userId))
//            {
//                return this.NotFound("Stripe reference not found.");
//            }

//            var accountId = await _stripeAccountService.GetAccountIdAsync(userId);

//            var bankAccount = await _externalAccountService.FindBankAccountAsync(accountId);

//            if (bankAccount == null)
//            {
//                return this.NotFound("Bank account not found.");
//            }

//            return this.Ok(_mapper.Map<StripeBankAccountDto>(bankAccount));
//        }

//        [HttpPost]
//        [SwaggerOperation("Update bank account.")]
//        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StripeBankAccountDto))]
//        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
//        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
//        public async Task<IActionResult> UpdateBankAccountAsync([FromBody] CreateStripeBankAccountRequest request)
//        {
//            var userId = HttpContext.GetUserId();

//            if (!await _stripeService.UserExistsAsync(userId))
//            {
//                return this.NotFound("Stripe reference not found.");
//            }

//            var accountId = await _stripeAccountService.GetAccountIdAsync(userId);

//            var bankAccount = await _externalAccountService.ChangeBankAccountAsync(accountId, request.Token);

//            return this.Ok(_mapper.Map<StripeBankAccountDto>(bankAccount));
//        }
//    }
//}
