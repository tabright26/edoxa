import { loadGameCredential } from "./actions";
import { LOAD_GAME_CREDENTIAL, LOAD_GAME_CREDENTIAL_SUCCESS, LOAD_GAME_CREDENTIAL_FAIL } from "./types";

describe("identity actions", () => {
  it("should create an action to get user games id", () => {
    const expectedType = [LOAD_GAME_CREDENTIAL, LOAD_GAME_CREDENTIAL_SUCCESS, LOAD_GAME_CREDENTIAL_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = "/identity/api/games";

    const object = loadGameCredential();

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });
});
