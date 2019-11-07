// Filename: ChallengesController.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Web.Aggregator.Services;
using eDoxa.Web.Aggregator.Transformers;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Web.Aggregator.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/challenges")]
    [ApiExplorerSettings(GroupName = "Challenge")]
    public class ChallengesController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IChallengesService _challengesService;
        private readonly ICashierService _cashierService;

        public ChallengesController(IIdentityService identityService, IChallengesService challengesService, ICashierService cashierService)
        {
            _identityService = identityService;
            _challengesService = challengesService;
            _cashierService = cashierService;
        }

        [HttpGet]
        public async Task<IActionResult> FetchChallengesAsync()
        {
            var doxatagsFromIdentityService = await _identityService.FetchDoxatagsAsync();

            var challengesFromCashierService = await _cashierService.FetchChallengesAsync();

            var challengesFromChallengesService = await _challengesService.FetchChallengesAsync();

            return this.Ok(ChallengeTransformer.Transform(challengesFromChallengesService, challengesFromCashierService, doxatagsFromIdentityService));
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateChallengeAsync([FromBody] CreateChallengeRequest request)
        //{
        //    //var doxatagsFromIdentityService = await _identityService.FetchDoxatagsAsync();

        //    //var challengeFromCashierService = await _cashierService.CreateChallengeAsync(request);

        //    try
        //    {
        //        var challengeFromChallengesService = await _challengesService.CreateChallengeAsync(
        //            new ChallengePostRequest(
        //                request.Name,
        //                request.Game,
        //                request.BestOf,
        //                request.Entries,
        //                request.Duration));

        //        return this.Ok(challengeFromChallengesService);

        //        //return this.Ok( /*ChallengeTransformer.Transform(challengeFromChallengesService, challengeFromCashierService, doxatagsFromIdentityService)*/);
        //    }
        //    catch (ValidationApiException exception)
        //    {
        //        //await _cashierService.DeleteChallengeAsync(challengeFromCashierService.Id);

        //        return this.BadRequest(exception.Content);
        //    }
        //    catch (ApiException exception)
        //    {
        //        //await _cashierService.DeleteChallengeAsync(challengeFromCashierService.Id);

        //        return this.BadRequest(exception.Content);
        //    }
        //}

        [HttpGet("{challengeId}")]
        public async Task<IActionResult> FindChallengeAsync(Guid challengeId)
        {
            var doxatagsFromIdentityService = await _identityService.FetchDoxatagsAsync();

            var challengeFromCashierService = await _cashierService.FindChallengeAsync(challengeId);

            var challengeFromChallengesService = await _challengesService.FindChallengeAsync(challengeId);

            return this.Ok(ChallengeTransformer.Transform(challengeFromChallengesService, challengeFromCashierService, doxatagsFromIdentityService));
        }

        //[HttpPost("{challengeId}")]
        //public async Task<IActionResult> SynchronizeChallengeAsync(Guid challengeId)
        //{
        //    var doxatagsFromIdentityService = await _identityService.FetchDoxatagsAsync();

        //    var challengeFromCashierService = await _cashierService.FindChallengeAsync(challengeId);

        //    var challengeFromChallengesService = await _challengesService.SynchronizeChallengeAsync(challengeId);

        //    return this.Ok(ChallengeTransformer.Transform(challengeFromChallengesService, challengeFromCashierService, doxatagsFromIdentityService));
        //}
    }
}
