import { LOAD_DOXATAGS_SUCCESS, LOAD_DOXATAGS_FAIL } from "../../actions/identity/identity";

export const initialState = [];

export const reducer = (state = initialState, action) => {
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
