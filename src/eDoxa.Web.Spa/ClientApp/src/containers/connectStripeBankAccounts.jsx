import React, { Component } from "react";
import { connect } from "react-redux";
import { SubmissionError } from "redux-form";
import { show } from "redux-modal";
import { CREATE_BANK_ACCOUNT_MODAL } from "modals";
import { loadBankAccounts, createBankAccount, deleteBankAccount, updateBankAccount } from "actions/stripe/creators";
import actionTypes from "actions/stripe";

const connectStripeBankAccounts = WrappedComponent => {
  class Container extends Component {
    componentDidMount() {
      this.props.actions.loadBankAccounts();
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
        loadBankAccounts: () => dispatch(loadBankAccounts()),
        createBankAccount: data => {
          dispatch(createBankAccount(data)).then(async action => {
            switch (action.type) {
              case actionTypes.CREATE_BANK_ACCOUNT_SUCCESS:
                await dispatch(loadBankAccounts());
                break;
              case actionTypes.CREATE_BANK_ACCOUNT_FAIL:
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
        updateBankAccount: (bankAccountId, data) => {
          dispatch(updateBankAccount(bankAccountId, data)).then(async action => {
            switch (action.type) {
              case actionTypes.UPDATE_BANK_ACCOUNT_SUCCESS:
                await dispatch(loadBankAccounts());
                break;
              case actionTypes.UPDATE_BANK_ACCOUNT_FAIL:
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
        deleteBankAccount: bankAccountId => {
          dispatch(deleteBankAccount(bankAccountId)).then(async action => {
            switch (action.type) {
              case actionTypes.DELETE_BANK_ACCOUNT_SUCCESS:
                await dispatch(loadBankAccounts());
                break;
              case actionTypes.DELETE_BANK_ACCOUNT_FAIL:
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
        showCreateBankAccountModal: () => dispatch(show(CREATE_BANK_ACCOUNT_MODAL))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};

export default connectStripeBankAccounts;
