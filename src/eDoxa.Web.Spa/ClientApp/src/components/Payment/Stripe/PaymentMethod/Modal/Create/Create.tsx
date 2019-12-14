import React, { FunctionComponent } from "react";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { connectModal } from "redux-modal";
import StripePaymentMethodForm from "components/Payment/Stripe/PaymentMethod/Form";
import { CREATE_STRIPE_PAYMENTMETHOD_MODAL } from "modals";
import { compose } from "recompose";

const CreateStripePaymentMethodModal: FunctionComponent<any> = ({ show, handleHide, type }) => (
  <Modal isOpen={show} toggle={handleHide}>
    <ModalHeader toggle={handleHide}>ADD NEW PAYMENT METHOD</ModalHeader>
    <ModalBody>
      <StripePaymentMethodForm.Create type={type} handleCancel={() => handleHide()} />
    </ModalBody>
  </Modal>
);

const enhance = compose<any, any>(connectModal({ name: CREATE_STRIPE_PAYMENTMETHOD_MODAL }));

export default enhance(CreateStripePaymentMethodModal);
