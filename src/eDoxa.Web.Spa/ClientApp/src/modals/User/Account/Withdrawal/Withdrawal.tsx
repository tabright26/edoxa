import React, { FunctionComponent } from "react";
import { connectModal } from "redux-modal";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { WITHDRAWAL_MODAL } from "modals";
import AccountForm from "forms/User/Account/Withdrawal";

const WithdrawalModal: FunctionComponent<any> = ({ show, handleHide, actions, amounts }) => (
  <Modal isOpen={show} toggle={handleHide}>
    <ModalHeader toggle={handleHide}>WITHDRAWAL (MONEY)</ModalHeader>
    <ModalBody>
      <AccountForm initialValues={{ amounts }} onSubmit={fields => actions.withdrawal(fields.amount).then(() => handleHide())} handleCancel={handleHide} />
    </ModalBody>
  </Modal>
);

export default connectModal({ name: WITHDRAWAL_MODAL })(WithdrawalModal);
