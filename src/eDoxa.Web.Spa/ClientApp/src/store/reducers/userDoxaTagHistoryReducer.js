import { LOAD_DOXATAG_HISTORY_SUCCESS, LOAD_DOXATAG_HISTORY_FAIL } from "../actions/identityActions";

export const reducer = (state = [], action) => {
  switch (action.type) {
    case LOAD_DOXATAG_HISTORY_SUCCESS: {
      const { status, data } = action.payload;
      return status !== 204 ? data : state;
    }
    case LOAD_DOXATAG_HISTORY_FAIL:
    default: {
      return state;
    }
  }
};
