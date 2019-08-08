import { LOAD_USERS_SUCCESS, LOAD_USERS_FAIL } from "../actions/identityActions";

export const reducer = (state = [], action) => {
  switch (action.type) {
    case LOAD_USERS_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case LOAD_USERS_FAIL:
      console.log(action.payload);
      return state;
    default:
      return state;
  }
};
