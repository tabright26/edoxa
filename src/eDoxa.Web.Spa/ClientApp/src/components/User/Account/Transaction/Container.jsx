import React, { Component } from 'react';
import { connect } from 'react-redux';
import { loadUserAccountTransactions } from '../../../../store/actions/userAccountActions';

const withUserAccountTransactionsContainer = WrappedComponent => {
  class UserAccountTransactionsContainer extends Component {
    componentDidMount() {
      this.props.actions.loadUserAccountTransactions();
    }

    render() {
      return <WrappedComponent transactions={this.props.transactions} />;
    }
  }

  const mapStateToProps = state => {
    return {
      transactions: state.cashier.transactions
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        loadUserAccountTransactions: () =>
          dispatch(loadUserAccountTransactions())
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(UserAccountTransactionsContainer);
};

export default withUserAccountTransactionsContainer;
