import {
  LOAD_BANK_ACCOUNTS_SUCCESS,
  LOAD_BANK_ACCOUNTS_FAIL,
  CHANGE_BANK_ACCOUNT_SUCCESS,
  CHANGE_BANK_ACCOUNT_FAIL,
  BankAccountsActionTypes
} from "./types";
import { AxiosErrorData } from "store/types";
import { SubmissionError } from "redux-form";

export const initialState = { data: [] };

export const reducer = (state = initialState, action: BankAccountsActionTypes) => {
  switch (action.type) {
    case LOAD_BANK_ACCOUNTS_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case CHANGE_BANK_ACCOUNT_FAIL:
      const { isAxiosError, response } = action.error;
      if (isAxiosError) {
        throw new SubmissionError<AxiosErrorData>(response.data.errors);
      }
      break;
    case CHANGE_BANK_ACCOUNT_SUCCESS:
    case LOAD_BANK_ACCOUNTS_FAIL:
    default:
      return state;
  }
};
