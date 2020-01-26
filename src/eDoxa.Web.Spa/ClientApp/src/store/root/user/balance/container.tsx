import React, { FunctionComponent, useEffect } from "react";
import { connect, MapStateToProps } from "react-redux";
import { loadUserBalance } from "store/actions/cashier";
import { RootState } from "store/types";
import { Currency } from "types";
import { UserBalanceState } from "./types";

interface UserAccountBalanceStateProps {
  balance: UserBalanceState;
}

interface UserAccountBalanceOwnProps {
  currency: Currency;
}

export const withUserAccountBalance = (
  HighOrderComponent: FunctionComponent<any>
) => {
  const Container: FunctionComponent<any> = props => {
    useEffect((): void => {
      props.loadUserAccountBalance();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps: MapStateToProps<
    UserAccountBalanceStateProps,
    UserAccountBalanceOwnProps,
    RootState
  > = (state, ownProps) => {
    return {
      balance: state.root.user.balance[ownProps.currency]
    };
  };

  const mapDispatchToProps = (
    dispatch: any,
    ownProps: UserAccountBalanceOwnProps
  ) => {
    return {
      loadUserAccountBalance: () => dispatch(loadUserBalance(ownProps.currency))
    };
  };

  return connect(mapStateToProps, mapDispatchToProps)(Container);
};
