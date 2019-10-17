import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadUserAccountBalance } from "store/root/user/account/balance/actions";
import { RootState } from "store/root/types";
import { Currency, BalanceProp as BalanceSelector } from "store/root/user/account/types";

interface UserAccountBalanceProps {
  currency: Currency;
  selector: BalanceSelector;
}

export const withUserAccountBalance = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect((): void => {
      props.loadUserAccountBalance();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState, ownProps: UserAccountBalanceProps) => {
    switch (ownProps.currency) {
      case "money":
        return {
          available: state.user.account.balance.data.money.available,
          pending: state.user.account.balance.data.money.pending,
          currency: ownProps.currency,
          selector: ownProps.selector
        };
      case "token":
        return {
          available: state.user.account.balance.data.token.available,
          pending: state.user.account.balance.data.token.pending,
          currency: ownProps.currency,
          selector: ownProps.selector
        };
      default:
        throw new Error("Invalid balance currency.");
    }
  };

  const mapDispatchToProps = (dispatch: any, ownProps: UserAccountBalanceProps) => {
    return {
      loadUserAccountBalance: () => dispatch(loadUserAccountBalance(ownProps.currency))
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
