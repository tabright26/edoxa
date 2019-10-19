import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { loadStripePaymentMethods } from "./actions";
import { RootState } from "store/types";

export const withStripePaymentMethods = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect(() => {
      props.loadStripePaymentMethods();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState, ownProps: any) => {
    const {
      data: { data },
      error,
      loading
    } = state.root.payment.stripe.paymentMethods;
    return {
      paymentMethods: {
        data: data.filter(paymentMethod => paymentMethod.type === ownProps.paymentMethodType),
        error,
        loading
      }
    };
  };

  const mapDispatchToProps = (dispatch: any, ownProps: any) => {
    return {
      loadStripePaymentMethods: () => dispatch(loadStripePaymentMethods(ownProps.paymentMethodType))
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
