import {
  LOAD_BANK_ACCOUNTS_SUCCESS,
  LOAD_BANK_ACCOUNTS_FAIL,
  CREATE_BANK_ACCOUNT_SUCCESS,
  CREATE_BANK_ACCOUNT_FAIL,
  UPDATE_BANK_ACCOUNT_SUCCESS,
  UPDATE_BANK_ACCOUNT_FAIL,
  DELETE_BANK_ACCOUNT_SUCCESS,
  DELETE_BANK_ACCOUNT_FAIL,
  BankAccountsActionTypes
} from "./types";
import { AxiosErrorData } from "interfaces/axios";
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
    case CREATE_BANK_ACCOUNT_FAIL:
    case UPDATE_BANK_ACCOUNT_FAIL:
    case DELETE_BANK_ACCOUNT_FAIL:
      const { isAxiosError, response } = action.error;
      if (isAxiosError) {
        throw new SubmissionError<AxiosErrorData>(response.data.errors);
      }
      break;
    case CREATE_BANK_ACCOUNT_SUCCESS:
    case UPDATE_BANK_ACCOUNT_SUCCESS:
    case DELETE_BANK_ACCOUNT_SUCCESS:
    case LOAD_BANK_ACCOUNTS_FAIL:
    default:
      return state;
  }
};
