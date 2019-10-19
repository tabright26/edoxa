import React, { FunctionComponent, useEffect } from "react";
import { connect, MapStateToProps } from "react-redux";
import { loadUserAccountBalance } from "store/root/user/account/balance/actions";
import { RootState } from "store/types";
import { Currency } from "types";
import { UserAccountBalanceState } from "./types";

interface UserAccountBalanceStateProps {
  balance: UserAccountBalanceState;
}

interface UserAccountBalanceOwnProps {
  currency: Currency;
}

export const withUserAccountBalance = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect((): void => {
      props.loadUserAccountBalance();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps: MapStateToProps<UserAccountBalanceStateProps, UserAccountBalanceOwnProps, RootState> = (state, ownProps) => {
    return {
      balance: state.root.user.account.balance[ownProps.currency]
    };
  };

  const mapDispatchToProps = (dispatch: any, ownProps: UserAccountBalanceOwnProps) => {
    return {
      loadUserAccountBalance: () => dispatch(loadUserAccountBalance(ownProps.currency))
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
