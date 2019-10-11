import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadBankAccounts, changeBankAccount } from "store/stripe/bankAccounts/actions";
import { RootState } from "store/types";

export const connectStripeBankAccounts = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, bankAccounts, hasBankAccount, ...attributes }) => {
    useEffect((): void => {
      actions.loadBankAccounts();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <ConnectedComponent actions={actions} bankAccounts={bankAccounts} hasBankAccount={hasBankAccount} {...attributes} />;
  };

  const mapStateToProps = (state: RootState) => {
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
            .createToken("person", {
              first_name: fields.firstName,
              last_name: fields.lastName,
              gender: fields.gender,
              email: fields.email,
              dob: {
                day: fields.dob.day,
                month: fields.dob.month,
                year: fields.dob.year
              },
              address: {
                line1: fields.address.line1,
                city: fields.address.city,
                state: fields.address.state,
                postal_code: fields.address.postalCode
              },
              // country: fields.country,
              // account_holder_type: "individual",
              // account_holder_name: fields.accountHolderName,
              // routing_number: fields.routingNumber,
              // account_number: fields.accountNumber,
              // currency: "usd"
              tos_shown_and_accepted: true
            })
            .then(result => {
              if (result.token) {
                console.log(result.token);
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
