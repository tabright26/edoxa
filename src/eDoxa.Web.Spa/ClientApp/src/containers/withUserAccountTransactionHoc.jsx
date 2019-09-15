import React, { Component } from "react";
import { connect } from "react-redux";
import { loadUserAccountTransactions } from "../actions/cashier/cashier";

const withUserAccountTransactionHoc = WrappedComponent => {
  class UserAccountTransactionContainer extends Component {
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
  )(UserAccountTransactionContainer);
};

export default withUserAccountTransactionHoc;
