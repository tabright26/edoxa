import { LOAD_CLAN, LOAD_CLAN_SUCCESS, LOAD_CLAN_FAIL } from "./types";
import { loadClan } from "./actions";

describe("clan actions", () => {
  it("should create an action to get user clan", () => {
    const expectedType = [LOAD_CLAN, LOAD_CLAN_SUCCESS, LOAD_CLAN_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = "/organizations/api/clans";

    const actionCreator = loadClan();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
