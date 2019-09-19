import React from "react";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { connectModal } from "redux-modal";
import { CREATE_BANK_ACCOUNT_MODAL } from "modals";

const CreateStripeBankModal = ({ show, handleHide, className, actions }) => (
  <Modal size="lg" isOpen={show} toggle={handleHide} className={"modal-primary " + className}>
    <ModalHeader toggle={handleHide}>Add new bank</ModalHeader>
    <ModalBody></ModalBody>
  </Modal>
);

export default connectModal({ name: CREATE_BANK_ACCOUNT_MODAL })(CreateStripeBankModal);
