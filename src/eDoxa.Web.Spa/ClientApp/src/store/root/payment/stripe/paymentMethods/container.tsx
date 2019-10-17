import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { show } from "redux-modal";
import { CREATE_STRIPE_PAYMENTMETHOD_MODAL, UPDATE_STRIPE_PAYMENTMETHOD_MODAL, DELETE_STRIPE_PAYMENTMETHOD_MODAL } from "modals";
import { loadStripePaymentMethods } from "./actions";
import { RootState } from "store/root/types";
import { StripePaymentMethodType, STRIPE_PAYMENTMETHOD_CARD_TYPE } from "./types";

export const withStripePaymentMethods = (paymentMethodType: StripePaymentMethodType = STRIPE_PAYMENTMETHOD_CARD_TYPE) => (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect(() => {
      props.actions.loadStripePaymentMethods();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState) => {
    const {
      data: { data },
      error,
      loading
    } = state.payment.stripe.paymentMethods;
    return {
      paymentMethods: {
        data: data.filter(paymentMethod => paymentMethod.type === paymentMethodType),
        error,
        loading
      }
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    return {
      actions: {
        loadStripePaymentMethods: () => dispatch(loadStripePaymentMethods(paymentMethodType)),
        showCreatePaymentMethodModal: type => dispatch(show(CREATE_STRIPE_PAYMENTMETHOD_MODAL, { type })),
        showUpdatePaymentMethodModal: (paymentMethod: any) => dispatch(show(UPDATE_STRIPE_PAYMENTMETHOD_MODAL, { paymentMethod })),
        showDeletePaymentMethodModal: (paymentMethod: any) => dispatch(show(DELETE_STRIPE_PAYMENTMETHOD_MODAL, { paymentMethod }))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
