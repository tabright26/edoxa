import React, { Component } from 'react';
import { connect } from 'react-redux';
import { loadUserStripeCards } from '../../../../../store/actions/userAccountActions';

const withUserAccountStripeCardContainer = WrappedComponent => {
  class UserAccountStripeCardContainer extends Component {
    componentDidMount() {
      this.props.actions.loadUserStripeCards();
    }

    render() {
      const { cards, ...rest } = this.props;
      return <WrappedComponent cards={cards} {...rest} />;
    }
  }

  const mapStateToProps = state => {
    return {
      cards: state.user.account.stripe.cards.data
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
  )(UserAccountStripeCardContainer);
};

export default withUserAccountStripeCardContainer;
