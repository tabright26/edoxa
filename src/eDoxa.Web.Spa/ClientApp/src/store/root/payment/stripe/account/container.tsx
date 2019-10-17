import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { RootState } from "store/root/types";
import { loadStripeAccount } from "./actions";

export const withStripeAccount = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect((): void => {
      props.loadStripeAccount();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState) => {
    return {
      account: state.payment.stripe.account
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      loadStripeAccount: () => dispatch(loadStripeAccount())
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
