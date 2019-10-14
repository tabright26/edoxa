import React, { FunctionComponent } from "react";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { connectModal } from "redux-modal";
import StripePaymentMethodForm from "forms/Payment/Stripe/PaymentMethod";
import { UPDATE_STRIPE_PAYMENTMETHOD_MODAL } from "modals";
import { withStripePaymentMethods } from "store/root/payment/paymentMethods/container";
import { CARD_PAYMENTMETHOD_TYPE } from "store/root/payment/paymentMethods/types";
import { compose } from "recompose";

const UpdateStripePaymentMethodModal: FunctionComponent<any> = ({ show, handleHide, actions, paymentMethod }) => (
  <Modal size="lg" isOpen={show} toggle={handleHide}>
    <ModalHeader toggle={handleHide}>UPDATE PAYMENT METHOD</ModalHeader>
    <ModalBody>
      <StripePaymentMethodForm.Update initialValues={paymentMethod} onSubmit={fields => actions.updatePaymentMethod(paymentMethod.id, fields).then(() => handleHide())} />
    </ModalBody>
  </Modal>
);

const enhance = compose<any, any>(
  connectModal({ name: UPDATE_STRIPE_PAYMENTMETHOD_MODAL }),
  withStripePaymentMethods(CARD_PAYMENTMETHOD_TYPE)
);

export default enhance(UpdateStripePaymentMethodModal);
