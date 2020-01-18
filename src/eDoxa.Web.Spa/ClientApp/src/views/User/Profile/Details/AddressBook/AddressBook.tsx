import React, { useState, FunctionComponent, useEffect } from "react";
import { faPlus, faEdit, faTimes } from "@fortawesome/free-solid-svg-icons";
import { Card, CardHeader, CardBody } from "reactstrap";
import AddressDetails from "components/User/Address/Details";
import AddressForm from "components/User/Address/Form";
import UserAddressModal from "components/User/Address/Modal";
import { compose } from "recompose";
import Button from "components/Shared/Button";
import { Loading } from "components/Shared/Loading";
import { connect } from "react-redux";
import { RootState } from "store/types";
import { loadUserAddressBook } from "store/actions/identity";
import { show } from "redux-modal";
import { CREATE_USER_ADDRESS_MODAL } from "utils/modal/constants";

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

const AddressBook: FunctionComponent<any> = ({
  className,
  limit,
  addressBook: { data, error, loading },
  loadAddressBook,
  showCreateUserAddressModal
}) => {
  useEffect((): void => {
    if (data.length === 0) {
      loadAddressBook();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  return (
    <Card className={`card-accent-primary ${className}`}>
      <CardHeader className="d-flex">
        <strong className="text-uppercase my-auto">ADDRESS BOOK</strong>
        <small className="ml-2 my-auto text-muted">
          ({data.length}/{limit})
        </small>
        <Button.Link
          className="p-0 ml-auto my-auto"
          icon={faPlus}
          onClick={() => showCreateUserAddressModal()}
          disabled={data.length >= limit}
        >
          ADD A NEW ADDRESS
        </Button.Link>
        <UserAddressModal.Create />
      </CardHeader>
      <CardBody>
        {loading ? (
          <Loading />
        ) : (
          data.map((address, index) => (
            <AddressItem
              key={index}
              address={address}
              hasMore={data.length !== index + 1}
            />
          ))
        )}
      </CardBody>
    </Card>
  );
};

const mapStateToProps = (state: RootState) => {
  return {
    addressBook: state.root.user.addressBook,
    limit: state.static.identity.addressBook.limit
  };
};

const mapDispatchToProps = (dispatch: any) => {
  return {
    loadAddressBook: () => dispatch(loadUserAddressBook()),
    showCreateUserAddressModal: () => dispatch(show(CREATE_USER_ADDRESS_MODAL))
  };
};

const enhance = compose<any, any>(connect(mapStateToProps, mapDispatchToProps));

export default enhance(AddressBook);
