import React, { FunctionComponent } from "react";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { connectModal } from "redux-modal";
import StripePaymentMethodForm from "forms/Payment/PaymentMethod";
import { DELETE_PAYMENTMETHOD_MODAL } from "modals";
import { connectStripePaymentMethods } from "store/root/payment/paymentMethods/container";
import { CARD_PAYMENTMETHOD_TYPE } from "store/root/payment/paymentMethods/types";
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
  connectModal({ name: DELETE_PAYMENTMETHOD_MODAL }),
  connectStripePaymentMethods(CARD_PAYMENTMETHOD_TYPE)
);

export default enhance(DeleteStripePaymentMethodModal);
