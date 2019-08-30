import React, { Component } from "react";
import { connect } from "react-redux";
import { loadUserStripeCards } from "../../../../store/actions/stripeActions";

const withStripeCardHoc = WrappedComponent => {
  class StripeCardContainer extends Component {
    componentDidMount() {
      this.props.actions.loadUserStripeCards();
    }

    render() {
      const { cards, ...attributes } = this.props;
      return <WrappedComponent cards={cards} {...attributes} />;
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
  )(StripeCardContainer);
};

export default withStripeCardHoc;
