import React, { Component } from 'react';
import { connect } from 'react-redux';

import { fetchCards, hasBankAccount } from '../../store/actions/cashierActions';

class PaymentMethods extends Component {
  componentDidMount() {
    this.props.actions.fetchCards();
    this.props.actions.hasBankAccount();
  }
  render() {
    return <></>;
  }
}

const mapStateToProps = state => {
  return {
    cards: state.cashier.cards
  };
};

const mapDispatchToProps = dispatch => {
  return {
    actions: {
      fetchCards: () => dispatch(fetchCards()),
      hasBankAccount: () => dispatch(hasBankAccount())
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(PaymentMethods);
