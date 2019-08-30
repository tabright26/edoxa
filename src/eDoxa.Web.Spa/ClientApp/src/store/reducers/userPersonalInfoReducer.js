import { LOAD_PERSONAL_INFO_SUCCESS, LOAD_PERSONAL_INFO_FAIL } from "../actions/identityActions";

export const reducer = (state = null, action) => {
  switch (action.type) {
    case LOAD_PERSONAL_INFO_SUCCESS: {
      const { status, data } = action.payload;
      return status !== 204 ? data : state;
    }
    case LOAD_PERSONAL_INFO_FAIL:
    default: {
      return state;
    }
  }
};
