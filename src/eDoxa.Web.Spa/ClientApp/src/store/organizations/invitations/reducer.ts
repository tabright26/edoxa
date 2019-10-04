import { SubmissionError } from "redux-form";
import { AxiosErrorData } from "interfaces/axios";

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
    case LOAD_INVITATIONS_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }

    case LOAD_INVITATION_SUCCESS:
      return [...state, action.payload.data];

    case ADD_INVITATION_FAIL:
    case ACCEPT_INVITATION_FAIL:
    case DECLINE_INVITATION_FAIL: {
      const { isAxiosError, response } = action.error;
      if (isAxiosError) {
        throw new SubmissionError<AxiosErrorData>(response.data.errors);
      }
      break;
    }

    case ADD_INVITATION_SUCCESS:
    case ACCEPT_INVITATION_SUCCESS:
    case DECLINE_INVITATION_SUCCESS:
    case LOAD_INVITATIONS_FAIL:
    case LOAD_INVITATION_FAIL:
    default: {
      return state;
    }
  }
};
