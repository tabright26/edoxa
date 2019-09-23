import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadPaymentMethods } from "store/stripe/cards/actions";
import { AppState } from "store/types";

const connectStripePaymentMethods = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, cards, ...attributes }) => {
    useEffect((): void => {
      actions.loadCards();
    });
    return <ConnectedComponent actions={actions} cards={cards} {...attributes} />;
  };

  const mapStateToProps = (state: AppState) => {
    return {
      cards: state.stripe.cards
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadCards: () => dispatch(loadPaymentMethods("cus_F5L8mRzm6YN5ma", "card"))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};

export default connectStripePaymentMethods;
