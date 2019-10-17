import React, { FunctionComponent } from "react";
import { Form, FormGroup } from "reactstrap";
import { Field, FormSection, reduxForm } from "redux-form";
import Button from "components/Shared/Override/Button";
import Input from "components/Shared/Override/Input";
import { CREATE_USER_INFORMATION_FORM } from "forms";
import { compose } from "recompose";
import { validate } from "./validate";
import DaySelectField from "components/Shared/Override/Field/Day";
import MonthSelectField from "components/Shared/Override/Field/Month";
import YearSelectField from "components/Shared/Override/Field/Year";

const CreateUserInformationsForm: FunctionComponent<any> = ({ handleSubmit, createUserInformations }) => (
  <Form onSubmit={handleSubmit((data: any) => createUserInformations(data))}>
    <dl className="row mb-0">
      <dd className="col-sm-3 text-muted mb-0">Name</dd>
      <dd className="col-sm-9 mb-0">
        <dl className="row">
          <dt className="col-sm-4 mb-0">
            <Field name="firstName" label="Enter your first name" component={Input.Text} />
          </dt>
          <dd className="col-sm-4 mb-0">
            <Field name="lastName" label="Enter your last name" component={Input.Text} />
          </dd>
        </dl>
      </dd>
      <dd className="col-sm-3 text-muted mb-0">Date of birth</dd>
      <dd className="col-sm-9 mb-0">
        <FormGroup>
          <FormSection name="birthDate">
            <YearSelectField className="d-inline" width="75px" />
            <span className="d-inline mx-2">/</span>
            <MonthSelectField className="d-inline" width="60px" />
            <span className="d-inline mx-2">/</span>
            <DaySelectField className="d-inline" width="60px" />
          </FormSection>
        </FormGroup>
      </dd>
      <dd className="col-sm-3 text-muted mb-0">Gender</dd>
      <dd className="col-sm-3 mb-0">
        <FormGroup>
          <Field name="gender" type="select" component={Input.Select}>
            <option value="">Select your gender...</option>
            <option value="Male">Male</option>
            <option value="Female">Female</option>
            <option value="Other">Other</option>
          </Field>
        </FormGroup>
      </dd>
      <dd className="col-sm-6 mb-0">{""}</dd>
      <dd className="col-sm-3 mb-0">{""}</dd>
      <dd className="col-sm-9 mb-0">
        <Button.Save />
      </dd>
    </dl>
  </Form>
);

const enhance = compose<any, any>(reduxForm({ form: CREATE_USER_INFORMATION_FORM, validate }));

export default enhance(CreateUserInformationsForm);
