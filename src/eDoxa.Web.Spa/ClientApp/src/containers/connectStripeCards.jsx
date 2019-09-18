import React, { Component } from "react";
import { connect } from "react-redux";
import { SubmissionError } from "redux-form";
import { show } from "redux-modal";
import { CREATE_CARD_MODAL } from "modals";
import { loadCards, createCard, updateCard, deleteCard } from "actions/stripe/creators";
import actionTypes from "actions/stripe";

const connectStripeCards = WrappedComponent => {
  class Container extends Component {
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
      cards: state.stripe.cards.data
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        loadCards: () => dispatch(loadCards()),
        createCard: async data => {
          dispatch(createCard(data)).then(async action => {
            switch (action.type) {
              case actionTypes.CREATE_CARD_SUCCESS:
                await dispatch(loadCards());
                break;
              case actionTypes.CREATE_CARD_FAIL:
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
        updateCard: async (cardId, data) => {
          dispatch(updateCard(cardId, data)).then(async action => {
            switch (action.type) {
              case actionTypes.UPDATE_CARD_SUCCESS:
                await dispatch(loadCards());
                break;
              case actionTypes.UPDATE_CARD_FAIL:
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
        deleteCard: async cardId => {
          dispatch(deleteCard(cardId)).then(async action => {
            switch (action.type) {
              case actionTypes.DELETE_CARD_SUCCESS:
                await dispatch(loadCards());
                break;
              case actionTypes.DELETE_CARD_FAIL:
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
        showCreateCardModal: () => dispatch(show(CREATE_CARD_MODAL))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};

export default connectStripeCards;
