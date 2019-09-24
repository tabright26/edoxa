import { reducer, initialState } from "./reducer";

const challenges204Data = [];
const challenges200Data = [{ id: "1" }, { id: "2" }, { id: "1" }];

const challengeSuccessData = { id: "1" };
const challengeExpectedState = [...initialState, challengeSuccessData];

describe("arena challenges reducer", () => {
  it("should return the initial state", () => {
    const action: any = {};
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_CHALLENGES_SUCCESS 204", () => {
    const action: any = {
      type: "LOAD_CHALLENGES_SUCCESS",
      payload: { status: 204, data: challenges204Data }
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_CHALLENGES_SUCCESS 200", () => {
    const action: any = {
      type: "LOAD_CHALLENGES_SUCCESS",
      payload: { status: 200, data: challenges200Data }
    };
    expect(reducer(initialState, action)).toEqual(challenges200Data);
  });

  it("should handle LOAD_CHALLENGES_FAIL", () => {
    const action: any = {
      type: "LOAD_CHALLENGES_FAIL"
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });

  it("should handle LOAD_CHALLENGE_SUCCESS", () => {
    const action: any = {
      type: "LOAD_CHALLENGE_SUCCESS",
      payload: { data: challengeSuccessData }
    };
    expect(reducer(initialState, action)).toEqual(challengeExpectedState);
  });

  it("should handle LOAD_CHALLENGE_FAIL", () => {
    const action: any = {
      type: "LOAD_CHALLENGE_FAIL"
    };
    expect(reducer(initialState, action)).toEqual(initialState);
  });
});
