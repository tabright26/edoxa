import React from "react";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { connectModal } from "redux-modal";
import StripePaymentMethodForm from "forms/Stripe/PaymentMethod";
import { UPDATE_PAYMENTMETHOD_MODAL } from "modals";
import connectStripePaymentMethods from "containers/connectStripePaymentMethods";

const UpdatePaymentMethodModal = ({ show, handleHide, actions, paymentMethod }) => (
  <Modal isOpen={show} toggle={handleHide}>
    <ModalHeader toggle={handleHide}>UPDATE PAYMENT METHOD</ModalHeader>
    <ModalBody>
      <dl className="row mb-0">
        <dt className="col-sm-2 mb-0">{`**** ${paymentMethod.card.last4}`}</dt>
        <dd className="col-sm-8 mb-0">
          <StripePaymentMethodForm.Update onSubmit={fields => actions.updatePaymentMethod(paymentMethod.id, fields).then(() => handleHide())} handleCancel={() => handleHide()} />
        </dd>
      </dl>
    </ModalBody>
  </Modal>
);

export default connectModal({ name: UPDATE_PAYMENTMETHOD_MODAL })(connectStripePaymentMethods(UpdatePaymentMethodModal));
