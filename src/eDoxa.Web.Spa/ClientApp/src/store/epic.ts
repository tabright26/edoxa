import { combineEpics } from "redux-observable";
import { ofType } from "redux-observable";
import { NEVER } from "rxjs";
import { switchMap } from "rxjs/operators";

import {
  USER_ACCOUNT_DEPOSIT_MONEY_SUCCESS,
  USER_ACCOUNT_DEPOSIT_TOKEN_SUCCESS,
  USER_ACCOUNT_DEPOSIT_MONEY_FAIL,
  USER_ACCOUNT_DEPOSIT_TOKEN_FAIL
} from "store/root/user/account/deposit/types";

import {
  USER_ACCOUNT_WITHDRAWAL_MONEY_FAIL,
  USER_ACCOUNT_WITHDRAWAL_MONEY_SUCCESS
} from "store/root/user/account/withdrawal/types";

import {
  CREATE_USER_ADDRESS_SUCCESS,
  DELETE_USER_ADDRESS_SUCCESS,
  UPDATE_USER_ADDRESS_SUCCESS,
  CREATE_USER_ADDRESS_FAIL,
  DELETE_USER_ADDRESS_FAIL,
  UPDATE_USER_ADDRESS_FAIL
} from "store/root/user/addressBook/types";

import { loadUserAddressBook } from "store/root/user/addressBook/actions";

import {
  UPDATE_USER_DOXATAG_SUCCESS,
  UPDATE_USER_DOXATAG_FAIL
} from "store/root/user/doxatagHistory/types";

import { loadUserDoxatagHistory } from "store/root/user/doxatagHistory/actions";

import {
  CREATE_USER_INFORMATIONS_SUCCESS,
  UPDATE_USER_INFORMATIONS_SUCCESS,
  CREATE_USER_INFORMATIONS_FAIL,
  UPDATE_USER_INFORMATIONS_FAIL
} from "store/root/user/information/types";

import { loadUserInformations } from "store/root/user/information/actions";

import {
  FORGOT_USER_PASSWORD_SUCCESS,
  RESET_USER_PASSWORD_SUCCESS,
  FORGOT_USER_PASSWORD_FAIL,
  RESET_USER_PASSWORD_FAIL
} from "store/root/user/password/types";

import { push } from "connected-react-router";
import { REACT_APP_AUTHORITY } from "keys";

import {
  UPDATE_USER_PHONE_SUCCESS,
  UPDATE_USER_PHONE_FAIL
} from "store/root/user/phone/types";

import { loadUserPhone } from "store/root/user/phone/actions";

const formUserAccountDepositSuccessEpic = (action$: any): any =>
  action$.pipe(
    ofType(
      USER_ACCOUNT_DEPOSIT_MONEY_SUCCESS,
      USER_ACCOUNT_DEPOSIT_TOKEN_SUCCESS
    ),
    switchMap((action: any): any => {
      const { resolve } = action.meta.previousAction.meta;
      if (resolve) {
        resolve(action.payload);
      }
    })
  );

const formUserAccountDepositFailEpic = (action$: any) =>
  action$.pipe(
    ofType(USER_ACCOUNT_DEPOSIT_MONEY_FAIL, USER_ACCOUNT_DEPOSIT_TOKEN_FAIL),
    switchMap((action: any) => {
      const { reject } = action.meta.previousAction.meta;
      if (reject) {
        reject(action.error);
      }
      return NEVER;
    })
  );

const formUserAccountDepositEpic = combineEpics(
  formUserAccountDepositSuccessEpic,
  formUserAccountDepositFailEpic
);

const formUserAccountWithdrawalSuccessEpic = (action$: any): any =>
  action$.pipe(
    ofType(USER_ACCOUNT_WITHDRAWAL_MONEY_SUCCESS),
    switchMap((action: any): any => {
      const { resolve } = action.meta.previousAction.meta;
      if (resolve) {
        resolve(action.payload);
      }
    })
  );

const formUserAccountWithdrawalFailEpic = (action$: any) =>
  action$.pipe(
    ofType(USER_ACCOUNT_WITHDRAWAL_MONEY_FAIL),
    switchMap((action: any) => {
      const { reject } = action.meta.previousAction.meta;
      if (reject) {
        reject(action.error);
      }
      return NEVER;
    })
  );

const formUserAccountWithdrawalEpic = combineEpics(
  formUserAccountWithdrawalSuccessEpic,
  formUserAccountWithdrawalFailEpic
);

const formUserAccountEpic = combineEpics(
  formUserAccountDepositEpic,
  formUserAccountWithdrawalEpic
);

//-------------------------------------------------------------------------------------------------

const formUserAddressSuccessEpic = (action$: any): any =>
  action$.pipe(
    ofType(
      CREATE_USER_ADDRESS_SUCCESS,
      DELETE_USER_ADDRESS_SUCCESS,
      UPDATE_USER_ADDRESS_SUCCESS
    ),
    switchMap((action: any): any => {
      const { resolve } = action.meta.previousAction.meta;
      if (resolve) {
        resolve(action.payload);
      }
      return Promise.resolve(loadUserAddressBook());
    })
  );

