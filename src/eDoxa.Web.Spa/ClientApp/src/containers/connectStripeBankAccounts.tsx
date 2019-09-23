import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { show } from "redux-modal";
import { CREATE_BANK_ACCOUNT_MODAL } from "modals";
import { loadBankAccounts, createBankAccount, deleteBankAccount, updateBankAccount } from "reducers/stripe/bankAccounts/actions";

const connectStripeBankAccounts = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, bankAccounts, hasBankAccount, ...attributes }) => {
    useEffect((): void => {
      actions.loadBankAccounts();
    });
    return <ConnectedComponent actions={actions} bankAccounts={bankAccounts} hasBankAccount={hasBankAccount} {...attributes} />;
  };

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
        createBankAccount: (data: any) => dispatch(createBankAccount(data)).then(() => dispatch(loadBankAccounts())),
        updateBankAccount: (bankAccountId: string, data: any) => dispatch(updateBankAccount(bankAccountId, data)).then(() => dispatch(loadBankAccounts())),
        deleteBankAccount: (bankAccountId: string) => dispatch(deleteBankAccount(bankAccountId)).then(() => dispatch(loadBankAccounts())),
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
