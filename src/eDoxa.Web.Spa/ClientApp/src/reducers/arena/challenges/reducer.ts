import { LOAD_CHALLENGES_SUCCESS, LOAD_CHALLENGES_FAIL, LOAD_CHALLENGE_SUCCESS, LOAD_CHALLENGE_FAIL, ChallengesActionTypes } from "reducers/arena/challenges/types";

export const initialState = [];

export const reducer = (state = initialState, action: ChallengesActionTypes) => {
  switch (action.type) {
    case LOAD_CHALLENGES_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case LOAD_CHALLENGE_SUCCESS:
      return [...state, action.payload.data];
    case LOAD_CHALLENGE_FAIL:
    case LOAD_CHALLENGES_FAIL:
    default:
      return state;
  }
};
