import { LOAD_CLAN_SUCCESS, LOAD_CLAN_FAIL, ClanActionTypes } from "./types";

export const initialState = [];

export const reducer = (state = initialState, action: ClanActionTypes) => {
  switch (action.type) {
    case LOAD_CLAN_SUCCESS:
      return [...state, action.payload.data];
    case LOAD_CLAN_FAIL:
    default: {
      return state;
    }
  }
};
