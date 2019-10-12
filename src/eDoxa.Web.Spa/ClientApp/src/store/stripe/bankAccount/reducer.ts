import { LOAD_BANK_ACCOUNT_SUCCESS, LOAD_BANK_ACCOUNT_FAIL, UPDATE_BANK_ACCOUNT_SUCCESS, UPDATE_BANK_ACCOUNT_FAIL, BankAccountActionTypes } from "./types";
import { AxiosErrorData } from "store/types";
import { Reducer } from "redux";
import { SubmissionError } from "redux-form";

export const initialState = { data: {} };

export const reducer: Reducer<any, BankAccountActionTypes> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_BANK_ACCOUNT_SUCCESS:
      return { data: action.payload.data };
    case UPDATE_BANK_ACCOUNT_FAIL:
      const { isAxiosError, response } = action.error;
      if (isAxiosError) {
        throw new SubmissionError<AxiosErrorData>(response.data.errors);
      }
      break;
    case UPDATE_BANK_ACCOUNT_SUCCESS:
    case LOAD_BANK_ACCOUNT_FAIL:
    default:
      return state;
  }
};
