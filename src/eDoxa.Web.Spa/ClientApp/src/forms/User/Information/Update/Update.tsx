import React, { FunctionComponent } from "react";
import { Form, FormGroup } from "reactstrap";
import { Field, reduxForm } from "redux-form";
import moment from "moment";
import Input from "components/Shared/Override/Input";
import Button from "components/Shared/Override/Button";
import { UPDATE_PERSONALINFO_FORM } from "forms";
import { validate } from "./validate";
import { compose } from "recompose";

const UpdateInformationForm: FunctionComponent<any> = ({ handleSubmit, handleCancel, initialValues: { lastName, birthDate, gender } }) => (
  <Form onSubmit={handleSubmit}>
    <dl className="row mb-0">
      <dd className="col-sm-3 text-muted mb-0">Name</dd>
      <dd className="col-sm-9 mb-0">
        <dl className="row">
          <dt className="col-sm-4 mb-0">
            <Field name="firstName" label="Enter your first name" component={Input.Text} />
          </dt>
          <dd className="col-sm-4 mb-0">
            <Input.Text type="text" value={lastName} bsSize="sm" disabled />
          </dd>
        </dl>
      </dd>
      <dd className="col-sm-3 text-muted mb-0">Date of birth</dd>
      <dd className="col-sm-9 mb-0">
        <FormGroup>
          <Input.Text
            type="text"
            className="d-inline"
            value={moment
              .unix(birthDate)
              .toDate()
              .getMonth()}
            name="month"
            bsSize="sm"
            style={{ width: "60px" }}
            disabled
          />
          <span className="d-inline mx-2">/</span>
          <Input.Text
            type="text"
            className="d-inline"
            value={moment
              .unix(birthDate)
              .toDate()
              .getDate()}
            name="day"
            bsSize="sm"
            style={{ width: "60px" }}
            disabled
          />
          <span className="d-inline mx-2">/</span>
          <Input.Text
            className="d-inline"
            type="text"
            value={moment
              .unix(birthDate)
              .toDate()
              .getFullYear()}
            name="year"
            bsSize="sm"
            style={{ width: "75px" }}
            disabled
          />
        </FormGroup>
      </dd>
      <dd className="col-sm-3 mb-0 text-muted">Gender</dd>
      <dd className="col-sm-3 mb-0">
        <Input.Text type="text" value="Male" name={gender} bsSize="sm" disabled />
      </dd>
      <dd className="col-sm-6 mb-0">{""}</dd>
      <dd className="col-sm-3 mb-0">{""}</dd>
      <dd className="col-sm-9 mb-0">
        <Button.Save className="mt-3 mr-2" />
        <Button.Cancel className="mt-3" onClick={handleCancel} />
      </dd>
    </dl>
  </Form>
);

const enhance = compose<any, any>(reduxForm<any, { handleCancel: () => any }, string>({ form: UPDATE_PERSONALINFO_FORM, validate }));

export default enhance(UpdateInformationForm);
