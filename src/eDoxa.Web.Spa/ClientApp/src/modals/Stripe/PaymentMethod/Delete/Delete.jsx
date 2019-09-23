import React from "react";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { connectModal } from "redux-modal";
import StripePaymentMethodForm from "forms/Stripe/PaymentMethod";
import { DELETE_PAYMENTMETHOD_MODAL } from "modals";
import connectStripePaymentMethods from "store/stripe/cards/container";

const DeletePaymentMethodModal = ({ show, handleHide, actions, paymentMethod }) => (
  <Modal isOpen={show} toggle={handleHide}>
    <ModalHeader toggle={handleHide}>DELETE PAYMENT METHOD</ModalHeader>
    <ModalBody>
      <StripePaymentMethodForm.Delete onSubmit={() => actions.detachPaymentMethod(paymentMethod.id).then(() => handleHide())} handleCancel={() => handleHide()} />
    </ModalBody>
  </Modal>
);

export default connectModal({ name: DELETE_PAYMENTMETHOD_MODAL })(connectStripePaymentMethods(DeletePaymentMethodModal));
