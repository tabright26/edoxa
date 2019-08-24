import React, { useState } from "react";
import { Card, CardHeader, CardBody } from "reactstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEdit } from "@fortawesome/free-solid-svg-icons";
import withPhoneNumber from "../../../../containers/App/User/Profile/Details/withPhoneNumber";
import PhoneNumberForm from "../../../../forms/User/PhoneNumber";

const PhoneNumberCard = ({ className, phoneNumber }) => {
  const [isFormHidden, setFormHidden] = useState(true);
  return (
    <Card className={className}>
      <CardHeader>
        <strong>PHONE NUMBER</strong>
        {isFormHidden ? (
          <div className="card-header-actions btn-link" onClick={() => setFormHidden(false)}>
            <small>
              <FontAwesomeIcon icon={faEdit} /> UPDATE
            </small>
          </div>
        ) : null}
      </CardHeader>
      <CardBody>
        <dl className="row mb-0">
          <dd className="col-sm-3 text-muted mb-0">Phone Number</dd>
          <dd className="col-sm-5 mb-0">{isFormHidden ? <span>{phoneNumber}</span> : <PhoneNumberForm.Update handleCancel={() => setFormHidden(true)} />}</dd>
        </dl>
      </CardBody>
    </Card>
  );
};

export default withPhoneNumber(PhoneNumberCard);
