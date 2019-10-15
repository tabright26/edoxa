import { throwAxiosSubmissionError } from "store/middlewares/axios/types";
import {
  LOAD_INVITATIONS,
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
  InvitationsActionTypes,
  InvitationsState
} from "./types";
import { Reducer } from "redux";

export const initialState: InvitationsState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<InvitationsState, InvitationsActionTypes> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_INVITATIONS: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_INVITATIONS_SUCCESS: {
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return { data: state.data, error: null, loading: false };
        default:
          return { data: data, error: null, loading: false };
      }
    }
    case LOAD_INVITATIONS_FAIL: {
      return { data: state.data, error: LOAD_INVITATIONS_FAIL, loading: false };
    }
    case LOAD_INVITATION_SUCCESS: {
      return { data: [...state.data, action.payload.data], error: null, loading: false };
    }
    case LOAD_INVITATION_FAIL: {
      return { data: state.data, error: LOAD_INVITATION_FAIL, loading: false };
    }
    case ADD_INVITATION_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case ADD_INVITATION_FAIL: {
      throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    case ACCEPT_INVITATION_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case ACCEPT_INVITATION_FAIL: {
      throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    case DECLINE_INVITATION_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case DECLINE_INVITATION_FAIL: {
      throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
