import React, { FunctionComponent } from "react";
import { connectModal } from "redux-modal";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { DEPOSIT_MODAL } from "modals";
import UserAccountForm from "components/User/Account/Form/Deposit";
import { compose } from "recompose";

const DepositModal: FunctionComponent<any> = ({ show, handleHide, bundles, currency }) => (
  <Modal size="lg" isOpen={show} toggle={handleHide}>
    <ModalHeader toggle={handleHide}>DEPOSIT (MONEY)</ModalHeader>
    <ModalBody>
      <UserAccountForm currency={currency} bundles={bundles} handleCancel={handleHide} />
    </ModalBody>
  </Modal>
);

const enhance = compose<any, any>(connectModal({ name: DEPOSIT_MODAL }));

export default enhance(DepositModal);
