import { Reducer } from "redux";
import { UserAccountState } from "./types";
import {
  UserAccountActions,
  LOGOUT_USER_ACCOUNT_SUCCESS
} from "store/actions/identity/types";
import produce, { Draft } from "immer";

const initialState: UserAccountState = {
  logout: {
    token: null
  }
};

export const reducer: Reducer<UserAccountState, UserAccountActions> = produce(
  (draft: Draft<UserAccountState>, action: UserAccountActions) => {
    switch (action.type) {
      case LOGOUT_USER_ACCOUNT_SUCCESS: {
        draft.logout.token = action.payload.data;
        break;
      }
    }
  },
  initialState
);
