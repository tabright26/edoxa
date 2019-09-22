import React, { Component } from "react";
import { connect } from "react-redux";
import { loadPaymentMethods } from "actions/stripe/creators";

const connectStripePaymentMethods = WrappedComponent => {
  class Container extends Component<any> {
    componentDidMount() {
      this.props.actions.loadCards();
    }
    render() {
      const { cards, ...attributes } = this.props;
      return <WrappedComponent cards={cards} {...attributes} />;
    }
  }

  const mapStateToProps = state => {
    return {
      cards: state.stripe.cards
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        loadCards: () => dispatch(loadPaymentMethods("cus_F5L8mRzm6YN5ma", "card"))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};

export default connectStripePaymentMethods;
