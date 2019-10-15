import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadUserAccountTransactions } from "store/root/user/account/transactions/actions";
import { RootState } from "store/root/types";

export const withUserAccountTransactions = currency => (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, transactions, ...attributes }) => {
    useEffect((): void => {
      actions.loadUserAccountTransactions();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent actions={actions} transactions={transactions} {...attributes} />;
  };

  const mapStateToProps = (state: RootState) => {
    return {
      transactions: state.user.account.transactions.data.filter(transaction => transaction.currency.toLowerCase() === currency.toLowerCase())
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadUserAccountTransactions: () => dispatch(loadUserAccountTransactions(currency))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
