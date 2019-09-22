import { LoadDoxaTagHistoryActionType } from "actions/identity/creators";
import { IAxiosAction } from "interfaces/axios";

export const initialState = [];

export const reducer = (state = initialState, action: IAxiosAction<LoadDoxaTagHistoryActionType>) => {
  switch (action.type) {
    case "LOAD_DOXATAG_HISTORY_SUCCESS":
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case "LOAD_DOXATAG_HISTORY_FAIL":
    default: {
      return state;
    }
  }
};
