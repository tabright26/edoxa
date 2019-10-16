import React, { useState, FunctionComponent } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlus, faEdit, faTimes } from "@fortawesome/free-solid-svg-icons";
import { Card, CardHeader, CardBody } from "reactstrap";
import Address from "components/Shared/Localization/Address";
import AddressForm from "forms/User/Address";
import { withUserAddressBook } from "store/root/user/addressBook/container";
import UserAddressModal from "modals/User/Address";
import { compose } from "recompose";
import Button from "components/Shared/Override/Button";

const AddressItem: FunctionComponent<any> = ({ index, actions, address, length }) => {
  const [updateFormHidden, hideUpdateForm] = useState(true);
  const [deleteFormHidden, hideDeleteForm] = useState(true);
  return (
    <>
      <dl className={`row ${length === index ? "mb-0" : null}`}>
        <dd className="col-sm-3 m-0 text-muted">{`Address ${index}`}</dd>
        {!updateFormHidden ? (
          <dd className="col-sm-6 m-0">
            <AddressForm.Update initialValues={address} onSubmit={fields => actions.updateAddress(address.id, fields).then(() => hideUpdateForm(true))} handleCancel={() => hideUpdateForm(true)} />
          </dd>
        ) : (
          <dd className="col-sm-5 m-0">
            <Address address={address} />
            {!deleteFormHidden ? <AddressForm.Delete onSubmit={() => actions.removeAddress(address.id).then(() => hideDeleteForm(true))} handleCancel={() => hideDeleteForm(true)} /> : null}
          </dd>
        )}
        {deleteFormHidden && updateFormHidden ? (
          <dd className="col-sm-4 mb-0 d-flex">
            <Button.Link
              className="p-0 ml-auto"
              icon={faTimes}
              onClick={() => {
                hideUpdateForm(true);
                hideDeleteForm(false);
              }}
            >
              REMOVE
            </Button.Link>
            <Button.Link
              className="p-0 ml-auto"
              icon={faEdit}
              onClick={() => {
                hideDeleteForm(true);
                hideUpdateForm(false);
              }}
            >
              UPDATE
            </Button.Link>
          </dd>
        ) : null}
      </dl>
      {length !== index ? <hr className="border-secondary" /> : null}
    </>
  );
};

const AddressBook: FunctionComponent<any> = ({ className, addressBook, actions }) => (
  <Card className={`card-accent-primary ${className}`}>
    <CardHeader className="d-flex">
      <strong className="text-uppercase my-auto">ADDRESS BOOK</strong>
      <Button.Link className="p-0 ml-auto my-auto" icon={faPlus} onClick={() => actions.showCreateAddressModal()}>
        ADD A NEW ADDRESS
      </Button.Link>
      <UserAddressModal.Create actions={actions} />
    </CardHeader>
    <CardBody>
      {addressBook.map((address, index) => (
        <AddressItem key={index} index={index + 1} actions={actions} address={address} length={addressBook.length} />
      ))}
    </CardBody>
  </Card>
);

const enhance = compose<any, any>(withUserAddressBook);

export default enhance(AddressBook);
