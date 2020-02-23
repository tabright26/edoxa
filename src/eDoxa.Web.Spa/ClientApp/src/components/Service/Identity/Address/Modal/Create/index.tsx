import React, { FunctionComponent } from "react";
import { connectModal, InjectedProps } from "redux-modal";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import { CREATE_USER_ADDRESS_MODAL } from "utils/modal/constants";
import AddressForm from "components/Service/Identity/Address/Form";
import { compose } from "recompose";

type InnerProps = InjectedProps;

type OutterProps = {};

type Props = InnerProps & OutterProps;

const Create: FunctionComponent<Props> = ({ show, handleHide }) => (
  <Modal backdrop="static" centered isOpen={show} toggle={handleHide}>
    <ModalHeader className="text-uppercase my-auto" toggle={handleHide}>
      Add new address
    </ModalHeader>
    <ModalBody>
      {show && <AddressForm.Create handleCancel={handleHide} />}
    </ModalBody>
  </Modal>
);

const enhance = compose<InnerProps, OutterProps>(
  connectModal({ name: CREATE_USER_ADDRESS_MODAL, destroyOnHide: false })
);

export default enhance(Create);
