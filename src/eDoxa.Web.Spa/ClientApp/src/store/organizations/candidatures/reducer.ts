import { LOAD_CANDIDATURES_FAIL, LOAD_CANDIDATURES_SUCCESS, CandidaturesActionTypes } from "./types";

export const initialState = [];

export const reducer = (state = initialState, action: CandidaturesActionTypes) => {
  switch (action.type) {
    case LOAD_CANDIDATURES_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case LOAD_CANDIDATURES_FAIL:
    default: {
      return state;
    }
  }
};
