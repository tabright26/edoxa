import React, { Component } from "react";
import { connect } from "react-redux";
import { SubmissionError } from "redux-form";
import { show } from "redux-modal";
import { CREATE_BANK_MODAL } from "modals";
import { loadUserStripeBankAccounts, addStripeBank, removeStripeBank, updateStripeBank } from "actions/stripe/creators";
import actionTypes from "actions/stripe";

const withStripeBankHoc = WrappedComponent => {
  class StripeBankContainer extends Component {
    componentDidMount() {
      this.props.actions.loadUserStripeBankAccounts();
    }

    render() {
      const { bank, hasBankAccount, ...attributes } = this.props;
      return <WrappedComponent bank={bank} {...attributes} hasBankAccount={hasBankAccount} />;
    }
  }

  const mapStateToProps = state => {
    return {
      bank: state.stripe.bankAccounts.data,
      hasBankAccount: state.stripe.bankAccounts.data.lenth,
      stripeCustomerId: state.oidc.user.profile["stripe:customerId"]
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        loadUserStripeBankAccounts: () => dispatch(loadUserStripeBankAccounts()),
        addStripeBank: async data => {
          dispatch(addStripeBank(data)).then(async action => {
            switch (action.type) {
              case actionTypes.ADD_STRIPE_BANK_SUCCESS:
                await dispatch(loadUserStripeBankAccounts());
                break;
              case actionTypes.ADD_STRIPE_BANK_FAIL:
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
        removeStripeBank: async cardId => {
          dispatch(removeStripeBank(cardId)).then(async action => {
            switch (action.type) {
              case actionTypes.REMOVE_STRIPE_BANK_SUCCESS:
                await dispatch(loadUserStripeBankAccounts());
                break;
              case actionTypes.REMOVE_STRIPE_BANK_FAIL:
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
        updateStripeBank: async (cardId, data) => {
          dispatch(updateStripeBank(cardId, data)).then(async action => {
            switch (action.type) {
              case actionTypes.UPDATE_STRIPE_BANK_SUCCESS:
                await dispatch(loadUserStripeBankAccounts());
                break;
              case actionTypes.UPDATE_STRIPE_BANK_FAIL:
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
        showCreateBankModal: () => dispatch(show(CREATE_BANK_MODAL))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(StripeBankContainer);
};

export default withStripeBankHoc;
