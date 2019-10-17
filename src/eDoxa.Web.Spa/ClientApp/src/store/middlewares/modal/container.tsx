import React, { FunctionComponent } from "react";
import { connect } from "react-redux";
import { show } from "redux-modal";
import {
  CREATE_CLAN_MODAL,
  CREATE_USER_ADDRESS_MODAL,
  CHALLENGE_MATCH_SCORE_MODAL,
  CREATE_STRIPE_PAYMENTMETHOD_MODAL,
  UPDATE_STRIPE_PAYMENTMETHOD_MODAL,
  DELETE_STRIPE_PAYMENTMETHOD_MODAL,
  DEPOSIT_MODAL,
  WITHDRAWAL_MODAL
} from "modals";

export const withModals = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => <HighOrderComponent {...props} />;

  const mapDispatchToProps = (dispatch: any) => {
    return {
      modals: {
        showCreateUserAddressModal: () => dispatch(show(CREATE_USER_ADDRESS_MODAL)),
        showChallengeMatchScoreModal: stats => dispatch(show(CHALLENGE_MATCH_SCORE_MODAL, { stats })),
        showCreateClanModal: () => dispatch(show(CREATE_CLAN_MODAL)),
        showDepositModal: (actions, amounts) => dispatch(show(DEPOSIT_MODAL, { actions, amounts })),
        showWithdrawalModal: (actions, amounts) => dispatch(show(WITHDRAWAL_MODAL, { actions, amounts })),
        showCreateStripePaymentMethodModal: type => dispatch(show(CREATE_STRIPE_PAYMENTMETHOD_MODAL, { type })),
        showUpdateStripePaymentMethodModal: (paymentMethod: any) => dispatch(show(UPDATE_STRIPE_PAYMENTMETHOD_MODAL, { paymentMethod })),
        showDeleteStripePaymentMethodModal: (paymentMethod: any) => dispatch(show(DELETE_STRIPE_PAYMENTMETHOD_MODAL, { paymentMethod }))
      }
    };
  };

  return connect(
    null,
    mapDispatchToProps
  )(Container);
};
