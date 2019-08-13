import React, { Component, Fragment } from "react";

import withAddressBookHoc from "../../withAddressBookHoc";

const AddressItem = ({ address }) => (
  <Fragment>
    <dd className="col-sm-3 mb-0 text-muted">{address.type}</dd>
    <dd className="col-sm-9 mb-0">
      <address className="text-uppercase mb-0">
        <span>{address.line1}</span>
        <br />
        {address.line2 ? (
          <>
            <span>{address.line2}</span>
            <br />
          </>
        ) : null}
        <span>
          {address.city}, {address.state ? <>{address.state}</> : null} {address.postalCode}
        </span>
        <br />
        <span>{address.country}</span>
      </address>
    </dd>
  </Fragment>
);

const AddressBook = ({ addressBook }) => addressBook.map((address, index) => <AddressItem key={index} address={address} />);

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

export default withAddressBookHoc(AddressBookData);
