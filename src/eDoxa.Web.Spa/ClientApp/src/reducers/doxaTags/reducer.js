import actions from "../../actions/identity";

export const initialState = [];

export const reducer = (state = initialState, action) => {
  switch (action.type) {
    case actions.LOAD_DOXATAGS_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case actions.LOAD_DOXATAGS_FAIL:
    default:
      return state;
  }
};
