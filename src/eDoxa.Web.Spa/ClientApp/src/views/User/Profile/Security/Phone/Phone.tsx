import React, { useState, FunctionComponent } from "react";
import { Card, CardHeader, CardBody } from "reactstrap";
import { faEdit } from "@fortawesome/free-solid-svg-icons";
import Badge from "components/Shared/Badge";
import { withUserPhone } from "store/root/user/phone/container";
import UserPhoneForm from "components/User/Phone/Form";
import { compose } from "recompose";
import Button from "components/Shared/Button";
import Loading from "components/Shared/Loading";

const Phone: FunctionComponent<any> = ({
  className,
  phone: {
    data: { number, verified },
    error,
    loading
  }
}) => {
  const [buttonDisabled, setButtonDisabled] = useState(false);
  return (
    <Card className={`card-accent-primary ${className}`}>
      <CardHeader className="d-flex">
        <strong className="text-uppercase my-auto">PHONE</strong>
        <Badge.Verification className="ml-3 my-auto" verified={verified} />
        <Button.Link className="p-0 ml-auto my-auto" icon={faEdit} disabled={buttonDisabled} onClick={() => setButtonDisabled(true)}>
          UPDATE
        </Button.Link>
      </CardHeader>
      <CardBody>
        {loading ? (
          <Loading />
        ) : (
          <dl className="row mb-0">
            <dd className="col-sm-3 text-muted mb-0">Number</dd>
            <dd className="col-sm-5 mb-0">{buttonDisabled || !number ? <UserPhoneForm.Update handleCancel={() => setButtonDisabled(false)} /> : <span>{number}</span>}</dd>
          </dl>
        )}
      </CardBody>
    </Card>
  );
};

const enhance = compose<any, any>(withUserPhone);

export default enhance(Phone);
