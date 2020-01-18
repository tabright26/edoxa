import React, { FunctionComponent, useEffect } from "react";
import { connect, MapStateToProps } from "react-redux";
import { loadStripePaymentMethods } from "store/actions/payment";
import { RootState } from "store/types";
import { StripePaymentMethodsState } from "./types";

interface StateProps {
  readonly paymentMethods: StripePaymentMethodsState;
}

interface OwnProps {}

export const withStripePaymentMethods = (
  HighOrderComponent: FunctionComponent<any>
) => {
  const Container: FunctionComponent<any> = props => {
    useEffect(() => {
      if (!props.paymentMethods.data.length) {
        props.loadStripePaymentMethods();
      }
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps: MapStateToProps<
    StateProps,
    OwnProps,
    RootState
  > = state => {
    return {
      paymentMethods: state.root.payment.stripe.paymentMethods
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      loadStripePaymentMethods: () => dispatch(loadStripePaymentMethods())
    };
  };

  return connect(mapStateToProps, mapDispatchToProps)(Container);
};
