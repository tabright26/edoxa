import React, { FunctionComponent } from "react";
import { injectStripe } from "react-stripe-elements";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { connectModal } from "redux-modal";
import { CHANGE_BANKACCOUNT_MODAL } from "modals";
import { connectStripeBankAccount } from "store/root/payment/bankAccount/container";
import BankAccountForm from "forms/Payment/BankAccount";

const CreateStripeBankModal: FunctionComponent<any> = ({ show, handleHide, className, actions, stripe }) => (
  <Modal size="lg" isOpen={show} toggle={handleHide} className={"modal-primary " + className}>
    <ModalHeader toggle={handleHide}>Add new bank</ModalHeader>
    <ModalBody>
      <BankAccountForm.Update onSubmit={() => actions.createBankAccount({}, stripe)} handleCancel={handleHide} />
    </ModalBody>
  </Modal>
);

export default injectStripe(connectModal({ name: CHANGE_BANKACCOUNT_MODAL })(connectStripeBankAccount(CreateStripeBankModal)));
