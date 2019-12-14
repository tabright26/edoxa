import {
  LOAD_USER_INFORMATIONS,
  LOAD_USER_INFORMATIONS_SUCCESS,
  LOAD_USER_INFORMATIONS_FAIL,
  CREATE_USER_INFORMATIONS,
  CREATE_USER_INFORMATIONS_SUCCESS,
  CREATE_USER_INFORMATIONS_FAIL,
  UPDATE_USER_INFORMATIONS,
  UPDATE_USER_INFORMATIONS_SUCCESS,
  UPDATE_USER_INFORMATIONS_FAIL,
  UserInformationsActions,
  UserInformationsState
} from "store/actions/identity/types";
import { Reducer } from "redux";
import produce, { Draft } from "immer";

export const initialState: UserInformationsState = {
  data: null,
  error: null,
  loading: false
};

export const reducer: Reducer<
  UserInformationsState,
  UserInformationsActions
> = produce(
  (draft: Draft<UserInformationsState>, action: UserInformationsActions) => {
    switch (action.type) {
      case LOAD_USER_INFORMATIONS:
        draft.error = null;
        draft.loading = true;
        break;
      case LOAD_USER_INFORMATIONS_SUCCESS:
        draft.data = action.payload.data;
        draft.error = null;
        draft.loading = false;
        break;
      case LOAD_USER_INFORMATIONS_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
      case CREATE_USER_INFORMATIONS:
        draft.error = null;
        draft.loading = true;
        break;
      case CREATE_USER_INFORMATIONS_SUCCESS:
        draft.error = null;
        draft.loading = false;
        break;
      case CREATE_USER_INFORMATIONS_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
      case UPDATE_USER_INFORMATIONS:
        draft.error = null;
        draft.loading = true;
        break;
      case UPDATE_USER_INFORMATIONS_SUCCESS:
        draft.error = null;
        draft.loading = false;
        break;
      case UPDATE_USER_INFORMATIONS_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
    }
  },
  initialState
);
