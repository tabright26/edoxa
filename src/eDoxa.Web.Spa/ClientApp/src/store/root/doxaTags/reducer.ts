import { LOAD_DOXATAGS, LOAD_DOXATAGS_SUCCESS, LOAD_DOXATAGS_FAIL, DoxatagsState, DoxatagsActions } from "./types";
import { Reducer } from "redux";

export const initialState: DoxatagsState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<DoxatagsState, DoxatagsActions> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_DOXATAGS: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_DOXATAGS_SUCCESS: {
      const { status, data } = action.payload;
      switch (status) {
        case 204: {
          return { data: state.data, error: null, loading: false };
        }
        default: {
          return { data: data, error: null, loading: false };
        }
      }
    }
    case LOAD_DOXATAGS_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
