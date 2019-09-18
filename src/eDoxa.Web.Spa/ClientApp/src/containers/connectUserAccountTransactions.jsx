import React, { Component } from "react";
import { connect } from "react-redux";
import { loadUserAccountTransactions } from "actions/cashier/creators";

const connectUserAccountTransactions = WrappedComponent => {
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
      transactions: state.user.account.transactions
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        loadUserAccountTransactions: () => dispatch(loadUserAccountTransactions())
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};

export default connectUserAccountTransactions;
