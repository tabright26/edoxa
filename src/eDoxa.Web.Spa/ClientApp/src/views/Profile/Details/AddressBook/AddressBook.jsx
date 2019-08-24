import React, { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlus, faEdit, faTimes } from "@fortawesome/free-solid-svg-icons";
import { Card, CardHeader, CardBody } from "reactstrap";
import Address from "../../../../components/Address";
import AddressForm from "../../../../forms/User/Address";
import withAddressBook from "../../../../containers/App/User/Profile/Details/withAddressBook";

const AddressCard = ({ index, actions, address, length }) => {
  const [updateFormHidden, hideUpdateForm] = useState(true);
  const [deleteFormHidden, hideDeleteForm] = useState(true);
  return (
    <>
      <dl className={`row ${length === index ? "mb-0" : null}`}>
        <dd className="col-sm-3 m-0 text-muted">{`Address ${index}`}</dd>
        {!updateFormHidden ? (
          <dd className="col-sm-6 m-0">
            <AddressForm.Update
              initialValues={{
                line1: address.line1,
                line2: address.line2,
                city: address.city,
                state: address.state,
                postalCode: address.postalCode
              }}
              country={address.country}
              onSubmit={fields => actions.updateAddress(address.id, fields).then(() => hideUpdateForm(true))}
              handleCancel={() => hideUpdateForm(true)}
            />
          </dd>
        ) : (
          <dd className="col-sm-5 m-0">
            <Address address={address} />
            {!deleteFormHidden ? <AddressForm.Delete onSubmit={() => actions.removeAddress(address.id).then(() => hideDeleteForm(true))} handleCancel={() => hideDeleteForm(true)} /> : null}
          </dd>
        )}
        {deleteFormHidden && updateFormHidden ? (
          <dd className="col-sm-4 mb-0 d-flex">
            <span
              className="btn-link ml-auto"
              onClick={() => {
                hideUpdateForm(true);
                hideDeleteForm(false);
              }}
            >
              <small>
                <FontAwesomeIcon icon={faTimes} /> REMOVE
              </small>
            </span>
            <span
              className="btn-link ml-auto"
              onClick={() => {
                hideDeleteForm(true);
                hideUpdateForm(false);
              }}
            >
              <small>
                <FontAwesomeIcon icon={faEdit} /> UPDATE
              </small>
            </span>
          </dd>
        ) : null}
      </dl>
      {length !== index ? <hr className="border-secondary" /> : null}
    </>
  );
};

const AddressBookCard = ({ className, addressBook, actions }) => {
  return (
    <Card className={className}>
      <CardHeader>
        <strong>ADDRESS BOOK</strong>
        <div className="card-header-actions btn-link" onClick={() => actions.showCreateAddressModal()}>
          <small>
            <FontAwesomeIcon icon={faPlus} /> ADD A NEW ADDRESS
          </small>
        </div>
      </CardHeader>
      <CardBody>
        {addressBook.map((address, index) => (
          <AddressCard key={index} index={index + 1} actions={actions} address={address} length={addressBook.length} />
        ))}
      </CardBody>
    </Card>
  );
};

export default withAddressBook(AddressBookCard);
