import React, { Component } from "react";
import { connect } from "react-redux";
import { SubmissionError } from "redux-form";
import { show } from "redux-modal";
import { CREATE_PAYMENTMETHOD_MODAL, UPDATE_PAYMENTMETHOD_MODAL, DELETE_PAYMENTMETHOD_MODAL } from "modals";
import { loadPaymentMethods, attachPaymentMethod, updatePaymentMethod, detachPaymentMethod } from "actions/stripe/actionCreators";
import { DetachPaymentMethodActionType, UpdatePaymentMethodActionType } from "actions/stripe/actionTypes";
import { IAxiosAction } from "interfaces/axios";

const connectStripePaymentMethods = WrappedComponent => {
  class Container extends Component<any> {
    render() {
      const { cards, ...attributes } = this.props;
      return <WrappedComponent cards={cards} {...attributes} />;
    }
  }

  const mapDispatchToProps = dispatch => {
    const attachPaymentMethodAction = (paymentMethodId, customer) => dispatch(attachPaymentMethod(paymentMethodId, customer));
    const detachPaymentMethodAction = paymentMethodId =>
      dispatch(detachPaymentMethod(paymentMethodId)).then(async (action: IAxiosAction<DetachPaymentMethodActionType>) => {
        switch (action.type) {
          case "DETACH_PAYMENTMETHOD_SUCCESS":
            await dispatch(loadPaymentMethods("cus_F5L8mRzm6YN5ma", "card"));
            break;
          case "DETACH_PAYMENTMETHOD_FAIL":
            const { isAxiosError, response } = action.error;
            if (isAxiosError) {
              throw new SubmissionError(response.data.errors);
            }
            break;
          default:
            break;
        }
      });
    const createPaymentMethodAction = (fields, stripe, type) =>
      stripe.createPaymentMethod(type).then(result => {
        if (result.paymentMethod) {
          return attachPaymentMethodAction(result.paymentMethod.id, "cus_F5L8mRzm6YN5ma").then(() => dispatch(loadPaymentMethods("cus_F5L8mRzm6YN5ma", "card")));
        } else {
          return Promise.reject(result.error);
        }
      });
    const updatePaymentMethodAction = (paymentMethodId, data) =>
      dispatch(updatePaymentMethod(paymentMethodId, data.card.exp_month, data.card.exp_year)).then((action: IAxiosAction<UpdatePaymentMethodActionType>) => {
        switch (action.type) {
          case "UPDATE_PAYMENTMETHOD_SUCCESS":
            dispatch(loadPaymentMethods("cus_F5L8mRzm6YN5ma", "card"));
            break;
          case "UPDATE_PAYMENTMETHOD_FAIL":
            const { isAxiosError, response } = action.error;
            if (isAxiosError) {
              throw new SubmissionError(response.data.errors);
            }
            break;
          default:
            break;
        }
      });

    const showCreatePaymentMethodModal = () => dispatch(show(CREATE_PAYMENTMETHOD_MODAL));
    const showUpdatePaymentMethodModal = paymentMethod => dispatch(show(UPDATE_PAYMENTMETHOD_MODAL, { paymentMethod }));
    const showDeletePaymentMethodModal = paymentMethod => dispatch(show(DELETE_PAYMENTMETHOD_MODAL, { paymentMethod }));
    return {
      actions: {
        detachPaymentMethod: detachPaymentMethodAction,
        createPaymentMethod: createPaymentMethodAction,
        updatePaymentMethod: updatePaymentMethodAction,
        showCreatePaymentMethodModal,
        showUpdatePaymentMethodModal,
        showDeletePaymentMethodModal
      }
    };
  };

  return connect(
    null,
    mapDispatchToProps
  )(Container);
};

export default connectStripePaymentMethods;
