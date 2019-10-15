import { throwAxiosSubmissionError } from "store/middlewares/axios/types";
import { Reducer } from "redux";
import { LOAD_MEMBERS, LOAD_MEMBERS_FAIL, LOAD_MEMBERS_SUCCESS, KICK_MEMBER_FAIL, KICK_MEMBER_SUCCESS, LEAVE_CLAN_FAIL, LEAVE_CLAN_SUCCESS, MembersState, MembersActionTypes } from "./types";

export const initialState: MembersState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<MembersState, MembersActionTypes> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_MEMBERS: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_MEMBERS_SUCCESS: {
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return { data: state.data, error: null, loading: false };
        default:
          return { data: data, error: null, loading: false };
      }
    }
    case LOAD_MEMBERS_FAIL: {
      return { data: state.data, error: LOAD_MEMBERS_FAIL, loading: false };
    }
    case LEAVE_CLAN_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case LEAVE_CLAN_FAIL: {
      throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    case KICK_MEMBER_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case KICK_MEMBER_FAIL: {
      throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
