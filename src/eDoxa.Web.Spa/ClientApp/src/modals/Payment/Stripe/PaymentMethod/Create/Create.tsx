import React, { FunctionComponent } from "react";
import { injectStripe } from "react-stripe-elements";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { connectModal } from "redux-modal";
import StripePaymentMethodForm from "forms/Payment/Stripe/PaymentMethod";
import { CREATE_STRIPE_PAYMENTMETHOD_MODAL } from "modals";
import { CARD_PAYMENTMETHOD_TYPE } from "store/root/payment/paymentMethods/types";
import { withStripePaymentMethods } from "store/root/payment/paymentMethods/container";
import { compose } from "recompose";

const CreateStripePaymentMethodModal: FunctionComponent<any> = ({ show, handleHide, actions, stripe }) => (
  <Modal isOpen={show} toggle={handleHide}>
    <ModalHeader toggle={handleHide}>ADD NEW PAYMENT METHOD</ModalHeader>
    <ModalBody>
      <StripePaymentMethodForm.Create onSubmit={fields => actions.createPaymentMethod(fields, stripe).then(() => handleHide())} handleCancel={() => handleHide()} />
    </ModalBody>
  </Modal>
);

const enhance = compose<any, any>(
  injectStripe,
  connectModal({ name: CREATE_STRIPE_PAYMENTMETHOD_MODAL }),
  withStripePaymentMethods(CARD_PAYMENTMETHOD_TYPE)
);

export default enhance(CreateStripePaymentMethodModal);
