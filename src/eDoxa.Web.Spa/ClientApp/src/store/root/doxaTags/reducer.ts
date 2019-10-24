import { LOAD_DOXATAGS, LOAD_DOXATAGS_SUCCESS, LOAD_DOXATAGS_FAIL, DoxatagsState, DoxatagsActions } from "./types";
import { Reducer } from "redux";
import produce, { Draft } from "immer";

export const initialState: DoxatagsState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<DoxatagsState, DoxatagsActions> = produce((draft: Draft<DoxatagsState>, action: DoxatagsActions) => {
  switch (action.type) {
    case LOAD_DOXATAGS:
      draft.error = null;
      draft.loading = true;
      break;
    case LOAD_DOXATAGS_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          draft.error = null;
          draft.loading = false;
          break;
        default:
          draft.data = data;
          draft.error = null;
          draft.loading = false;
          break;
      }
      break;
    case LOAD_DOXATAGS_FAIL:
      draft.error = action.error;
      draft.loading = false;
      break;
  }
}, initialState);
