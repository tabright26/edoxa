import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadStripeBankAccount } from "./actions";
import { RootState } from "store/types";

export const withStripeBankAccount = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect((): void => {
      if (!props.bankAccount.data) {
        props.loadStripeBankAccount();
      }
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState) => {
    return {
      bankAccount: state.root.payment.stripe.bankAccount,
      hasBankAccount: state.root.payment.stripe.bankAccount.data
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      loadStripeBankAccount: () => dispatch(loadStripeBankAccount())
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
