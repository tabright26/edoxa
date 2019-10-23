import React, { FunctionComponent, useEffect } from "react";
import { connect, MapStateToProps } from "react-redux";
import { loadStripePaymentMethods } from "./actions";
import { RootState } from "store/types";
import { StripePaymentMethodType } from "types";
import { StripePaymentMethodsState } from "./types";

interface StateProps {
  readonly paymentMethods: StripePaymentMethodsState;
}

interface OwnProps {
  readonly paymentMethodType: StripePaymentMethodType;
}

export const withStripePaymentMethods = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect(() => {
      props.loadStripePaymentMethods();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (state, ownProps) => {
    const { data, error, loading } = state.root.payment.stripe.paymentMethods;
    return {
      paymentMethods: {
        data: data.filter(paymentMethod => paymentMethod.type === ownProps.paymentMethodType),
        error,
        loading
      }
    };
  };

  const mapDispatchToProps = (dispatch: any, ownProps: OwnProps) => {
    return {
      loadStripePaymentMethods: () => dispatch(loadStripePaymentMethods(ownProps.paymentMethodType))
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
