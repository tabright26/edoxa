import React, { useState, FunctionComponent } from "react";
import { faPlus, faEdit, faTimes } from "@fortawesome/free-solid-svg-icons";
import { Card, CardHeader, CardBody } from "reactstrap";
import Address from "components/Shared/Localization/Address";
import AddressForm from "forms/User/Address";
import { withUserAddressBook } from "store/root/user/addressBook/container";
import UserAddressModal from "modals/User/Address";
import { compose } from "recompose";
import Button from "components/Shared/Override/Button";
import { withModals } from "store/middlewares/modal/container";

const AddressItem: FunctionComponent<any> = ({ hasMore, position, address }) => {
  const [updateFormHidden, hideUpdateForm] = useState(true);
  const [deleteFormHidden, hideDeleteForm] = useState(true);
  return (
    <>
      <dl className={`row ${!hasMore && "mb-0"}`}>
        <dd className="col-sm-3 m-0 text-muted">{`Address ${position}`}</dd>
        {!updateFormHidden ? (
          <dd className="col-sm-6 m-0">
            <AddressForm.Update addressId={address.id} handleCancel={() => hideUpdateForm(true)} />
          </dd>
        ) : (
          <dd className="col-sm-5 m-0">
            <Address address={address} />
            {!deleteFormHidden && <AddressForm.Delete addressId={address.id} handleCancel={() => hideDeleteForm(true)} />}
          </dd>
        )}
        {deleteFormHidden && updateFormHidden && (
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
        )}
      </dl>
      {hasMore && <hr className="border-secondary" />}
    </>
  );
};

const AddressBook: FunctionComponent<any> = ({ className, addressBook: { data, error, loading }, modals }) => (
  <Card className={`card-accent-primary ${className}`}>
    <CardHeader className="d-flex">
      <strong className="text-uppercase my-auto">ADDRESS BOOK</strong>
      <Button.Link className="p-0 ml-auto my-auto" icon={faPlus} onClick={() => modals.showCreateUserAddressModal()}>
        ADD A NEW ADDRESS
      </Button.Link>
      <UserAddressModal.Create />
    </CardHeader>
    <CardBody>
      {data.map((address, index) => (
        <AddressItem key={index} address={address} position={index + 1} hasMore={data.length !== index + 1} />
      ))}
    </CardBody>
  </Card>
);

const enhance = compose<any, any>(
  withUserAddressBook,
  withModals
);

export default enhance(AddressBook);
