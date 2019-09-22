import { LoadGamesActionType } from "actions/identity/actionTypes";
import { IAxiosAction } from "interfaces/axios";

export const initialState = [];

export const reducer = (state = initialState, action: IAxiosAction<LoadGamesActionType>) => {
  switch (action.type) {
    case "LOAD_GAMES_SUCCESS":
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case "LOAD_GAMES_FAIL":
    default: {
      return state;
    }
  }
};
