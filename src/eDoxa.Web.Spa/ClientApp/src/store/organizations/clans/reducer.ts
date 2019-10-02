import { LOAD_CLANS_SUCCESS, LOAD_CLANS_FAIL, ClansActionTypes } from "./types";

export const initialState = [];

export const reducer = (state = initialState, action: ClansActionTypes) => {
  switch (action.type) {
    case LOAD_CLANS_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case LOAD_CLANS_FAIL:
    default: {
      return state;
    }
  }
};
