import React, { Component } from "react";
import { connect } from "react-redux";
import { SubmissionError } from "redux-form";
import { show } from "redux-modal";
import { CREATE_BANK_ACCOUNT_MODAL } from "modals";
import { loadBankAccounts, createBankAccount, deleteBankAccount, updateBankAccount } from "actions/stripe/actionCreators";
import { CreateBankAccountActionType, DeleteBankAccountActionType, UpdateBankAccountActionType } from "actions/stripe/actionTypes";
import { IAxiosAction } from "interfaces/axios";

const connectStripeBankAccounts = WrappedComponent => {
  class Container extends Component<any> {
    componentDidMount() {
      this.props.actions.loadBankAccounts();
    }

    render() {
      const { bankAccounts, hasBankAccount, ...attributes } = this.props;
      return <WrappedComponent bankAccounts={bankAccounts} {...attributes} hasBankAccount={hasBankAccount} />;
    }
  }

  const mapStateToProps = state => {
    return {
      bankAccounts: state.stripe.bankAccounts,
      hasBankAccount: state.stripe.bankAccounts.data.lenth
    };
  };

  const mapDispatchToProps = dispatch => {
    return {
      actions: {
        loadBankAccounts: () => dispatch(loadBankAccounts()),
        createBankAccount: async data => {
          // const { token, error } = await stripe.createToken("bank_account", {
          //   country: "US",
          //   currency: "usd",
          //   routing_number: "110000000",
          //   account_number: "000123456789",
          //   account_holder_name: "Jenny Rosen",
          //   account_holder_type: "individual"
          // });
          dispatch(createBankAccount(data)).then(async (action: IAxiosAction<CreateBankAccountActionType>) => {
            switch (action.type) {
              case "CREATE_BANK_ACCOUNT_SUCCESS":
                await dispatch(loadBankAccounts());
                break;
              case "CREATE_BANK_ACCOUNT_FAIL":
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
          dispatch(updateBankAccount(bankAccountId, data)).then(async (action: IAxiosAction<UpdateBankAccountActionType>) => {
            switch (action.type) {
              case "UPDATE_BANK_ACCOUNT_SUCCESS":
                await dispatch(loadBankAccounts());
                break;
              case "UPDATE_BANK_ACCOUNT_FAIL":
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
          dispatch(deleteBankAccount(bankAccountId)).then(async (action: IAxiosAction<DeleteBankAccountActionType>) => {
            switch (action.type) {
              case "DELETE_BANK_ACCOUNT_SUCCESS":
                await dispatch(loadBankAccounts());
                break;
              case "DELETE_BANK_ACCOUNT_FAIL":
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
