import React, { FunctionComponent } from "react";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { connectModal } from "redux-modal";
import StripePaymentMethodForm from "forms/Payment/Stripe/PaymentMethod";
import { DELETE_STRIPE_PAYMENTMETHOD_MODAL } from "modals";
import { withStripePaymentMethods } from "store/root/payment/stripe/paymentMethods/container";
import { STRIPE_PAYMENTMETHOD_CARD_TYPE } from "store/root/payment/stripe/paymentMethods/types";
import { compose } from "recompose";

const DeleteStripePaymentMethodModal: FunctionComponent<any> = ({ show, handleHide, actions, paymentMethod }) => (
  <Modal isOpen={show} toggle={handleHide}>
    <ModalHeader toggle={handleHide}>DELETE PAYMENT METHOD</ModalHeader>
    <ModalBody>
      <StripePaymentMethodForm.Delete onSubmit={() => actions.detachPaymentMethod(paymentMethod.id).then(() => handleHide())} handleCancel={() => handleHide()} />
    </ModalBody>
  </Modal>
);

const enhance = compose<any, any>(
  connectModal({ name: DELETE_STRIPE_PAYMENTMETHOD_MODAL }),
  withStripePaymentMethods(STRIPE_PAYMENTMETHOD_CARD_TYPE)
);

export default enhance(DeleteStripePaymentMethodModal);
