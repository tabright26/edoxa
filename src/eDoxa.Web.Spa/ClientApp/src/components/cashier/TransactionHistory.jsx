import React, { Component } from 'react';
import { connect } from 'react-redux';

import { loadUserAccountTransactions } from '../../store/actions/userAccountActions';

class TransactionHistory extends Component {
  componentDidMount() {
    this.props.actions.fetchTransactions();
  }
  render() {
    return <></>;
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
      fetchTransactions: () => dispatch(loadUserAccountTransactions())
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(TransactionHistory);
