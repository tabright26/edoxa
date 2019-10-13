import React from "react";
import { injectStripe } from "react-stripe-elements";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { connectModal } from "redux-modal";
import { CREATE_BANK_ACCOUNT_MODAL } from "modals";
import { connectStripeBankAccount } from "store/stripe/bankAccount/container";
import BankAccountForm from "forms/Stripe/BankAccount";

const CreateStripeBankModal = ({ show, handleHide, className, actions, stripe }) => (
  <Modal size="lg" isOpen={show} toggle={handleHide} className={"modal-primary " + className}>
    <ModalHeader toggle={handleHide}>Add new bank</ModalHeader>
    <ModalBody>
      <BankAccountForm.Create onSubmit={() => actions.createBankAccount({}, stripe)} actions={actions} />
    </ModalBody>
  </Modal>
);

export default injectStripe(connectModal({ name: CREATE_BANK_ACCOUNT_MODAL })(connectStripeBankAccount(CreateStripeBankModal)));
