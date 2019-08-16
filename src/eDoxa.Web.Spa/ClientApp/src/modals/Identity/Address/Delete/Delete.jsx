import React, { Component } from "react";
import { connect } from "react-redux";
import { connectModal } from "redux-modal";
import { Modal, ModalBody, ModalFooter } from "reactstrap";
import AddressForm from "../../../../forms/Identity/Address";
import { loadAddressBook, removeAddress } from "../../../../store/actions/identityActions";
import { DELETE_ADDRESS_MODAL } from "../modals";

class DeleteAddressModal extends Component {
  submit = values => {
    // print the form values to the console
    this.props
      .dispatch(removeAddress(this.props.addressId))
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
        <ModalBody>
          <h5>Are you sure you want to remove this address from your address book?</h5>
        </ModalBody>
        <ModalFooter>
          <AddressForm.Delete onSubmit={this.submit} handleCancel={handleHide} />
        </ModalFooter>
      </Modal>
    );
  }
}

export default connect()(connectModal({ name: DELETE_ADDRESS_MODAL })(DeleteAddressModal));
