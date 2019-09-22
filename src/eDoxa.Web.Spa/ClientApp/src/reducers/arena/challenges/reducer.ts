import { IAxiosAction } from "interfaces/axios";
import { LoadChallengesActionType, LoadChallengeActionType } from "actions/arena/challenges/actionTypes";

export const initialState = [];

export const reducer = (state = initialState, action: IAxiosAction<LoadChallengesActionType | LoadChallengeActionType>) => {
  switch (action.type) {
    case "LOAD_CHALLENGES_SUCCESS":
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case "LOAD_CHALLENGE_SUCCESS":
      return [...state, action.payload.data];
    case "LOAD_CHALLENGE_FAIL":
    case "LOAD_CHALLENGES_FAIL":
    default:
      return state;
  }
};
