import React, { FunctionComponent } from "react";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { connectModal } from "redux-modal";
import StripePaymentMethodForm from "forms/Payment/Stripe/PaymentMethod";
import { UPDATE_STRIPE_PAYMENTMETHOD_MODAL } from "modals";
import { compose } from "recompose";

const UpdateStripePaymentMethodModal: FunctionComponent<any> = ({ show, handleHide, paymentMethod }) => (
  <Modal size="sm" isOpen={show} toggle={handleHide}>
    <ModalHeader toggle={handleHide}>UPDATE PAYMENT METHOD</ModalHeader>
    <ModalBody>
      <StripePaymentMethodForm.Update paymentMethodId={paymentMethod.id} handleCancel={() => handleHide()} />
    </ModalBody>
  </Modal>
);

const enhance = compose<any, any>(connectModal({ name: UPDATE_STRIPE_PAYMENTMETHOD_MODAL }));

export default enhance(UpdateStripePaymentMethodModal);
