import React from "react";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { connectModal } from "redux-modal";
import { CREATE_CREDITCARD_MODAL } from "../../../../modals";

import { Elements } from "react-stripe-elements";
import CreditCardForm from "../../../../forms/Stripe/Card";

const CreateStripeCardModal = ({ show, handleHide, className, actions }) => (
  <Modal size="lg" isOpen={show} toggle={handleHide} className={"modal-primary " + className}>
    <ModalHeader toggle={handleHide}>Add new credit card</ModalHeader>
    <ModalBody>
      <dl className="row mb-0">
        <dd className="col-sm-8 mb-0">
          <div className="Checkout">
            <Elements>
              <CreditCardForm.Create onSubmit={data => actions.addStripeBank(data).then(() => this.toggle())} handleCancel={() => this.toggle()} />
            </Elements>
          </div>
        </dd>
      </dl>
    </ModalBody>
  </Modal>
);

export default connectModal({ name: CREATE_CREDITCARD_MODAL })(CreateStripeCardModal);
