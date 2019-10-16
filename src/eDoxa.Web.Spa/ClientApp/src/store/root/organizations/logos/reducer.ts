import { Reducer } from "redux";
import { DOWNLOAD_CLAN_LOGO_SUCCESS, DOWNLOAD_CLAN_LOGO_FAIL, UPLOAD_CLAN_LOGO_FAIL, UPLOAD_CLAN_LOGO_SUCCESS, ClanLogosState, ClanLogosActions } from "./types";

export const initialState: ClanLogosState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<ClanLogosState, ClanLogosActions> = (state = initialState, action) => {
  switch (action.type) {
    case DOWNLOAD_CLAN_LOGO_SUCCESS: {
      return { data: [...state.data, action.payload.data], error: null, loading: false };
    }
    case DOWNLOAD_CLAN_LOGO_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    case UPLOAD_CLAN_LOGO_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case UPLOAD_CLAN_LOGO_FAIL: {
      //throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
