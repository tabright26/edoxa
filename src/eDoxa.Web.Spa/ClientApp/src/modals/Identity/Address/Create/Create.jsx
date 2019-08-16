import React, { Component } from "react";
import { connect } from "react-redux";
import { connectModal } from "redux-modal";
import { Modal, ModalBody, ModalHeader } from "reactstrap";
import AddressForm from "../../../../forms/Identity/Address";
import { loadAddressBook, addAddress } from "../../../../store/actions/identityActions";
import { CREATE_ADDRESS_MODAL } from "../modals";

class CreateAddressModal extends Component {
  submit = values => {
    // print the form values to the console
    this.props
      .dispatch(addAddress(values))
      .then(() => {
        this.props
          .dispatch(loadAddressBook())
          .then(() => this.props.handleHide())
          .catch(console.log);
      })
      .catch(console.log);
  };

  render() {
    const { show, handleHide, className } = this.props;
    return (
      <Modal size="lg" isOpen={show} toggle={handleHide} className={"modal-primary " + className}>
        <ModalHeader toggle={handleHide}>Add new address</ModalHeader>
        <ModalBody>
          <dl className="row mb-0">
            <dd className="col-sm-2 mb-0 text-muted">New address</dd>
            <dd className="col-sm-8 mb-0">
              <AddressForm.Create onSubmit={this.submit} handleCancel={handleHide} />
            </dd>
          </dl>
        </ModalBody>
      </Modal>
    );
  }
}

export default connect()(connectModal({ name: CREATE_ADDRESS_MODAL })(CreateAddressModal));
