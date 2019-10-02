import { LOAD_CLANS, LOAD_CLANS_SUCCESS, LOAD_CLANS_FAIL } from "./types";
import { loadClans } from "./actions";

describe("clans actions", () => {
  it("should create an action to get all clans", () => {
    const expectedType = [LOAD_CLANS, LOAD_CLANS_SUCCESS, LOAD_CLANS_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = "/organizations/api/clans";

    const actionCreator = loadClans();

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
