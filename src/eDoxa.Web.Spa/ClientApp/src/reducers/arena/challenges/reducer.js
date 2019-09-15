import actions from "../../../actions/arena/challenges";

export const initialState = [];

export const reducer = (state = initialState, action) => {
  switch (action.type) {
    case actions.LOAD_CHALLENGES_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case actions.LOAD_CHALLENGE_SUCCESS:
      return [...state, action.payload.data];
    case actions.LOAD_CHALLENGE_FAIL:
    case actions.LOAD_CHALLENGES_FAIL:
    default:
      return state;
  }
};
