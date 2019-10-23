import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { RootState } from "store/types";
import { loadStripeCustomer } from "./actions";

export const withStripeCustomer = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect((): void => {
      props.loadStripeCustomer();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState) => {
    return {
      customer: state.root.payment.stripe.customer
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      loadStripeCustomer: () => dispatch(loadStripeCustomer())
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};

export const withStripeCustomerHasDefaultPaymentMethod = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect((): void => {
      props.loadStripeCustomer();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState) => {
    const { data, error } = state.root.payment.stripe.customer;
    return {
      hasDefaultPaymentMethod: (!error && data && data.defaultPaymentMethodId) || false
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      loadStripeCustomer: () => dispatch(loadStripeCustomer())
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
