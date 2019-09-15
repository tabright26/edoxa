import { reducer, initialState } from "./reducer";
import actionTypes from "actions/arena/challenges";

const challenges204Data = [];
const challenges200Data = [{ id: "1" }, { id: "2" }, { id: "1" }];

const challengeSuccessData = { id: "1" };
const challengeExpectedState = [...initialState, challengeSuccessData];

describe("arena challenges reducer", () => {
  it("should return the initial state", () => {
    expect(reducer(initialState, {})).toEqual(initialState);
  });

  it("should handle LOAD_CHALLENGES_SUCCESS 204", () => {
    expect(
      reducer(initialState, {
        type: actionTypes.LOAD_CHALLENGES_SUCCESS,
        payload: { status: 204, data: challenges204Data }
      })
    ).toEqual(initialState);
  });

  it("should handle LOAD_CHALLENGES_SUCCESS 200", () => {
    expect(
      reducer(initialState, {
        type: actionTypes.LOAD_CHALLENGES_SUCCESS,
        payload: { status: 200, data: challenges200Data }
      })
    ).toEqual(challenges200Data);
  });

  it("should handle LOAD_CHALLENGES_FAIL", () => {
    expect(
      reducer(initialState, {
        type: actionTypes.LOAD_CHALLENGES_FAIL
      })
    ).toEqual(initialState);
  });

  it("should handle LOAD_CHALLENGE_SUCCESS", () => {
    expect(
      reducer(initialState, {
        type: actionTypes.LOAD_CHALLENGE_SUCCESS,
        payload: { data: challengeSuccessData }
      })
    ).toEqual(challengeExpectedState);
  });

  it("should handle LOAD_CHALLENGE_FAIL", () => {
    expect(
      reducer(initialState, {
        type: actionTypes.LOAD_CHALLENGE_FAIL
      })
    ).toEqual(initialState);
  });
});
