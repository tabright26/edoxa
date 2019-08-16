import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlus, faEdit, faTimes } from "@fortawesome/free-solid-svg-icons";
import { Card, CardHeader, CardBody } from "reactstrap";
import Address from "../../../../components/Address";
import withAddressBook from "../../../../containers/App/User/Profile/Details/withAddressBook";

const AddressBookCard = ({ className, addressBook, actions }) => (
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
        <dl key={index} className={`row ${addressBook.length === index + 1 ? "mb-0" : null}`}>
          <dd className="col-sm-3 m-0 text-muted"> {address.type === "Principal" ? address.type : `Address ${index}`}</dd>
          <dd className="col-sm-5 m-0">
            <Address address={address} />
          </dd>
          <dd className="col-sm-4 mb-0 d-flex">
            <span className="btn-link ml-auto" onClick={() => actions.showDeleteAddressModal(address.id)}>
              <small>
                <FontAwesomeIcon icon={faTimes} /> REMOVE
              </small>
            </span>
            <span className="btn-link ml-auto" onClick={() => actions.showUpdateAddressModal(address.id)}>
              <small>
                <FontAwesomeIcon icon={faEdit} /> UPDATE
              </small>
            </span>
          </dd>
        </dl>
      ))}
    </CardBody>
  </Card>
);

export default withAddressBook(AddressBookCard);
