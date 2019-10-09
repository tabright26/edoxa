import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadBankAccounts, changeBankAccount } from "store/stripe/bankAccounts/actions";
import { AppState } from "store/types";

export const connectStripeBankAccounts = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, bankAccounts, hasBankAccount, ...attributes }) => {
    useEffect((): void => {
      actions.loadBankAccounts();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <ConnectedComponent actions={actions} bankAccounts={bankAccounts} hasBankAccount={hasBankAccount} {...attributes} />;
  };

  const mapStateToProps = (state: AppState) => {
    return {
      bankAccounts: state.stripe.bankAccounts,
      hasBankAccount: state.stripe.bankAccounts.data.length
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadBankAccounts: () => dispatch(loadBankAccounts()),
        changeBankAccount: (fields, stripe) => {
          return stripe
            .createToken("bank_account", {
              country: fields.country,
              account_holder_type: "individual",
              account_holder_name: fields.accountHolderName,
              routing_number: fields.routingNumber,
              account_number: fields.accountNumber, 
              currency: "usd"
            })
            .then(result => {
              if (result.token) {
                return dispatch(changeBankAccount(result.token.id)).then(() => dispatch(loadBankAccounts()));
              } else {
                return Promise.reject(result.error);
              }
            });
        }
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
