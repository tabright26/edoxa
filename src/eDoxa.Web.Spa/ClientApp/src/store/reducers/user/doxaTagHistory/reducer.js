import { LOAD_DOXATAG_HISTORY_SUCCESS, LOAD_DOXATAG_HISTORY_FAIL } from "../../../actions/identityActions";

export const initialState = [];

export const reducer = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_DOXATAG_HISTORY_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case LOAD_DOXATAG_HISTORY_FAIL:
    default: {
      return state;
    }
  }
};
