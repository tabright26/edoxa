import React, { FunctionComponent, useEffect } from "react";
import { connect } from "react-redux";
import { show } from "redux-modal";
import { CREATE_PAYMENTMETHOD_MODAL, UPDATE_PAYMENTMETHOD_MODAL, DELETE_PAYMENTMETHOD_MODAL } from "modals";
import { loadPaymentMethods, attachPaymentMethod, updatePaymentMethod, detachPaymentMethod } from "store/stripe/paymentMethods/actions";
import { AppState } from "store/types";
import { PaymentMethodType, CARD_PAYMENTMETHOD_TYPE } from "./types";

export const connectStripePaymentMethods = (paymentMethodType: PaymentMethodType = CARD_PAYMENTMETHOD_TYPE) => (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, paymentMethods, ...attributes }) => {
    useEffect(() => {
      actions.loadPaymentMethods();
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);
    return <ConnectedComponent actions={actions} paymentMethods={paymentMethods} {...attributes} />;
  };

  const mapStateToProps = (state: AppState) => {
    return {
      paymentMethods: {
        data: state.stripe.paymentMethods.data.filter(paymentMethod => paymentMethod.type === paymentMethodType)
      }
    };
  };

  const mapDispatchToProps = (dispatch: any) => {
    const attachPaymentMethodAction = (paymentMethodId: string, customer: string) => dispatch(attachPaymentMethod(paymentMethodId, customer));
    return {
      actions: {
        loadPaymentMethods: () => dispatch(loadPaymentMethods("cus_F5L8mRzm6YN5ma", paymentMethodType)),
        detachPaymentMethod: (paymentMethodId: string) => dispatch(detachPaymentMethod(paymentMethodId)).then(() => dispatch(loadPaymentMethods("cus_F5L8mRzm6YN5ma", paymentMethodType))),
        createPaymentMethod: (fields, stripe) =>
          stripe.createPaymentMethod(paymentMethodType).then(result => {
            if (result.paymentMethod) {
              return attachPaymentMethodAction(result.paymentMethod.id, "cus_F5L8mRzm6YN5ma").then(() => dispatch(loadPaymentMethods("cus_F5L8mRzm6YN5ma", paymentMethodType)));
            } else {
              return Promise.reject(result.error);
            }
          }),
        updatePaymentMethod: (paymentMethodId: string, data: any) =>
          dispatch(updatePaymentMethod(paymentMethodId, data.card.exp_month, data.card.exp_year)).then(() => dispatch(loadPaymentMethods("cus_F5L8mRzm6YN5ma", paymentMethodType))),
        showCreatePaymentMethodModal: () => dispatch(show(CREATE_PAYMENTMETHOD_MODAL)),
        showUpdatePaymentMethodModal: (paymentMethod: any) => dispatch(show(UPDATE_PAYMENTMETHOD_MODAL, { paymentMethod })),
        showDeletePaymentMethodModal: (paymentMethod: any) => dispatch(show(DELETE_PAYMENTMETHOD_MODAL, { paymentMethod }))
      }
    };
  };

  return connect(
    mapStateToProps,
    mapDispatchToProps
  )(Container);
};
