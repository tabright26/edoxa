import { combineEpics } from "redux-observable";
import { ofType } from "redux-observable";
import { mapTo } from "rxjs/operators";

import { throwSubmissionError } from "utils/form/types";

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

//-------------------------------------------------------------------------------------------------

const formUserAccountDepositSuccessEpic = action$ =>
  action$.pipe(
    ofType(
      USER_ACCOUNT_DEPOSIT_MONEY_SUCCESS,
      USER_ACCOUNT_DEPOSIT_TOKEN_SUCCESS
    )
  );

const formUserAccountDepositFailEpic = action$ =>
  action$.pipe(
    ofType(USER_ACCOUNT_DEPOSIT_MONEY_FAIL, USER_ACCOUNT_DEPOSIT_TOKEN_FAIL),
    mapTo(action => throwSubmissionError(action.error))
  );

const formUserAccountDepositEpic = combineEpics(
  formUserAccountDepositSuccessEpic,
  formUserAccountDepositFailEpic
);

const formUserAccountWithdrawalSuccessEpic = action$ =>
  action$.pipe(ofType(USER_ACCOUNT_WITHDRAWAL_MONEY_SUCCESS));

const formUserAccountWithdrawalFailEpic = action$ =>
  action$.pipe(
    ofType(USER_ACCOUNT_WITHDRAWAL_MONEY_FAIL),
    mapTo(action => throwSubmissionError(action.error))
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

const formUserAddressSuccessEpic = action$ =>
  action$.pipe(
    ofType(
      CREATE_USER_ADDRESS_SUCCESS,
      DELETE_USER_ADDRESS_SUCCESS,
      UPDATE_USER_ADDRESS_SUCCESS
    ),
    mapTo(() => loadUserAddressBook())
  );

const formUserAddressFailEpic = action$ =>
  action$.pipe(
    ofType(
      CREATE_USER_ADDRESS_FAIL,
      DELETE_USER_ADDRESS_FAIL,
      UPDATE_USER_ADDRESS_FAIL
    ),
    mapTo(action => throwSubmissionError(action.error))
  );

const formUserAddressEpic = combineEpics(
  formUserAddressSuccessEpic,
  formUserAddressFailEpic
);

//-------------------------------------------------------------------------------------------------

const formUserDoxatagSuccessEpic = action$ =>
  action$.pipe(
    ofType(UPDATE_USER_DOXATAG_SUCCESS),
    mapTo(() => loadUserDoxatagHistory())
  );

const formUserDoxatagFailEpic = action$ =>
  action$.pipe(
    ofType(UPDATE_USER_DOXATAG_FAIL),
    mapTo(action => throwSubmissionError(action.error))
  );

const formUserDoxatagEpic = combineEpics(
  formUserDoxatagSuccessEpic,
  formUserDoxatagFailEpic
);

//-------------------------------------------------------------------------------------------------

const formUserInformationSuccessEpic = action$ =>
  action$.pipe(
    ofType(CREATE_USER_INFORMATIONS_SUCCESS, UPDATE_USER_INFORMATIONS_SUCCESS),
    mapTo(() => loadUserInformations())
  );

const formUserInformationFailEpic = action$ =>
  action$.pipe(
    ofType(CREATE_USER_INFORMATIONS_FAIL, UPDATE_USER_INFORMATIONS_FAIL),
    mapTo(action => throwSubmissionError(action.error))
  );

const formUserInformationEpic = combineEpics(
  formUserInformationSuccessEpic,
  formUserInformationFailEpic
);

//-------------------------------------------------------------------------------------------------

const formUserPasswordForgotSuccessEpic = action$ =>
  action$.pipe(
    ofType(FORGOT_USER_PASSWORD_SUCCESS),
    mapTo(() => push("/"))
  );

const formUserPasswordForgotFailEpic = action$ =>
  action$.pipe(
    ofType(FORGOT_USER_PASSWORD_FAIL),
    mapTo(action => throwSubmissionError(action.error))
  );

const formUserPasswordForgotEpic = combineEpics(
  formUserPasswordForgotSuccessEpic,
  formUserPasswordForgotFailEpic
);

const formUserPasswordResetSuccessEpic = action$ =>
  action$.pipe(
    ofType(RESET_USER_PASSWORD_SUCCESS),
    mapTo(() => (window.location.href = `${REACT_APP_AUTHORITY}/account/login`))
  );

const formUserPasswordResetFailEpic = action$ =>
  action$.pipe(
    ofType(RESET_USER_PASSWORD_FAIL),
    mapTo(action => throwSubmissionError(action.error))
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

const formUserPhoneSuccessEpic = action$ =>
  action$.pipe(
    ofType(UPDATE_USER_PHONE_SUCCESS),
    mapTo(() => loadUserPhone())
  );

const formUserPhoneFailEpic = action$ =>
  action$.pipe(
    ofType(UPDATE_USER_PHONE_FAIL),
    mapTo(action => throwSubmissionError(action.error))
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
