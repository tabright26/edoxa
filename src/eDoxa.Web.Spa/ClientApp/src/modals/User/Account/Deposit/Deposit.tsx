import React, { FunctionComponent } from "react";
import { connectModal } from "redux-modal";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { DEPOSIT_MODAL } from "modals";
import AccountForm from "forms/User/Account/Deposit";

const DepositModal: FunctionComponent<any> = ({ show, handleHide, actions, amounts }) => {
  const initialValues: any = { amounts };
  return (
    <Modal isOpen={show} toggle={handleHide}>
      <ModalHeader toggle={handleHide}>DEPOSIT (MONEY)</ModalHeader>
      <ModalBody>
        <AccountForm initialValues={initialValues} onSubmit={fields => actions.deposit(fields.amount).then(() => handleHide())} handleCancel={handleHide} />
      </ModalBody>
    </Modal>
  );
};

export default connectModal({ name: DEPOSIT_MODAL })(DepositModal);
