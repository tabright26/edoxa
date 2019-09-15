import actionTypes from "actions/arena/challenges";

export const initialState = [];

export const reducer = (state = initialState, action) => {
  switch (action.type) {
    case actionTypes.LOAD_CHALLENGES_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case actionTypes.LOAD_CHALLENGE_SUCCESS:
      return [...state, action.payload.data];
    case actionTypes.LOAD_CHALLENGE_FAIL:
    case actionTypes.LOAD_CHALLENGES_FAIL:
    default:
      return state;
  }
};
