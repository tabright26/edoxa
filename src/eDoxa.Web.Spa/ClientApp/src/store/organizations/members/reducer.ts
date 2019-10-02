import { LOAD_MEMBERS_FAIL, LOAD_MEMBERS_SUCCESS, MembersActionTypes } from "./types";

export const initialState = [];

export const reducer = (state = initialState, action: MembersActionTypes) => {
  switch (action.type) {
    case LOAD_MEMBERS_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case LOAD_MEMBERS_FAIL:
    default: {
      return state;
    }
  }
};
