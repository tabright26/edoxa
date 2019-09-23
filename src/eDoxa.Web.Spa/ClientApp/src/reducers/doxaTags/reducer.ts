import { LOAD_DOXATAGS_SUCCESS, LOAD_DOXATAGS_FAIL, DoxatagsActionTypes } from "./types";

export const initialState = [];

export const reducer = (state = initialState, action: DoxatagsActionTypes) => {
  switch (action.type) {
    case LOAD_DOXATAGS_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case LOAD_DOXATAGS_FAIL:
    default:
      return state;
  }
};
