import { LOAD_ADDRESS_SUCCESS, LOAD_ADDRESS_FAIL } from "../actions/identityActions";

export const reducer = (state = {}, action) => {
  switch (action.type) {
    case LOAD_ADDRESS_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case LOAD_ADDRESS_FAIL:
      console.log(action.payload);
      return state;
    default:
      return state;
  }
};
