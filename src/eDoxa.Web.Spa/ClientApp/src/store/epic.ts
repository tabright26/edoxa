import { combineEpics, ofType, Epic } from "redux-observable";
import { NEVER } from "rxjs";
import { switchMap, map, mapTo, delay } from "rxjs/operators";
import {
  DEPOSIT_TRANSACTION_SUCCESS,
  REDEEM_PROMOTION_FAIL,
  REDEEM_PROMOTION_SUCCESS,
  DepositTransactionAction,
  RedeemPromotionAction,
  WITHDRAW_TRANSACTION_SUCCESS,
  DEPOSIT_TRANSACTION_FAIL,
  WITHDRAW_TRANSACTION_FAIL,
  WithdrawTransactionAction
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
  REGISTER_USER_ACCOUNT_FAIL,
  RESEND_USER_EMAIL_SUCCESS,
  RESEND_USER_EMAIL_FAIL
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
import { RootActions, RootState } from "./types";
import { loadUserTransactionHistory } from "./actions/cashier";
import {
  REGISTER_CHALLENGE_PARTICIPANT_SUCCESS,
  RegisterChallengeParticipantAction,
  REGISTER_CHALLENGE_PARTICIPANT_FAIL
} from "./actions/challenge/types";
import {
  ATTACH_STRIPE_PAYMENTMETHOD_FAIL,
  ATTACH_STRIPE_PAYMENTMETHOD_SUCCESS
} from "./actions/payment/types";

const createTransactionEpic: Epic<
  DepositTransactionAction | WithdrawTransactionAction,
  any,
  RootState
> = action$ =>
  action$.pipe(
    ofType(
      DEPOSIT_TRANSACTION_SUCCESS,
      DEPOSIT_TRANSACTION_FAIL,
      WITHDRAW_TRANSACTION_SUCCESS,
      WITHDRAW_TRANSACTION_FAIL
    ),
    delay(2500),
    mapTo(loadUserTransactionHistory()),
    delay(5000),
    mapTo(loadUserTransactionHistory())
  );

const registerChallengeParticipantEpic: Epic<
  RegisterChallengeParticipantAction,
  any,
  RootState
> = (action$, state$) =>
  action$.pipe(
    ofType(REGISTER_CHALLENGE_PARTICIPANT_SUCCESS),
    delay(2500), // SHOULD BE REMOVED WE SIGNALR WILL BE INSTALL.
    map(action =>
      loadUserTransactionHistory(
        state$.value.root.challenge.data.find(
          challenge => challenge.id === action.payload.data.challengeId
        ).payout.entryFee.type
      )
    )
  );

const redeemPromotionEpic: Epic<
  RedeemPromotionAction,
  any,
  RootState
> = action$ =>
  action$.pipe(
    ofType(REDEEM_PROMOTION_SUCCESS),
    map(action => loadUserTransactionHistory(action.payload.data.currency.type))
  );

const onSubmitSuccessEpic: Epic<RootActions, any, RootState> = action$ =>
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
      VALIDATE_GAME_AUTHENTICATION_SUCCESS,
      GENERATE_GAME_AUTHENTICATION_SUCCESS,
      UNLINK_GAME_CREDENTIAL_SUCCESS,
      REDEEM_PROMOTION_SUCCESS,
      LOGIN_USER_ACCOUNT_SUCCESS,
      REGISTER_USER_ACCOUNT_SUCCESS,
      REGISTER_CHALLENGE_PARTICIPANT_SUCCESS,
      WITHDRAW_TRANSACTION_SUCCESS,
      ATTACH_STRIPE_PAYMENTMETHOD_SUCCESS,
      RESEND_USER_EMAIL_SUCCESS
    ),
    switchMap(action => {
      const { resolve } = action.meta.previousAction.meta;
      if (resolve) {
        resolve(action.payload);
      }
      return NEVER;
    })
  );

const onSubmitFailEpic: Epic<RootActions, any, RootState> = action$ =>
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
      VALIDATE_GAME_AUTHENTICATION_FAIL,
      GENERATE_GAME_AUTHENTICATION_FAIL,
      UNLINK_GAME_CREDENTIAL_FAIL,
      REDEEM_PROMOTION_FAIL,
      LOGIN_USER_ACCOUNT_FAIL,
      REGISTER_USER_ACCOUNT_FAIL,
      REGISTER_CHALLENGE_PARTICIPANT_FAIL,
      WITHDRAW_TRANSACTION_FAIL,
      ATTACH_STRIPE_PAYMENTMETHOD_FAIL,
      RESEND_USER_EMAIL_FAIL
    ),
    switchMap(action => {
      const { reject } = action.meta.previousAction.meta;
      if (reject) {
        reject(action.error);
      }
      return NEVER;
    })
  );

export const epic = combineEpics<RootActions, any, any, any>(
  createTransactionEpic,
  registerChallengeParticipantEpic,
  redeemPromotionEpic,
  onSubmitSuccessEpic,
  onSubmitFailEpic
);
