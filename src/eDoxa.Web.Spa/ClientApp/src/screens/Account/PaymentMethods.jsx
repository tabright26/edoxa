import React, { Component } from 'react';
import { connect } from 'react-redux';

import {
  loadUserStripeCards,
  loadUserStripeBankAccounts
} from '../../store/actions/userAccountActions';

class PaymentMethods extends Component {
  componentDidMount() {
    this.props.actions.loadUserStripeCards();
    this.props.actions.loadUserStripeBankAccounts();
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
      loadUserStripeCards: () => dispatch(loadUserStripeCards()),
      loadUserStripeBankAccounts: () => dispatch(loadUserStripeBankAccounts())
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(PaymentMethods);
