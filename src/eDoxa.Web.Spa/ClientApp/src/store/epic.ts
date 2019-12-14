import { combineEpics } from "redux-observable";
import { ofType } from "redux-observable";
import { NEVER } from "rxjs";
import { switchMap } from "rxjs/operators";

import {
  USER_ACCOUNT_DEPOSIT_MONEY_SUCCESS,
  USER_ACCOUNT_DEPOSIT_TOKEN_SUCCESS,
  USER_ACCOUNT_DEPOSIT_MONEY_FAIL,
  USER_ACCOUNT_DEPOSIT_TOKEN_FAIL
} from "store/actions/account/types";

import {
  USER_ACCOUNT_WITHDRAWAL_MONEY_FAIL,
  USER_ACCOUNT_WITHDRAWAL_MONEY_SUCCESS
} from "store/actions/account/types";

import {
  CREATE_USER_ADDRESS_SUCCESS,
  DELETE_USER_ADDRESS_SUCCESS,
  UPDATE_USER_ADDRESS_SUCCESS,
  CREATE_USER_ADDRESS_FAIL,
  DELETE_USER_ADDRESS_FAIL,
  UPDATE_USER_ADDRESS_FAIL
} from "store/actions/identity/types";

import {
  UPDATE_USER_DOXATAG_SUCCESS,
  UPDATE_USER_DOXATAG_FAIL
} from "store/actions/identity/types";

import {
  CREATE_USER_INFORMATIONS_SUCCESS,
  UPDATE_USER_INFORMATIONS_SUCCESS,
  CREATE_USER_INFORMATIONS_FAIL,
  UPDATE_USER_INFORMATIONS_FAIL
} from "store/actions/identity/types";

import {
  FORGOT_USER_PASSWORD_SUCCESS,
  RESET_USER_PASSWORD_SUCCESS,
  FORGOT_USER_PASSWORD_FAIL,
  RESET_USER_PASSWORD_FAIL
} from "store/actions/identity/types";

//import { push } from "connected-react-router";
//import { REACT_APP_AUTHORITY } from "keys";

import {
  UPDATE_USER_PHONE_SUCCESS,
  UPDATE_USER_PHONE_FAIL
} from "store/actions/identity/types";

const formSuccessEpic = (action$: any): any =>
  action$.pipe(
    ofType(
      USER_ACCOUNT_DEPOSIT_MONEY_SUCCESS,
      USER_ACCOUNT_DEPOSIT_TOKEN_SUCCESS,
      USER_ACCOUNT_WITHDRAWAL_MONEY_SUCCESS,
      CREATE_USER_ADDRESS_SUCCESS,
      DELETE_USER_ADDRESS_SUCCESS,
      UPDATE_USER_ADDRESS_SUCCESS,
      UPDATE_USER_DOXATAG_SUCCESS,
      CREATE_USER_INFORMATIONS_SUCCESS,
      UPDATE_USER_INFORMATIONS_SUCCESS,
      FORGOT_USER_PASSWORD_SUCCESS,
      RESET_USER_PASSWORD_SUCCESS,
      UPDATE_USER_PHONE_SUCCESS
    ),
    switchMap((action: any): any => {
      const { resolve } = action.meta.previousAction.meta;
      if (resolve) {
        resolve(action.payload);
      }
    })
  );

const formFailEpic = (action$: any) =>
  action$.pipe(
    ofType(
      USER_ACCOUNT_DEPOSIT_MONEY_FAIL,
      USER_ACCOUNT_DEPOSIT_TOKEN_FAIL,
      USER_ACCOUNT_WITHDRAWAL_MONEY_FAIL,
      CREATE_USER_ADDRESS_FAIL,
      DELETE_USER_ADDRESS_FAIL,
      UPDATE_USER_ADDRESS_FAIL,
      UPDATE_USER_DOXATAG_FAIL,
      CREATE_USER_INFORMATIONS_FAIL,
      UPDATE_USER_INFORMATIONS_FAIL,
      FORGOT_USER_PASSWORD_FAIL,
      RESET_USER_PASSWORD_FAIL,
      UPDATE_USER_PHONE_FAIL
    ),
    switchMap((action: any) => {
      const { reject } = action.meta.previousAction.meta;
      if (reject) {
        reject(action.error);
      }
      return NEVER;
    })
  );

//-------------------------------------------------------------------------------------------------

//const passwordForgotAction = push("/");
//const passwordResetAction = (window.location.href = `${REACT_APP_AUTHORITY}/account/login`);

//-------------------------------------------------------------------------------------------------

const epic = combineEpics(formSuccessEpic, formFailEpic);

export const epics = epic;
