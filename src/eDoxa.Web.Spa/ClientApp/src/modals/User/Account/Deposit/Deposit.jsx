import React from "react";
import { connectModal } from "redux-modal";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { DEPOSIT_MODAL } from "modals";
import AccountForm from "forms/User/Account";

const DepositModal = ({ show, handleHide, actions, amounts }) => (
  <Modal isOpen={show} toggle={handleHide}>
    <ModalHeader toggle={handleHide}>DEPOSIT (MONEY)</ModalHeader>
    <ModalBody>
      <AccountForm.Deposit initialValues={{ amounts }} onSubmit={fields => actions.deposit(fields.amount).then(() => handleHide())} handleCancel={handleHide} />
    </ModalBody>
  </Modal>
);

export default connectModal({ name: DEPOSIT_MODAL })(DepositModal);
