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
} from "./types";
import { Reducer } from "redux";

export const initialState: UserInformationsState = {
  data: null,
  error: null,
  loading: false
};

export const reducer: Reducer<UserInformationsState, UserInformationsActions> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_USER_INFORMATIONS: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_USER_INFORMATIONS_SUCCESS: {
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
    case LOAD_USER_INFORMATIONS_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    case CREATE_USER_INFORMATIONS: {
      return { data: state.data, error: null, loading: true };
    }
    case CREATE_USER_INFORMATIONS_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case CREATE_USER_INFORMATIONS_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    case UPDATE_USER_INFORMATIONS: {
      return { data: state.data, error: null, loading: true };
    }
    case UPDATE_USER_INFORMATIONS_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case UPDATE_USER_INFORMATIONS_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
