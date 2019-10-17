import React, { FunctionComponent, useEffect } from "react";
import { connect, MapStateToProps, MapDispatchToProps } from "react-redux";
import { loadUserAccountTransactions } from "store/root/user/account/transactions/actions";
import { RootState } from "store/root/types";
import { UserAccountTransactionsState } from "./types";
import { Currency, TransactionType, TransactionStatus } from "types";

interface UserAccountTransactionsStateProps {
  transactions: UserAccountTransactionsState;
}

interface UserAccountTransactionsOwnProps {
  currency?: Currency | null;
  type?: TransactionType | null;
  status?: TransactionStatus | null;
}

export const withUserAccountTransactions = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect((): void => {
      props.loadUserAccountTransactions();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps: MapStateToProps<UserAccountTransactionsStateProps, UserAccountTransactionsOwnProps, RootState> = state => {
    return {
      transactions: state.user.account.transactions
    };
  };

  const mapDispatchToProps = (dispatch: any, ownProps: UserAccountTransactionsOwnProps) => {
    return {
      loadUserAccountTransactions: () => dispatch(loadUserAccountTransactions(ownProps.currency, ownProps.type, ownProps.status))
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
