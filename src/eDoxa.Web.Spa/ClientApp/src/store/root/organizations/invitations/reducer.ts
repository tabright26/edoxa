import { throwAxiosSubmissionError } from "store/middlewares/axios/types";
import {
  LOAD_INVITATIONS_FAIL,
  LOAD_INVITATIONS_SUCCESS,
  LOAD_INVITATION_SUCCESS,
  LOAD_INVITATION_FAIL,
  ADD_INVITATION_SUCCESS,
  ADD_INVITATION_FAIL,
  ACCEPT_INVITATION_SUCCESS,
  ACCEPT_INVITATION_FAIL,
  DECLINE_INVITATION_SUCCESS,
  DECLINE_INVITATION_FAIL,
  InvitationsActionTypes
} from "./types";

export const initialState = [];

export const reducer = (state = initialState, action: InvitationsActionTypes) => {
  switch (action.type) {
    case LOAD_INVITATIONS_SUCCESS: {
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    }
    case LOAD_INVITATIONS_FAIL: {
      return state;
    }
    case LOAD_INVITATION_SUCCESS: {
      return [...state, action.payload.data];
    }
    case LOAD_INVITATION_FAIL: {
      return state;
    }
    case ADD_INVITATION_SUCCESS: {
      return state;
    }
    case ADD_INVITATION_FAIL: {
      throwAxiosSubmissionError(action.error);
      return state;
    }
    case ACCEPT_INVITATION_SUCCESS: {
      return state;
    }
    case ACCEPT_INVITATION_FAIL: {
      throwAxiosSubmissionError(action.error);
      return state;
    }
    case DECLINE_INVITATION_SUCCESS: {
      return state;
    }
    case DECLINE_INVITATION_FAIL: {
      throwAxiosSubmissionError(action.error);
      return state;
    }
    default: {
      return state;
    }
  }
};
