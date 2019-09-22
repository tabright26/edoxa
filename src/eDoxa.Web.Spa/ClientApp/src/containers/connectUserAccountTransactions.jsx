import React, { Component } from "react";
import { connect } from "react-redux";
import { loadUserAccountTransactions } from "actions/cashier/creators";

const connectUserAccountTransactions = currency => WrappedComponent => {
  class Container extends Component {
    componentDidMount() {
      this.props.actions.loadUserAccountTransactions();
    }

    render() {
      const { transactions, ...attributes } = this.props;
      return <WrappedComponent transactions={transactions} {...attributes} />;
    }
  }

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
