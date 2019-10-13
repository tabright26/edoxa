import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadBankAccount, changeBankAccount } from "store/root/payment/bankAccount/actions";
import { RootState } from "store/root/types";

export const connectStripeBankAccount: any = (ConnectedComponent: FunctionComponent<any>): any => {
  const Container: any = ({ actions, bankAccounts, hasBankAccount, ...attributes }) => {
    useEffect((): void => {
      actions.loadBankAccount();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <ConnectedComponent actions={actions} bankAccounts={bankAccounts} hasBankAccount={hasBankAccount} {...attributes} />;
  };

  const mapStateToProps = (state: RootState) => {
    const bankAccount = state.payment.bankAccount;
    return {
      bankAccount: bankAccount,
      hasBankAccount: bankAccount.data
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadBankAccount: () => dispatch(loadBankAccount()),
        updateBankAccount: (fields, country, stripe) => {
          return stripe
            .createToken("bank_account", {
              account_holder_name: fields.accountHolderName,
              routing_number: fields.routingNumber,
              account_number: fields.accountNumber,
              currency: fields.currency,
              country: country
            })
            .then(result => {
              if (result.token) {
                console.log(result.token);
                return dispatch(changeBankAccount(result.token.id)).then(() => dispatch(loadBankAccount()));
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
