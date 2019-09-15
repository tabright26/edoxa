import React, { Component } from "react";
import { connect } from "react-redux";
import { SubmissionError } from "redux-form";
import { show } from "redux-modal";
import { CREATE_CREDITCARD_MODAL } from "modals";
import { loadUserStripeCards, addStripeCreditCard, removeStripeCreditCard, updateStripeCreditCard } from "actions/stripe/creators";
import actionTypes from "actions/stripe";

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
      cards: state.stripe.cards.data,
      stripeCustomerId: state.oidc.user.profile["stripe:customerId"]
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        loadUserStripeCards: () => dispatch(loadUserStripeCards()),
        addStripeCreditCard: async data => {
          dispatch(addStripeCreditCard(data)).then(async action => {
            switch (action.type) {
              case actionTypes.ADD_STRIPE_CREDIT_CARD_SUCCESS:
                await dispatch(loadUserStripeCards());
                break;
              case actionTypes.ADD_STRIPE_CREDIT_CARD_FAIL:
                const { isAxiosError, response } = action.error;
                if (isAxiosError) {
                  throw new SubmissionError(response.data.errors);
                }
                break;
              default:
                console.error(action);
                break;
            }
          });
        },
        removeStripeCreditCard: async cardId => {
          dispatch(removeStripeCreditCard(cardId)).then(async action => {
            switch (action.type) {
              case actionTypes.REMOVE_STRIPE_CREDIT_CARD_SUCCESS:
                await dispatch(loadUserStripeCards());
                break;
              case actionTypes.REMOVE_STRIPE_CREDIT_CARD_FAIL:
                const { isAxiosError, response } = action.error;
                if (isAxiosError) {
                  throw new SubmissionError(response.data.errors);
                }
                break;
              default:
                console.error(action);
                break;
            }
          });
        },
        updateStripeCreditCard: async (cardId, data) => {
          dispatch(updateStripeCreditCard(cardId, data)).then(async action => {
            switch (action.type) {
              case actionTypes.UPDATE_STRIPE_CREDIT_CARD_SUCCESS:
                await dispatch(loadUserStripeCards());
                break;
              case actionTypes.UPDATE_STRIPE_CREDIT_CARD_FAIL:
                const { isAxiosError, response } = action.error;
                if (isAxiosError) {
                  throw new SubmissionError(response.data.errors);
                }
                break;
              default:
                console.error(action);
                break;
            }
          });
        },
        showCreateCreditCardModal: () => dispatch(show(CREATE_CREDITCARD_MODAL))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(StripeCardContainer);
};

export default withStripeCardHoc;
