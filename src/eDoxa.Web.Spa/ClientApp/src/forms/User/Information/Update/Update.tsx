import React, { FunctionComponent } from "react";
import { Form, FormGroup } from "reactstrap";
import { Field, reduxForm, FormSection } from "redux-form";
import Input from "components/Shared/Override/Input";
import Button from "components/Shared/Override/Button";
import { UPDATE_USER_INFORMATION_FORM } from "forms";
import { validate } from "./validate";
import { compose } from "recompose";
import DaySelectField from "components/Shared/Override/Form/Field/Day";
import MonthSelectField from "components/Shared/Override/Form/Field/Month";
import YearSelectField from "components/Shared/Override/Form/Field/Year";

const UpdateUserInformationsForm: FunctionComponent<any> = ({ updateUserInformations, handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit(data => updateUserInformations(data).then(() => handleCancel()))}>
    <dl className="row mb-0">
      <dd className="col-sm-3 text-muted mb-0">Name</dd>
      <dd className="col-sm-9 mb-0">
        <dl className="row">
          <dt className="col-sm-4 mb-0">
            <Field name="firstName" label="Enter your first name" component={Input.Text} />
          </dt>
          <dd className="col-sm-4 mb-0">
            <Field name="lastName" label="Enter your first name" component={Input.Text} disabled />
          </dd>
        </dl>
      </dd>
      <dd className="col-sm-3 text-muted mb-0">Date of birth</dd>
      <dd className="col-sm-9 mb-0">
        <FormGroup>
          <FormSection name="dob">
            <YearSelectField className="d-inline" width="75px" disabled />
            <span className="d-inline mx-2">/</span>
            <MonthSelectField className="d-inline" width="60px" disabled />
            <span className="d-inline mx-2">/</span>
            <DaySelectField className="d-inline" width="60px" disabled />
          </FormSection>
        </FormGroup>
      </dd>
      <dd className="col-sm-3 mb-0 text-muted">Gender</dd>
      <dd className="col-sm-3 mb-0">
        <Field name="gender" label="Gender" component={Input.Text} disabled />
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

const enhance = compose<any, any>(reduxForm<any, { handleCancel: () => any }, string>({ form: UPDATE_USER_INFORMATION_FORM, validate }));

export default enhance(UpdateUserInformationsForm);
