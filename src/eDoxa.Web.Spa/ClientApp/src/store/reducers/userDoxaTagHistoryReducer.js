import { LOAD_DOXATAG_HISTORY_SUCCESS, LOAD_DOXATAG_HISTORY_FAIL } from "../actions/identityActions";

export const reducer = (state = [], action) => {
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
      console.log(action.payload);
      return state;
    default:
      return state;
  }
};
