import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadUserAccountTransactions } from "reducers/user/account/transactions/actions";

const connectUserAccountTransactions = currency => (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, transactions, ...attributes }) => {
    useEffect((): void => {
      actions.loadUserAccountTransactions();
    });
    return <ConnectedComponent actions={actions} transactions={transactions} {...attributes} />;
  };

  const mapStateToProps = state => {
    return {
      transactions: state.user.account.transactions.filter(transaction => transaction.currency.toLowerCase() === currency.toLowerCase())
    };
  };

  const mapDispatchToProps = dispatch => {
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

export default connectUserAccountTransactions;
