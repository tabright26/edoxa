import React, { useState, FunctionComponent } from "react";
import { faEdit, faTimes } from "@fortawesome/free-solid-svg-icons";
import AddressDetails from "components/User/Address/Details";
import AddressForm from "components/User/Address/Form";
import Button from "components/Shared/Button";

const AddressItem: FunctionComponent<any> = ({ hasMore, address }) => {
  const [updateFormHidden, hideUpdateForm] = useState(true);
  const [deleteFormHidden, hideDeleteForm] = useState(true);
  return (
    <>
      <dl className={`row ${!hasMore && "mb-0"}`}>
        <dd className="col-sm-3 m-0 text-muted">Address</dd>
        {!updateFormHidden ? (
          <dd className="col-sm-6 m-0">
            <AddressForm.Update
              addressId={address.id}
              handleCancel={() => hideUpdateForm(true)}
            />
          </dd>
        ) : (
          <dd className="col-sm-5 m-0">
            <AddressDetails address={address} />
            {!deleteFormHidden && (
              <AddressForm.Delete
                addressId={address.id}
                handleCancel={() => hideDeleteForm(true)}
              />
            )}
          </dd>
        )}
        {deleteFormHidden && updateFormHidden && (
          <dd className="col-sm-4 mb-0 d-flex">
            <Button.Link
              className="p-0 ml-auto"
              icon={faTimes}
              onClick={() => hideDeleteForm(false)}
            >
              REMOVE
            </Button.Link>
            <Button.Link
              className="p-0 ml-auto"
              icon={faEdit}
              onClick={() => hideUpdateForm(false)}
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

export default AddressItem;