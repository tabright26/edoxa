import React, { FunctionComponent } from "react";
import { connectModal, InjectedProps } from "redux-modal";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { CREATE_USER_ADDRESS_MODAL } from "utils/modal/constants";
import AddressForm from "components/User/Address/Form";
import { compose } from "recompose";

type InnerProps = InjectedProps;

type OutterProps = {};

type Props = InnerProps & OutterProps;

const CustomModal: FunctionComponent<Props> = ({ show, handleHide }) => (
  <Modal className="modal-dialog-centered" isOpen={show} toggle={handleHide}>
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

const enhance = compose<InnerProps, OutterProps>(
  connectModal({ name: CREATE_USER_ADDRESS_MODAL, destroyOnHide: false })
);

export default enhance(CustomModal);
