import { throwAxiosSubmissionError } from "store/middlewares/axios/types";
import { LOAD_MEMBERS_FAIL, LOAD_MEMBERS_SUCCESS, KICK_MEMBER_FAIL, KICK_MEMBER_SUCCESS, LEAVE_CLAN_FAIL, LEAVE_CLAN_SUCCESS, MembersActionTypes } from "./types";

export const initialState = [];

export const reducer = (state = initialState, action: MembersActionTypes) => {
  switch (action.type) {
    case LOAD_MEMBERS_SUCCESS: {
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    }
    case LOAD_MEMBERS_FAIL: {
      return state;
    }
    case LEAVE_CLAN_SUCCESS: {
      return state;
    }
    case LEAVE_CLAN_FAIL: {
      throwAxiosSubmissionError(action.error);
      return state;
    }
    case KICK_MEMBER_SUCCESS: {
      return state;
    }
    case KICK_MEMBER_FAIL: {
      throwAxiosSubmissionError(action.error);
      return state;
    }
    default: {
      return state;
    }
  }
};
