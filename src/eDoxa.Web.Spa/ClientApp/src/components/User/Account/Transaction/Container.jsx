import React, { Component } from 'react';
import { connect } from 'react-redux';
import { loadUserAccountTransactions } from '../../../../store/actions/userAccountActions';

const withUserAccountTransactionContainer = WrappedComponent => {
  class UserAccountTransactionContainer extends Component {
    componentDidMount() {
      this.props.actions.loadUserAccountTransactions();
    }

    render() {
      const { transactions, ...rest } = this.props;
      return <WrappedComponent transactions={transactions} {...rest} />;
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
        loadUserAccountTransactions: () =>
          dispatch(loadUserAccountTransactions())
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(UserAccountTransactionContainer);
};

export default withUserAccountTransactionContainer;
