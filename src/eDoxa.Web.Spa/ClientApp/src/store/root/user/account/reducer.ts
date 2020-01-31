import { Reducer } from "redux";
import { UserAccountState } from "./types";
import { LOGOUT_USER_ACCOUNT_SUCCESS } from "store/actions/identity/types";
import produce, { Draft } from "immer";
import { RootActions } from "store/types";

const initialState: UserAccountState = {
  logout: {
    token: null
  }
};

export const reducer: Reducer<UserAccountState, RootActions> = produce(
  (draft: Draft<UserAccountState>, action: RootActions) => {
    switch (action.type) {
      case LOGOUT_USER_ACCOUNT_SUCCESS: {
        draft.logout.token = action.payload.data;
        break;
      }
    }
  },
  initialState
);
