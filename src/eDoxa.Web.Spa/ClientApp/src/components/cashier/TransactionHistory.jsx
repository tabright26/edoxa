import React, { Component } from 'react';
import { connect } from 'react-redux';

import { fetchTransactions } from '../../store/actions/cashierActions';

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
      fetchTransactions: () => dispatch(fetchTransactions())
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(TransactionHistory);
