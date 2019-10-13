import React from "react";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { connectModal } from "redux-modal";
import StripePaymentMethodForm from "forms/Stripe/PaymentMethod";
import { UPDATE_PAYMENTMETHOD_MODAL } from "modals";
import { connectStripePaymentMethods } from "store/root/stripe/paymentMethods/container";
import { CARD_PAYMENTMETHOD_TYPE } from "store/root/stripe/paymentMethods/types";

const UpdatePaymentMethodModal = ({ show, handleHide, actions, paymentMethod }) => (
  <Modal size="lg" isOpen={show} toggle={handleHide}>
    <ModalHeader toggle={handleHide}>UPDATE PAYMENT METHOD</ModalHeader>
    <ModalBody>
      <StripePaymentMethodForm.Update initialValues={paymentMethod} onSubmit={fields => actions.updatePaymentMethod(paymentMethod.id, fields).then(() => handleHide())} />
    </ModalBody>
  </Modal>
);

export default connectModal({ name: UPDATE_PAYMENTMETHOD_MODAL })(connectStripePaymentMethods(CARD_PAYMENTMETHOD_TYPE)(UpdatePaymentMethodModal));
