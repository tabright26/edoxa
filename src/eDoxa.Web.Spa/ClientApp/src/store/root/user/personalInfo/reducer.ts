import { SubmissionError } from "redux-form";
import { AxiosErrorData } from "store/middlewares/axios/types";
import { PersonalInfoActionTypes } from "./types";

export const initialState = null;

export const reducer = (state = initialState, action: PersonalInfoActionTypes) => {
  switch (action.type) {
    case "LOAD_PERSONAL_INFO_SUCCESS":
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case "CREATE_PERSONAL_INFO_FAIL":
    case "UPDATE_PERSONAL_INFO_FAIL":
      const { isAxiosError, response } = action.error;
      if (isAxiosError) {
        throw new SubmissionError<AxiosErrorData>(response.data.errors);
      }
      break;
    case "CREATE_PERSONAL_INFO_SUCCESS":
    case "UPDATE_PERSONAL_INFO_SUCCESS":
    case "LOAD_PERSONAL_INFO_FAIL":
    default: {
      return state;
    }
  }
};
