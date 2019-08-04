import React, { Component } from 'react';
import { connect } from 'react-redux';

import {
  loadUserStripeCards,
  hasUserStripeBankAccount
} from '../../store/actions/userAccountActions';

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
      fetchCards: () => dispatch(loadUserStripeCards()),
      hasBankAccount: () => dispatch(hasUserStripeBankAccount())
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(PaymentMethods);
