import { Reducer } from "redux";
import {
  DOWNLOAD_CLAN_LOGO,
  DOWNLOAD_CLAN_LOGO_SUCCESS,
  DOWNLOAD_CLAN_LOGO_FAIL,
  UPLOAD_CLAN_LOGO,
  UPLOAD_CLAN_LOGO_FAIL,
  UPLOAD_CLAN_LOGO_SUCCESS
} from "store/actions/clan/types";
import produce, { Draft } from "immer";
import { ClanLogosState } from "./types";
import { RootActions } from "store/types";

export const initialState: ClanLogosState = {
  data: [],
  loading: false
};

export const reducer: Reducer<ClanLogosState, RootActions> = produce(
  (draft: Draft<ClanLogosState>, action: RootActions) => {
    switch (action.type) {
      case DOWNLOAD_CLAN_LOGO:
        draft.loading = true;
        break;
      case DOWNLOAD_CLAN_LOGO_SUCCESS:
        draft.data = [...draft.data, action.payload.data];
        draft.loading = false;
        break;
      case DOWNLOAD_CLAN_LOGO_FAIL:
        draft.loading = false;
        break;
      case UPLOAD_CLAN_LOGO:
        draft.loading = true;
        break;
      case UPLOAD_CLAN_LOGO_SUCCESS:
        draft.loading = false;
        break;
      case UPLOAD_CLAN_LOGO_FAIL:
        draft.loading = false;
        break;
    }
  },
  initialState
);
