import {
  LOAD_CHALLENGES,
  LOAD_CHALLENGES_SUCCESS,
  LOAD_CHALLENGES_FAIL,
  LOAD_CHALLENGE,
  LOAD_CHALLENGE_SUCCESS,
  LOAD_CHALLENGE_FAIL,
  ChallengesState,
  ChallengesActions
} from "store/root/arena/challenges/types";
import { Reducer } from "redux";
import produce, { Draft } from "immer";

export const initialState: ChallengesState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<ChallengesState, ChallengesActions> = produce((draft: Draft<ChallengesState>, action: ChallengesActions) => {
  switch (action.type) {
    case LOAD_CHALLENGES:
      draft.error = null;
      draft.loading = true;
      break;
    case LOAD_CHALLENGES_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          draft.error = null;
          draft.loading = false;
          break;
        default:
          draft.data = data;
          draft.error = null;
          draft.loading = false;
          break;
      }
      break;
    case LOAD_CHALLENGES_FAIL:
      draft.error = action.error;
      draft.loading = false;
      break;
    case LOAD_CHALLENGE:
      draft.error = null;
      draft.loading = true;
      break;
    case LOAD_CHALLENGE_SUCCESS:
      draft.data = [...draft.data, action.payload.data];
      draft.error = null;
      draft.loading = false;
      break;
    case LOAD_CHALLENGE_FAIL:
      draft.error = action.error;
      draft.loading = false;
      break;
  }
}, initialState);
