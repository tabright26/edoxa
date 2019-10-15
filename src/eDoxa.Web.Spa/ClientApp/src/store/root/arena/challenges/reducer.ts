import { LOAD_CHALLENGES, LOAD_CHALLENGES_SUCCESS, LOAD_CHALLENGES_FAIL, LOAD_CHALLENGE_SUCCESS, LOAD_CHALLENGE_FAIL, ChallengesState, ChallengesActionTypes } from "store/root/arena/challenges/types";
import { Reducer } from "redux";

export const initialState: ChallengesState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<ChallengesState, ChallengesActionTypes> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_CHALLENGES: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_CHALLENGES_SUCCESS: {
      const { status, data } = action.payload;
      switch (status) {
        case 204: {
          return { data: state.data, error: null, loading: false };
        }
        default: {
          // FRANCIS: DATA SHOULD BE SOMETHING LIKE [...state.data, ...action.payload.data].
          return { data: data, error: null, loading: false };
        }
      }
    }
    case LOAD_CHALLENGES_FAIL: {
      return { data: state.data, error: LOAD_CHALLENGES_FAIL, loading: false };
    }
    case LOAD_CHALLENGE_SUCCESS: {
      return { data: [...state.data, action.payload.data], error: null, loading: false };
    }
    case LOAD_CHALLENGE_FAIL: {
      return { data: state.data, error: LOAD_CHALLENGE_FAIL, loading: false };
    }
    default: {
      return state;
    }
  }
};
