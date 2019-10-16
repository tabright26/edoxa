import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { show } from "redux-modal";
import { CREATE_STRIPE_PAYMENTMETHOD_MODAL, UPDATE_STRIPE_PAYMENTMETHOD_MODAL, DELETE_STRIPE_PAYMENTMETHOD_MODAL } from "modals";
import { loadStripePaymentMethods, attachStripePaymentMethod, updateStripePaymentMethod, detachStripePaymentMethod } from "./actions";
import { RootState } from "store/root/types";
import { StripePaymentMethodType, STRIPE_PAYMENTMETHOD_CARD_TYPE } from "./types";

export const withStripePaymentMethods: any = (paymentMethodType: StripePaymentMethodType = STRIPE_PAYMENTMETHOD_CARD_TYPE) => (HighOrderComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = props => {
    useEffect(() => {
      props.actions.loadPaymentMethods();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <HighOrderComponent {...props} />;
  };

  const mapStateToProps = (state: RootState) => {
    return {
      paymentMethods: {
        data: state.payment.stripe.paymentMethods.data.data.filter(paymentMethod => paymentMethod.type === paymentMethodType)
      }
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    const attachPaymentMethodAction = (paymentMethodId: string) => dispatch(attachStripePaymentMethod(paymentMethodId));
    return {
      actions: {
        loadPaymentMethods: () => dispatch(loadStripePaymentMethods(paymentMethodType)),
        detachPaymentMethod: (paymentMethodId: string) => dispatch(detachStripePaymentMethod(paymentMethodId)).then(() => dispatch(loadStripePaymentMethods(paymentMethodType))),
        createPaymentMethod: (fields, stripe) =>
          stripe.createPaymentMethod(paymentMethodType).then(result => {
            if (result.paymentMethod) {
              return attachPaymentMethodAction(result.paymentMethod.id).then(() => dispatch(loadStripePaymentMethods(paymentMethodType)));
            } else {
              return Promise.reject(result.error);
            }
          }),
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
