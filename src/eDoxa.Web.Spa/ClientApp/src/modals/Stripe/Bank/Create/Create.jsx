import React from "react";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { connectModal } from "redux-modal";
import { CREATE_BANK_MODAL } from "../../../../modals";

import { Elements } from "react-stripe-elements";
import BankForm from "../../../../forms/Stripe/Bank";

const CreateStripeBankModal = ({ show, handleHide, className, actions }) => (
  <Modal size="lg" isOpen={show} toggle={handleHide} className={"modal-primary " + className}>
    <ModalHeader toggle={handleHide}>Add new bank</ModalHeader>
    <ModalBody>
      <dl className="row mb-0">
        <dd className="col-sm-8 mb-0">
          <div className="Checkout">
            <Elements>
              <BankForm.Create onSubmit={data => actions.addStripeBank(data).then(() => this.toggle())} handleCancel={() => this.toggle()} />
            </Elements>
          </div>
        </dd>
      </dl>
    </ModalBody>
  </Modal>
);

export default connectModal({ name: CREATE_BANK_MODAL })(CreateStripeBankModal);
