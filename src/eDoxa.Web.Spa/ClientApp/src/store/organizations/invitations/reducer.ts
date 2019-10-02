import { LOAD_INVITATIONS_FAIL, LOAD_INVITATIONS_SUCCESS, InvitationsActionTypes } from "./types";

export const initialState = [];

export const reducer = (state = initialState, action: InvitationsActionTypes) => {
  switch (action.type) {
    case LOAD_INVITATIONS_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case LOAD_INVITATIONS_FAIL:
    default: {
      return state;
    }
  }
};
