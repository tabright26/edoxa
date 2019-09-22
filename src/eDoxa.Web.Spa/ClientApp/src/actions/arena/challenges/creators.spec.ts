import { loadChallenges, loadChallenge } from "./creators";
import actionTypes from "./index";

describe("arena challenge actions", () => {
  it("should create an action to get all challenges", () => {
    const expectedType = [actionTypes.LOAD_CHALLENGES, actionTypes.LOAD_CHALLENGES_SUCCESS, actionTypes.LOAD_CHALLENGES_FAIL];
    const expectedMethod = "get";
    const expectedUrl = "/arena/challenge/api/challenges";

    const actionCreator = loadChallenges();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get a challenge", () => {
    const challengeId = "1";

    const expectedType = [actionTypes.LOAD_CHALLENGE, actionTypes.LOAD_CHALLENGE_SUCCESS, actionTypes.LOAD_CHALLENGE_FAIL];
    const expectedMethod = "get";
    const expectedUrl = `/arena/challenge/api/challenges/${challengeId}`;

    const actionCreator = loadChallenge(challengeId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
