import React from "react";
import { injectStripe } from "react-stripe-elements";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { connectModal } from "redux-modal";
import StripePaymentMethodForm from "forms/Stripe/PaymentMethod";
import { CREATE_PAYMENTMETHOD_MODAL } from "modals";
import connectStripePaymentMethods from "containers/connectStripePaymentMethods";

const CreatePaymentMethodModal = ({ show, handleHide, actions, stripe }) => (
  <Modal isOpen={show} toggle={handleHide}>
    <ModalHeader toggle={handleHide}>ADD NEW PAYMENT METHOD</ModalHeader>
    <ModalBody>
      <StripePaymentMethodForm.Create onSubmit={fields => actions.createPaymentMethod(fields, stripe, "card").then(() => handleHide())} handleCancel={() => handleHide()} />
    </ModalBody>
  </Modal>
);

export default injectStripe(connectModal({ name: CREATE_PAYMENTMETHOD_MODAL })(connectStripePaymentMethods(CreatePaymentMethodModal)));
