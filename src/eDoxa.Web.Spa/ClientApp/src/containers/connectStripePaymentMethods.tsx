import React, { FunctionComponent } from "react";
import { connect } from "react-redux";
import { show } from "redux-modal";
import { CREATE_PAYMENTMETHOD_MODAL, UPDATE_PAYMENTMETHOD_MODAL, DELETE_PAYMENTMETHOD_MODAL } from "modals";
import { loadPaymentMethods, attachPaymentMethod, updatePaymentMethod, detachPaymentMethod } from "reducers/stripe/cards/actions";

const connectStripePaymentMethods = (ConnectedComponent: FunctionComponent<any>) => {
  const Container: FunctionComponent<any> = ({ actions, ...attributes }) => <ConnectedComponent actions={actions} {...attributes} />;

  const mapDispatchToProps = dispatch => {
    const attachPaymentMethodAction = (paymentMethodId: string, customer: string) => dispatch(attachPaymentMethod(paymentMethodId, customer));
    return {
      actions: {
        detachPaymentMethod: (paymentMethodId: string) => dispatch(detachPaymentMethod(paymentMethodId)).then(() => dispatch(loadPaymentMethods("cus_F5L8mRzm6YN5ma", "card"))),
        createPaymentMethod: (fields, stripe, type) =>
          stripe.createPaymentMethod(type).then(result => {
            if (result.paymentMethod) {
              return attachPaymentMethodAction(result.paymentMethod.id, "cus_F5L8mRzm6YN5ma").then(() => dispatch(loadPaymentMethods("cus_F5L8mRzm6YN5ma", "card")));
            } else {
              return Promise.reject(result.error);
            }
          }),
        updatePaymentMethod: (paymentMethodId: string, data: any) =>
          dispatch(updatePaymentMethod(paymentMethodId, data.card.exp_month, data.card.exp_year)).then(() => dispatch(loadPaymentMethods("cus_F5L8mRzm6YN5ma", "card"))),
        showCreatePaymentMethodModal: () => dispatch(show(CREATE_PAYMENTMETHOD_MODAL)),
        showUpdatePaymentMethodModal: (paymentMethod: any) => dispatch(show(UPDATE_PAYMENTMETHOD_MODAL, { paymentMethod })),
        showDeletePaymentMethodModal: (paymentMethod: any) => dispatch(show(DELETE_PAYMENTMETHOD_MODAL, { paymentMethod }))
      }
    };
  };

  return connect(
    null,
    mapDispatchToProps
  )(Container);
};

export default connectStripePaymentMethods;
