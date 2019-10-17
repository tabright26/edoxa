import React, { useState, FunctionComponent } from "react";
import Moment from "react-moment";
import { faEdit } from "@fortawesome/free-solid-svg-icons";
import { Card, CardHeader, CardBody } from "reactstrap";
import { withtUserInformations } from "store/root/user/informations/container";
import UserInformationForm from "forms/User/Information";
import { compose } from "recompose";
import Button from "components/Shared/Override/Button";

const Informations: FunctionComponent<any> = ({ className, informations: { data, loading, error } }) => {
  const [buttonDisabled, setbuttonDisabled] = useState(false);
  return (
    <Card className={`card-accent-primary ${className}`}>
      <CardHeader className="d-flex">
        <strong className="text-uppercase my-auto">INFORMATIONS</strong>
        <Button.Link className="p-0 ml-auto my-auto" icon={faEdit} disabled={buttonDisabled || !data} onClick={() => setbuttonDisabled(true)}>
          UPDATE
        </Button.Link>
      </CardHeader>
      <CardBody>
        {!data ? (
          <UserInformationForm.Create handleCancel={() => setbuttonDisabled(false)} />
        ) : !buttonDisabled ? (
          <dl className="row mb-0">
            <dd className="col-sm-3 text-muted">Name</dd>
            <dd className="col-sm-9">
              {data.firstName} {data.lastName}
            </dd>
            <dd className="col-sm-3 text-muted">Date of birth</dd>
            <dd className="col-sm-9">
              <Moment unix format="ll">
                {data.birthDate}
              </Moment>
            </dd>
            <dd className="col-sm-3 text-muted mb-0">Gender</dd>
            <dd className="col-sm-9 mb-0">{data.gender}</dd>
          </dl>
        ) : (
          <UserInformationForm.Update handleCancel={() => setbuttonDisabled(false)} />
        )}
      </CardBody>
    </Card>
  );
};

const enhance = compose<any, any>(withtUserInformations);

export default enhance(Informations);
