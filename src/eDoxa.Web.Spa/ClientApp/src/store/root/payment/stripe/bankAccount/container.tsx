import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadStripeBankAccount, updateStripeBankAccount } from "./actions";
import { RootState } from "store/root/types";

export const withStripeBankAccount: any = (HighOrderComponent: FunctionComponent<any>): any => {
  const Container: any = props => {
    useEffect((): void => {
      props.actions.loadBankAccount();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState) => {
    return {
      bankAccount: state.payment.stripe.bankAccount,
      hasBankAccount: state.payment.stripe.bankAccount.data
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadBankAccount: () => dispatch(loadStripeBankAccount()),
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
                return dispatch(updateStripeBankAccount(result.token.id)).then(() => dispatch(loadStripeBankAccount()));
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
