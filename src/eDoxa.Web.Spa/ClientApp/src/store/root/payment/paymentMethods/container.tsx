import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { show } from "redux-modal";
import { CREATE_STRIPE_PAYMENTMETHOD_MODAL, UPDATE_STRIPE_PAYMENTMETHOD_MODAL, DELETE_STRIPE_PAYMENTMETHOD_MODAL } from "modals";
import { loadPaymentMethods, attachPaymentMethod, updatePaymentMethod, detachPaymentMethod } from "store/root/payment/paymentMethods/actions";
import { RootState } from "store/root/types";
import { PaymentMethodType, CARD_PAYMENTMETHOD_TYPE } from "./types";

export const withStripePaymentMethods: any = (paymentMethodType: PaymentMethodType = CARD_PAYMENTMETHOD_TYPE) => (HighOrderComponent: FunctionComponent<any>) => {
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
        data: state.payment.paymentMethods.data.data.filter(paymentMethod => paymentMethod.type === paymentMethodType)
      }
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    const attachPaymentMethodAction = (paymentMethodId: string) => dispatch(attachPaymentMethod(paymentMethodId));
    return {
      actions: {
        loadPaymentMethods: () => dispatch(loadPaymentMethods(paymentMethodType)),
        detachPaymentMethod: (paymentMethodId: string) => dispatch(detachPaymentMethod(paymentMethodId)).then(() => dispatch(loadPaymentMethods(paymentMethodType))),
        createPaymentMethod: (fields, stripe) =>
          stripe.createPaymentMethod(paymentMethodType).then(result => {
            if (result.paymentMethod) {
              return attachPaymentMethodAction(result.paymentMethod.id).then(() => dispatch(loadPaymentMethods(paymentMethodType)));
            } else {
              return Promise.reject(result.error);
            }
          }),
        updatePaymentMethod: (paymentMethodId: string, data: any) =>
          dispatch(updatePaymentMethod(paymentMethodId, data.card.exp_month, data.card.exp_year)).then(() => dispatch(loadPaymentMethods(paymentMethodType))),
        showCreatePaymentMethodModal: () => dispatch(show(CREATE_STRIPE_PAYMENTMETHOD_MODAL)),
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
