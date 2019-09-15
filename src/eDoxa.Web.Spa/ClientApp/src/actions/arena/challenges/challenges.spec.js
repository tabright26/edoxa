import { loadChallenges, loadChallenge, LOAD_CHALLENGES, LOAD_CHALLENGES_SUCCESS, LOAD_CHALLENGES_FAIL, LOAD_CHALLENGE, LOAD_CHALLENGE_SUCCESS, LOAD_CHALLENGE_FAIL } from "./challenges";

describe("arena challenge actions", () => {
  it("should create an action to get all challenges", () => {
    const expectedType = [LOAD_CHALLENGES, LOAD_CHALLENGES_SUCCESS, LOAD_CHALLENGES_FAIL];
    const expectedMethod = "get";
    const expectedUrl = "/arena/challenge/api/challenges";

    const actionCreator = loadChallenges();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get a challenge", () => {
    const challengeId = 1;

    const expectedType = [LOAD_CHALLENGE, LOAD_CHALLENGE_SUCCESS, LOAD_CHALLENGE_FAIL];
    const expectedMethod = "get";
    const expectedUrl = `/arena/challenge/api/challenges/${challengeId}`;

    const actionCreator = loadChallenge(challengeId);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
