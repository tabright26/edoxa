import { SubmissionError } from "redux-form";
import { AxiosErrorData } from "store/middlewares/axios/types";

import { LOAD_MEMBERS_FAIL, LOAD_MEMBERS_SUCCESS, KICK_MEMBER_FAIL, KICK_MEMBER_SUCCESS, LEAVE_CLAN_FAIL, LEAVE_CLAN_SUCCESS, MembersActionTypes } from "./types";

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

    case KICK_MEMBER_FAIL: {
      const { isAxiosError, response } = action.error;
      if (isAxiosError) {
        throw new SubmissionError<AxiosErrorData>(response.data.errors);
      }
      break;
    }

    case LEAVE_CLAN_SUCCESS:
    case LEAVE_CLAN_FAIL:
    case KICK_MEMBER_SUCCESS:
    case LOAD_MEMBERS_FAIL:
    default: {
      return state;
    }
  }
};
