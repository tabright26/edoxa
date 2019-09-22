import { LoadPersonalInfoActionType } from "actions/identity/actionTypes";
import { IAxiosAction } from "interfaces/axios";

export const initialState = null;

export const reducer = (state = initialState, action: IAxiosAction<LoadPersonalInfoActionType>) => {
  switch (action.type) {
    case "LOAD_PERSONAL_INFO_SUCCESS":
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case "LOAD_PERSONAL_INFO_FAIL":
    default: {
      return state;
    }
  }
};
