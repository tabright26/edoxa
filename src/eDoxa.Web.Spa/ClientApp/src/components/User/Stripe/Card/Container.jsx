import React, { Component } from 'react';
import { connect } from 'react-redux';
import { loadUserStripeCards } from '../../../../store/actions/userAccountActions';

const withUserStripeCardContainer = WrappedComponent => {
  class UserStripeCardContainer extends Component {
    componentDidMount() {
      this.props.actions.loadUserStripeCards();
    }

    render() {
      return <WrappedComponent cards={this.props.cards} />;
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
        loadUserStripeCards: () => dispatch(loadUserStripeCards())
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(UserStripeCardContainer);
};

export default withUserStripeCardContainer;
