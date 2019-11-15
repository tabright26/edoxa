import React, { FunctionComponent, useEffect } from "react";
import { connect, MapStateToProps } from "react-redux";
import { loadStripePaymentMethods } from "./actions";
import { RootState } from "store/types";
import { StripePaymentMethodType } from "types";
import { StripePaymentMethodsState } from "./types";
import produce, { Draft } from "immer";

interface StateProps {
  readonly paymentMethods: StripePaymentMethodsState;
}

interface OwnProps {
  readonly paymentMethodType: StripePaymentMethodType;
}

export const withStripePaymentMethods = (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect(() => {
      if (!props.paymentMethods.data.length) {
        props.loadStripePaymentMethods();
      }
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (state, ownProps) => {
    return {
      paymentMethods: produce(state.root.payment.stripe.paymentMethods, (draft: Draft<StripePaymentMethodsState>) => {
        draft.data = draft.data.filter(paymentMethod => paymentMethod.type === ownProps.paymentMethodType);
      })
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
