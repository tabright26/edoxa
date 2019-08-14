import React, { Component } from "react";
import { connect } from "react-redux";
import { Modal, ModalBody, ModalHeader } from "reactstrap";

import AddressBookForm from "../../../../../forms/Identity/AddressBook/Create/Create";

import { addAddress } from "../../../../../store/actions/identityActions";

class AddressBookModal extends Component {
  submit = values => {
    // print the form values to the console
    this.props.dispatch(addAddress(values));
  };

  render() {
    const { className, isOpen, toggle } = this.props;
    return (
      <Modal size="lg" isOpen={isOpen} toggle={toggle} className={"modal-primary " + className}>
        <ModalHeader toggle={toggle}>Add new address</ModalHeader>
        <ModalBody>
          <dl className="row mb-0">
            <dd className="col-sm-2 mb-0 text-muted">New address</dd>
            <dd className="col-sm-8 mb-0">
              <AddressBookForm onSubmit={this.submit} onCancel={toggle} />
            </dd>
          </dl>
        </ModalBody>
      </Modal>
    );
  }
}

export default connect()(AddressBookModal);
