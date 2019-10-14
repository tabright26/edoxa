import React, { useState } from "react";
import { Card, CardHeader, CardBody } from "reactstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEdit } from "@fortawesome/free-solid-svg-icons";
import Badge from "components/Shared/Override/Badge";
import { connectUserPhoneNumber } from "store/root/user/phoneNumber/container";
import UserPhoneForm from "forms/User/Phone";

const PhoneNumber = ({ className, phoneNumber, phoneNumberVerified, actions }) => {
  const [isFormHidden, setFormHidden] = useState(true);
  return (
    <Card className={className}>
      <CardHeader>
        <strong>PHONE NUMBER</strong>
        <Badge.Verification className="ml-3" verified={phoneNumberVerified} />
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
          <dd className="col-sm-3 text-muted mb-0">Phone number</dd>
          <dd className="col-sm-5 mb-0">
            {!isFormHidden || !phoneNumber ? (
              <UserPhoneForm.Update initialValues={{ phoneNumber }} onSubmit={fields => actions.changePhoneNumber(fields).then(() => setFormHidden(true))} handleCancel={() => setFormHidden(true)} />
            ) : (
              <span>{phoneNumber}</span>
            )}
          </dd>
        </dl>
      </CardBody>
    </Card>
  );
};

export default connectUserPhoneNumber(PhoneNumber);
