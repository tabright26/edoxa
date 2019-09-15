import React from "react";
import { Input, Form, FormGroup } from "reactstrap";
import { Field, reduxForm } from "redux-form";
import moment from "moment";
import myInput from "components/Shared/Override/Input";
import Button from "components/Shared/Override/Button";
import { UPDATE_PERSONALINFO_FORM } from "forms";
import validate from "./validate";

// TODO: Refactor inputs into components folder.
const UpdatePersonalInfoForm = ({ handleSubmit, handleCancel, personalInfo }) => {
  const birthDate = moment.unix(personalInfo.birthDate).toDate();
  return (
    <Form onSubmit={handleSubmit}>
      <dl className="row mb-0">
        <dd className="col-sm-3 text-muted mb-0">Name</dd>
        <dd className="col-sm-9 mb-0">
          <dl className="row">
            <dt className="col-sm-4 mb-0">
              <Field name="firstName" label="Enter your first name" component={props => <myInput.Text {...props} />} />
            </dt>
            <dd className="col-sm-4 mb-0">
              <Input type="text" value={personalInfo.lastName} bsSize="sm" disabled />
            </dd>
          </dl>
        </dd>
        <dd className="col-sm-3 text-muted mb-0">Birth date</dd>
        <dd className="col-sm-9 mb-0">
          <FormGroup>
            <Input type="text" className="d-inline" value={birthDate.getDay()} name="day" bsSize="sm" style={{ width: "60px" }} disabled />
            <span className="d-inline mx-2">/</span>
            <Input type="text" className="d-inline" value={birthDate.getMonth()} name="month" bsSize="sm" style={{ width: "60px" }} disabled />
            <span className="d-inline mx-2">/</span>
            <Input className="d-inline" type="text" value={birthDate.getFullYear()} name="year" bsSize="sm" style={{ width: "75px" }} disabled />
          </FormGroup>
        </dd>
        <dd className="col-sm-3 text-muted mb">Gender</dd>
        <dd className="col-sm-3 mb-0">
          <Input type="text" value="Male" name={personalInfo.gender} bsSize="sm" disabled />
          <Button.Save className="mt-3 mr-2" />
          <Button.Cancel className="mt-3" onClick={handleCancel} />
        </dd>
      </dl>
    </Form>
  );
};

export default reduxForm({ form: UPDATE_PERSONALINFO_FORM, validate })(UpdatePersonalInfoForm);
