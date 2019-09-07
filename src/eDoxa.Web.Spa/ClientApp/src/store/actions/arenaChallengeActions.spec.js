import { loadChallenges, loadChallenge, LOAD_CHALLENGES, LOAD_CHALLENGES_SUCCESS, LOAD_CHALLENGES_FAIL, LOAD_CHALLENGE, LOAD_CHALLENGE_SUCCESS, LOAD_CHALLENGE_FAIL } from "./arenaChallengeActions";

describe("arena challenge actions", () => {
  it("should create an action to get all challenges", () => {
    const expectedType = [LOAD_CHALLENGES, LOAD_CHALLENGES_SUCCESS, LOAD_CHALLENGES_FAIL];
    const expectedMethod = "get";
    const expectedUrl = "/arena/challenge/api/challenges";

    const object = loadChallenges();

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });

  it("should create an action to get a challenge", () => {
    const challengeId = 1;

    const expectedType = [LOAD_CHALLENGE, LOAD_CHALLENGE_SUCCESS, LOAD_CHALLENGE_FAIL];
    const expectedMethod = "get";
    const expectedUrl = `/arena/challenge/api/challenges/${challengeId}`;

    const object = loadChallenge(challengeId);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });
});
