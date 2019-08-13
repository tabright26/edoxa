import React, { Component, Fragment } from "react";

//import withAddressBookHoc from "../../withAddressBookHoc";

const AddressItem = ({ address }) => (
  <Fragment>
    <dd className="col-sm-3 mb-0 text-muted">Principal</dd>
    <dd className="col-sm-9 mb-0">
      <address className="mb-0">
        <span>{address.street}</span>
        <br />
        <span>
          {address.city}, {address.postalCode}
        </span>
        <br />
        <span>{address.country}</span>
      </address>
    </dd>
  </Fragment>
);

const AddressBook = ({ addressBook }) => addressBook.map(address => <AddressItem address={address} />);

class AddressBookData extends Component {
  render() {
    const { addressBook } = this.props;
    return (
      <dl className="row mb-0">
        <AddressBook addressBook={addressBook} />
      </dl>
    );
  }
}

export default AddressBookData;