const formUserAddressFailEpic = (action$: any) =>
  action$.pipe(
    ofType(
      CREATE_USER_ADDRESS_FAIL,
      DELETE_USER_ADDRESS_FAIL,
      UPDATE_USER_ADDRESS_FAIL
    ),
    switchMap((action: any) => {
      const { reject } = action.meta.previousAction.meta;
      if (reject) {
        reject(action.error);
      }
      return NEVER;
    })
  );

const formUserAddressEpic = combineEpics(
  formUserAddressSuccessEpic,
  formUserAddressFailEpic
);

//-------------------------------------------------------------------------------------------------

const formUserDoxatagSuccessEpic = (action$: any): any =>
  action$.pipe(
    ofType(UPDATE_USER_DOXATAG_SUCCESS),
    switchMap((action: any): any => {
      const { resolve } = action.meta.previousAction.meta;
      if (resolve) {
        resolve(action.payload);
      }
      return Promise.resolve(loadUserDoxatagHistory());
    })
  );

const formUserDoxatagFailEpic = (action$: any) =>
  action$.pipe(
    ofType(UPDATE_USER_DOXATAG_FAIL),
    switchMap((action: any) => {
      const { reject } = action.meta.previousAction.meta;
      if (reject) {
        reject(action.error);
      }
      return NEVER;
    })
  );

const formUserDoxatagEpic = combineEpics(
  formUserDoxatagSuccessEpic,
  formUserDoxatagFailEpic
);

//-------------------------------------------------------------------------------------------------

const formUserInformationSuccessEpic = (action$: any): any =>
  action$.pipe(
    ofType(CREATE_USER_INFORMATIONS_SUCCESS, UPDATE_USER_INFORMATIONS_SUCCESS),
    switchMap((action: any): any => {
      const { resolve } = action.meta.previousAction.meta;
      if (resolve) {
        resolve(action.payload);
      }
      return Promise.resolve(loadUserInformations());
    })
  );

const formUserInformationFailEpic = (action$: any) =>
  action$.pipe(
    ofType(CREATE_USER_INFORMATIONS_FAIL, UPDATE_USER_INFORMATIONS_FAIL),
    switchMap((action: any) => {
      const { reject } = action.meta.previousAction.meta;
      if (reject) {
        reject(action.error);
      }
      return NEVER;
    })
  );

const formUserInformationEpic = combineEpics(
  formUserInformationSuccessEpic,
  formUserInformationFailEpic
);

//-------------------------------------------------------------------------------------------------

const formUserPasswordForgotSuccessEpic = (action$: any): any =>
  action$.pipe(
    ofType(FORGOT_USER_PASSWORD_SUCCESS),
    switchMap((action: any): any => {
      const { resolve } = action.meta.previousAction.meta;
      if (resolve) {
        resolve(action.payload);
      }
      return Promise.resolve(push("/"));
    })
  );

const formUserPasswordForgotFailEpic = (action$: any) =>
  action$.pipe(
    ofType(FORGOT_USER_PASSWORD_FAIL),
    switchMap((action: any) => {
      const { reject } = action.meta.previousAction.meta;
      if (reject) {
        reject(action.error);
      }
      return NEVER;
    })
  );

const formUserPasswordForgotEpic = combineEpics(
  formUserPasswordForgotSuccessEpic,
  formUserPasswordForgotFailEpic
);

const formUserPasswordResetSuccessEpic = (action$: any): any =>
  action$.pipe(
    ofType(RESET_USER_PASSWORD_SUCCESS),
    switchMap((action: any): any => {
      const { resolve } = action.meta.previousAction.meta;
      if (resolve) {
        resolve(action.payload);
      }
      return Promise.resolve(
        (window.location.href = `${REACT_APP_AUTHORITY}/account/login`)
      );
    })
  );

const formUserPasswordResetFailEpic = (action$: any) =>
  action$.pipe(
    ofType(RESET_USER_PASSWORD_FAIL),
    switchMap((action: any) => {
      const { reject } = action.meta.previousAction.meta;
      if (reject) {
        reject(action.error);
      }
      return NEVER;
    })
  );

const formUserPasswordResetEpic = combineEpics(
  formUserPasswordResetSuccessEpic,
  formUserPasswordResetFailEpic
);

const formUserPasswordEpic = combineEpics(
  formUserPasswordForgotEpic,
  formUserPasswordResetEpic
);

//-------------------------------------------------------------------------------------------------

const formUserPhoneSuccessEpic = (action$: any): any =>
  action$.pipe(
    ofType(UPDATE_USER_PHONE_SUCCESS),
    switchMap((action: any): any => {
      const { resolve } = action.meta.previousAction.meta;
      if (resolve) {
        resolve(action.payload);
      }
      return Promise.resolve(loadUserPhone());
    })
  );

const formUserPhoneFailEpic = (action$: any) =>
  action$.pipe(
    ofType(UPDATE_USER_PHONE_FAIL),
    switchMap((action: any) => {
      const { reject } = action.meta.previousAction.meta;
      if (reject) {
        reject(action.error);
      }
      return NEVER;
    })
  );

const formUserPhoneEpic = combineEpics(
  formUserPhoneSuccessEpic,
  formUserPhoneFailEpic
);

//-------------------------------------------------------------------------------------------------

const epic = combineEpics(
  formUserAccountEpic,
  formUserAddressEpic,
  formUserDoxatagEpic,
  formUserInformationEpic,
  formUserPasswordEpic,
  formUserPhoneEpic
);

export const epics = epic;
