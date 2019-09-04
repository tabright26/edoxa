import { reducer } from "../reducers/arenaChallengesReducer";
import * as types from "../actions/arenaChallengeActions";

describe("arena challenges reducer", () => {
  it("should return the initial state", () => {
    expect(reducer(undefined, {})).toEqual([]);
  });

  it("should handle LOAD_CHALLENGES_SUCCESS 204", () => {
    expect(
      reducer([], {
        type: types.LOAD_CHALLENGES_SUCCESS,
        payload: { status: 204, data: "Run the tests" }
      })
    ).toEqual([]);
  });

  it("should handle LOAD_CHALLENGES_SUCCESS Empty", () => {
    expect(
      reducer([], {
        type: types.LOAD_CHALLENGES_SUCCESS,
        payload: { status: {}, data: "Run the tests" }
      })
    ).toEqual("Run the tests");
  });

  it("should handle LOAD_CHALLENGES_FAIL", () => {
    expect(
      reducer([], {
        type: types.LOAD_CHALLENGES_FAIL,
        payload: { status: {}, data: "This is an error message" }
      })
    ).toEqual([]);
  });

  it("should handle LOAD_CHALLENGE_SUCCESS", () => {
    expect(
      reducer([], {
        type: types.LOAD_CHALLENGE_SUCCESS,
        payload: { status: {}, data: "Run the tests" }
      })
    ).toEqual(["Run the tests"]);
  });

  it("should handle LOAD_CHALLENGE_FAIL", () => {
    expect(
      reducer([], {
        type: types.LOAD_CHALLENGE_FAIL,
        payload: { status: {}, data: "This is an error message" }
      })
    ).toEqual([]);
  });
});
