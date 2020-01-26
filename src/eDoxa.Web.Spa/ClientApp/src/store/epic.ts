import { combineEpics, ofType } from "redux-observable";
import { NEVER } from "rxjs";
import { switchMap } from "rxjs/operators";
import {
  CREATE_USER_TRANSACTION_SUCCESS,
  CREATE_USER_TRANSACTION_FAIL,
  REDEEM_PROMOTION_FAIL,
  REDEEM_PROMOTION_SUCCESS
} from "store/actions/cashier/types";
import {
  CREATE_USER_ADDRESS_SUCCESS,
  DELETE_USER_ADDRESS_SUCCESS,
  UPDATE_USER_ADDRESS_SUCCESS,
  CREATE_USER_ADDRESS_FAIL,
  DELETE_USER_ADDRESS_FAIL,
  UPDATE_USER_ADDRESS_FAIL,
  LOGIN_USER_ACCOUNT_SUCCESS,
  LOGIN_USER_ACCOUNT_FAIL,
  REGISTER_USER_ACCOUNT_SUCCESS,
  REGISTER_USER_ACCOUNT_FAIL
} from "store/actions/identity/types";
import {
  CHANGE_USER_DOXATAG_SUCCESS,
  CHANGE_USER_DOXATAG_FAIL
} from "store/actions/identity/types";
import {
  CREATE_USER_PROFILE_SUCCESS,
  UPDATE_USER_PROFILE_SUCCESS,
  CREATE_USER_PROFILE_FAIL,
  UPDATE_USER_PROFILE_FAIL
} from "store/actions/identity/types";
import {
  FORGOT_USER_PASSWORD_SUCCESS,
  RESET_USER_PASSWORD_SUCCESS,
  FORGOT_USER_PASSWORD_FAIL,
  RESET_USER_PASSWORD_FAIL
} from "store/actions/identity/types";
import {
  UPDATE_USER_PHONE_SUCCESS,
  UPDATE_USER_PHONE_FAIL
} from "store/actions/identity/types";
import {
  VALIDATE_GAME_AUTHENTICATION_FAIL,
  VALIDATE_GAME_AUTHENTICATION_SUCCESS,
  GENERATE_GAME_AUTHENTICATION_SUCCESS,
  GENERATE_GAME_AUTHENTICATION_FAIL,
  UNLINK_GAME_CREDENTIAL_SUCCESS,
  UNLINK_GAME_CREDENTIAL_FAIL
} from "./actions/game/types";

const formSuccessEpic = (action$: any): any =>
  action$.pipe(
    ofType(
      CREATE_USER_ADDRESS_SUCCESS,
      DELETE_USER_ADDRESS_SUCCESS,
      UPDATE_USER_ADDRESS_SUCCESS,
      CHANGE_USER_DOXATAG_SUCCESS,
      CREATE_USER_PROFILE_SUCCESS,
      UPDATE_USER_PROFILE_SUCCESS,
      FORGOT_USER_PASSWORD_SUCCESS,
      RESET_USER_PASSWORD_SUCCESS,
      UPDATE_USER_PHONE_SUCCESS,
      CREATE_USER_TRANSACTION_SUCCESS,
      VALIDATE_GAME_AUTHENTICATION_SUCCESS,
      GENERATE_GAME_AUTHENTICATION_SUCCESS,
      UNLINK_GAME_CREDENTIAL_SUCCESS,
      REDEEM_PROMOTION_SUCCESS,
      LOGIN_USER_ACCOUNT_SUCCESS,
      REGISTER_USER_ACCOUNT_SUCCESS
    ),
    switchMap((action: any): any => {
      const { resolve } = action.meta.previousAction.meta;
      if (resolve) {
        resolve(action.payload);
      }
      return NEVER;
    })
  );

const formFailEpic = (action$: any) =>
  action$.pipe(
    ofType(
      CREATE_USER_ADDRESS_FAIL,
      DELETE_USER_ADDRESS_FAIL,
      UPDATE_USER_ADDRESS_FAIL,
      CHANGE_USER_DOXATAG_FAIL,
      CREATE_USER_PROFILE_FAIL,
      UPDATE_USER_PROFILE_FAIL,
      FORGOT_USER_PASSWORD_FAIL,
      RESET_USER_PASSWORD_FAIL,
      UPDATE_USER_PHONE_FAIL,
      CREATE_USER_TRANSACTION_FAIL,
      VALIDATE_GAME_AUTHENTICATION_FAIL,
      GENERATE_GAME_AUTHENTICATION_FAIL,
      UNLINK_GAME_CREDENTIAL_FAIL,
      REDEEM_PROMOTION_FAIL,
      LOGIN_USER_ACCOUNT_FAIL,
      REGISTER_USER_ACCOUNT_FAIL
    ),
    switchMap((action: any) => {
      const { reject } = action.meta.previousAction.meta;
      if (reject) {
        reject(action.error);
      }
      return NEVER;
    })
  );

export const epic = combineEpics(formSuccessEpic, formFailEpic);
