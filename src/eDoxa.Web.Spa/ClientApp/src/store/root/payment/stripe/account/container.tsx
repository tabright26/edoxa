import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { RootState } from "store/types";
import { loadStripeAccount } from "./actions";
import { selectHasAccountVerified } from "./selectors";

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
      account: state.root.payment.stripe.account
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

export const withStripeHasAccountVerified = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect((): void => {
      props.loadStripeAccount();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState) => {
    const { data, error } = state.root.payment.stripe.account;
    return {
      hasAccountVerified: selectHasAccountVerified(data, error)
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
