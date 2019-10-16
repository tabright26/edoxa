import React, { useState, FunctionComponent } from "react";
import Moment from "react-moment";
import { faEdit } from "@fortawesome/free-solid-svg-icons";
import { Card, CardHeader, CardBody } from "reactstrap";
import { withtUserInformations } from "store/root/user/personalInfo/container";
import UserInformationForm from "forms/User/Information";
import { compose } from "recompose";
import Button from "components/Shared/Override/Button";

const PersonalInformations: FunctionComponent<any> = ({ className, informations, actions }) => {
  const [buttonDisabled, setbuttonDisabled] = useState(false);
  return (
    <Card className={`card-accent-primary ${className}`}>
      <CardHeader className="d-flex">
        <strong className="text-uppercase my-auto">INFORMATIONS</strong>
        <Button.Link className="p-0 ml-auto my-auto" icon={faEdit} disabled={buttonDisabled || !informations} onClick={() => setbuttonDisabled(true)}>
          UPDATE
        </Button.Link>
      </CardHeader>
      <CardBody>
        {!informations ? (
          <UserInformationForm.Create onSubmit={fields => actions.createPersonalInfo(fields).then(() => setbuttonDisabled(false))} handleCancel={() => setbuttonDisabled(false)} />
        ) : !buttonDisabled ? (
          <dl className="row mb-0">
            <dd className="col-sm-3 text-muted">Name</dd>
            <dd className="col-sm-9">
              {informations.firstName} {informations.lastName}
            </dd>
            <dd className="col-sm-3 text-muted">Date of birth</dd>
            <dd className="col-sm-9">
              <Moment unix format="ll">
                {informations.birthDate}
              </Moment>
            </dd>
            <dd className="col-sm-3 text-muted mb-0">Gender</dd>
            <dd className="col-sm-9 mb-0">{informations.gender}</dd>
          </dl>
        ) : (
          <UserInformationForm.Update
            initialValues={informations}
            onSubmit={fields => actions.updatePersonalInfo(fields).then(() => setbuttonDisabled(false))}
            handleCancel={() => setbuttonDisabled(false)}
          />
        )}
      </CardBody>
    </Card>
  );
};

const enhance = compose<any, any>(withtUserInformations);

export default enhance(PersonalInformations);
