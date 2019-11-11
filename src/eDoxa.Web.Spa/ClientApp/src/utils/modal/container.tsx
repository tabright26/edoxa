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
  GENERATE_GAME_AUTH_FACTOR_MODAL,
  LINK_GAME_CREDENTIAL_MODAL,
  UNLINK_GAME_CREDENTIAL_MODAL,
  DEPOSIT_MODAL,
  WITHDRAWAL_MODAL
} from "modals";
import { Currency, Bundle, ChallengeParticipantMatchStat, Game } from "types";

export const withModals = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => <HighOrderComponent {...props} />;

  const mapDispatchToProps = (dispatch: any) => {
    return {
      modals: {
        showGenerateGameAuthFactorModal: (game: Game) => dispatch(show(GENERATE_GAME_AUTH_FACTOR_MODAL, { game })),
        showLinkGameCredentialModal: () => dispatch(show(LINK_GAME_CREDENTIAL_MODAL)),
        showUnlinkGameCredentialModal: () => dispatch(show(UNLINK_GAME_CREDENTIAL_MODAL)),
        showCreateUserAddressModal: () => dispatch(show(CREATE_USER_ADDRESS_MODAL)),
        showChallengeMatchScoreModal: (stats: ChallengeParticipantMatchStat[]) => dispatch(show(CHALLENGE_MATCH_SCORE_MODAL, { stats })),
        showCreateClanModal: actions => dispatch(show(CREATE_CLAN_MODAL, { actions })),
        showDepositModal: (currency: Currency, bundles: Bundle[]) => dispatch(show(DEPOSIT_MODAL, { currency, bundles })),
        showWithdrawalModal: (currency: Currency, bundles: Bundle[]) => dispatch(show(WITHDRAWAL_MODAL, { currency, bundles })),
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
