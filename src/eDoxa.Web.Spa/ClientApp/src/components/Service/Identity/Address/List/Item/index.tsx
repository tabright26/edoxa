import React, { useState, FunctionComponent } from "react";
import { faEdit, faTimes } from "@fortawesome/free-solid-svg-icons";
import Details from "components/Service/Identity/Address/Details";
import AddressForm from "components/Service/Identity/Address/Form";
import Button from "components/Shared/Button";
import { Address } from "types/identity";

type Props = {
  hasMore: boolean;
  address: Address;
};

const Item: FunctionComponent<Props> = ({ hasMore, address }) => {
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
            <Details address={address} />
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
              size="sm"
              uppercase
              onClick={() => hideDeleteForm(false)}
            >
              REMOVE
            </Button.Link>
            <Button.Link
              className="p-0 ml-auto"
              icon={faEdit}
              size="sm"
              uppercase
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

export default Item;
