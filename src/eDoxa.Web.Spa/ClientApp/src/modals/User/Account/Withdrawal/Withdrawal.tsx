import React, { FunctionComponent } from "react";
import { connectModal } from "redux-modal";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { WITHDRAWAL_MODAL } from "modals";
import UserAccountForm from "forms/User/Account/Withdrawal";
import { compose } from "recompose";

const WithdrawalModal: FunctionComponent<any> = ({ show, handleHide, bundles, currency }) => (
  <Modal size="lg" isOpen={show} toggle={handleHide}>
    <ModalHeader toggle={handleHide}>WITHDRAWAL (MONEY)</ModalHeader>
    <ModalBody>
      <UserAccountForm bundles={bundles} currency={currency} handleCancel={handleHide} />
    </ModalBody>
  </Modal>
);

const enhance = compose<any, any>(connectModal({ name: WITHDRAWAL_MODAL }));

export default enhance(WithdrawalModal);
