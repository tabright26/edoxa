import React, { Component } from "react";
import { connect } from "react-redux";
import {
  loadUserStripeCards,
  addStripeCreditCard,
  removeStripeCreditCard,
  updateStripeCreditCard,
  ADD_STRIPE_CREDIT_CARD_SUCCESS,
  ADD_STRIPE_CREDIT_CARD_FAIL,
  REMOVE_STRIPE_CREDIT_CARD_SUCCESS,
  REMOVE_STRIPE_CREDIT_CARD_FAIL,
  UPDATE_STRIPE_CREDIT_CARD_SUCCESS,
  UPDATE_STRIPE_CREDIT_CARD_FAIL
} from "../store/actions/stripeActions";

import { SubmissionError } from "redux-form";
import { show } from "redux-modal";
import { CREATE_CREDITCARD_MODAL } from "../modals";

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
      cards: state.user.account.stripe.cards.data,
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
              case ADD_STRIPE_CREDIT_CARD_SUCCESS:
                await dispatch(loadUserStripeCards());
                break;
              case ADD_STRIPE_CREDIT_CARD_FAIL:
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
              case REMOVE_STRIPE_CREDIT_CARD_SUCCESS:
                await dispatch(loadUserStripeCards());
                break;
              case REMOVE_STRIPE_CREDIT_CARD_FAIL:
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
              case UPDATE_STRIPE_CREDIT_CARD_SUCCESS:
                await dispatch(loadUserStripeCards());
                break;
              case UPDATE_STRIPE_CREDIT_CARD_FAIL:
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
