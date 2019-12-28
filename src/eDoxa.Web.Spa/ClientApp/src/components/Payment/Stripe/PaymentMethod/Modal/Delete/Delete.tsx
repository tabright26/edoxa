import React, { FunctionComponent } from "react";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { connectModal } from "redux-modal";
import StripePaymentMethodForm from "components/Payment/Stripe/PaymentMethod/Form";
import { DELETE_STRIPE_PAYMENTMETHOD_MODAL } from "modals";
import { compose } from "recompose";

const DeleteStripePaymentMethodModal: FunctionComponent<any> = ({
  show,
  handleHide,
  paymentMethod
}) => (
  <Modal isOpen={show} toggle={handleHide}>
    <ModalHeader toggle={handleHide}>DELETE PAYMENT METHOD</ModalHeader>
    <ModalBody>
      <StripePaymentMethodForm.Delete
        paymentMethodId={paymentMethod.id}
        handleCancel={() => handleHide()}
      />
    </ModalBody>
  </Modal>
);

const enhance = compose<any, any>(
  connectModal({
    name: DELETE_STRIPE_PAYMENTMETHOD_MODAL,
    destroyOnHide: false
  })
);

export default enhance(DeleteStripePaymentMethodModal);
