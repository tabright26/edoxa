import actions from "../../../actions/identity";

export const initialState = null;

export const reducer = (state = initialState, action) => {
  switch (action.type) {
    case actions.LOAD_PERSONAL_INFO_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case actions.LOAD_PERSONAL_INFO_FAIL:
    default: {
      return state;
    }
  }
};
