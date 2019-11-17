import React from "react";
import { connectModal } from "redux-modal";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { CREATE_USER_ADDRESS_MODAL } from "modals";
import AddressForm from "forms/User/Address";
import { compose } from "recompose";

const CreateAddressModal = ({ show, handleHide, className }) => (
  <Modal size="lg" isOpen={show} toggle={handleHide} className={"modal-primary " + className}>
    <ModalHeader toggle={handleHide}>Add new address</ModalHeader>
    <ModalBody>
      <dl className="row mb-0">
        <dd className="col-sm-2 mb-0 text-muted">New address</dd>
        <dd className="col-sm-8 mb-0">
          <AddressForm.Create handleCancel={handleHide} />
        </dd>
      </dl>
    </ModalBody>
  </Modal>
);

const enhance = compose<any, any>(connectModal({ name: CREATE_USER_ADDRESS_MODAL }));

export default enhance(CreateAddressModal);
