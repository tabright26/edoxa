import React, { useState } from "react";
import Moment from "react-moment";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEdit } from "@fortawesome/free-solid-svg-icons";
import { Card, CardHeader, CardBody } from "reactstrap";
import { connectUserPersonalInfo } from "store/user/personalInfo/container";
import PersonalInfoForm from "forms/User/PersonalInfo";

const PersonalInfo = ({ className, personalInfo, actions }) => {
  const [isFormHidden, setFormHidden] = useState(true);
  return (
    <Card className={className}>
      <CardHeader>
        <strong>PERSONAL INFORMATIONS</strong>
        {isFormHidden && personalInfo ? (
          <div className="card-header-actions btn-link" onClick={() => setFormHidden(false)}>
            <small>
              <FontAwesomeIcon icon={faEdit} /> UPDATE
            </small>
          </div>
        ) : null}
      </CardHeader>
      <CardBody>
        {!personalInfo ? (
          <PersonalInfoForm.Create onSubmit={fields => actions.createPersonalInfo(fields).then(() => setFormHidden(true))} handleCancel={() => setFormHidden(true)} />
        ) : isFormHidden ? (
          <dl className="row mb-0">
            <dd className="col-sm-3 text-muted">Name</dd>
            <dd className="col-sm-9">
              {personalInfo.firstName} {personalInfo.lastName}
            </dd>
            <dd className="col-sm-3 text-muted">Date of birth</dd>
            <dd className="col-sm-9">
              <Moment unix format="ll">
                {personalInfo.birthDate}
              </Moment>
            </dd>
            <dd className="col-sm-3 text-muted mb-0">Gender</dd>
            <dd className="col-sm-9 mb-0">{personalInfo.gender}</dd>
          </dl>
        ) : (
          <PersonalInfoForm.Update initialValues={personalInfo} onSubmit={fields => actions.updatePersonalInfo(fields).then(() => setFormHidden(true))} handleCancel={() => setFormHidden(true)} />
        )}
      </CardBody>
    </Card>
  );
};

export default connectUserPersonalInfo(PersonalInfo);
