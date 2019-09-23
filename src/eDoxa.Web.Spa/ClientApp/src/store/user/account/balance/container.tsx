import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadUserAccountBalance } from "store/user/account/balance/actions";
import { AppState } from "store/types";

const connectUserAccountBalance = currency => (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, currency, available, pending, ...attributes }) => {
    useEffect((): void => {
      actions.loadUserAccountBalance();
    });
    return <ConnectedComponent actions={actions} currency={currency} available={available} pending={pending} {...attributes} />;
  };

  const mapStateToProps = (state: AppState) => {
    switch (currency) {
      case "money":
        return {
          available: state.user.account.balance.money.available,
          pending: state.user.account.balance.money.pending,
          currency
        };
      case "token":
        return {
          available: state.user.account.balance.token.available,
          pending: state.user.account.balance.token.pending,
          currency
        };
      default:
        throw new Error("Invalid balance currency.");
    }
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadUserAccountBalance: () => dispatch(loadUserAccountBalance(currency))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};

export default connectUserAccountBalance;
