import React from "react";
import { FormGroup, Form } from "reactstrap";
import { Field, FormSection, reduxForm } from "redux-form";
import Button from "components/Shared/Override/Button";
import Input from "components/Shared/Override/Input";
import { CREATE_PERSONALINFO_FORM } from "forms";
import { months, days, years } from "./helper";
import validate from "./validate";

const CreatePersonalInfoForm = ({ handleSubmit }) => (
  <Form onSubmit={handleSubmit}>
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
      <dd className="col-sm-3 text-muted mb-0">Birth date</dd>
      <dd className="col-sm-9 mb-0">
        <FormSection name="birthDate" component={FormGroup}>
          <Field className="d-inline" name="year" type="select" style={{ width: "75px" }} component={Input.Select}>
            <option value="">yyyy</option>
            {years.map((year, index) => (
              <option key={index} value={year}>
                {year}
              </option>
            ))}
          </Field>
          <span className="d-inline mx-2">/</span>
          <Field className="d-inline" name="month" type="select" style={{ width: "60px" }} component={Input.Select}>
            <option value="">MM</option>
            {months.map((month, index) => (
              <option key={index} value={month}>
                {month}
              </option>
            ))}
          </Field>
          <span className="d-inline mx-2">/</span>
          <Field className="d-inline" name="day" type="select" style={{ width: "60px" }} component={Input.Select}>
            <option value="">dd</option>
            {days.map((day, index) => (
              <option key={index} value={day}>
                {day}
              </option>
            ))}
          </Field>
        </FormSection>
      </dd>
      <dd className="col-sm-3 text-muted mb-0">Gender</dd>
      <dd className="col-sm-3 mb-0">
        <Field name="gender" type="select" component={Input.Select}>
          <option value="">Select your gender...</option>
          <option value="Male">Male</option>
          <option value="Female">Female</option>
          <option value="Other">Other</option>
        </Field>
      </dd>
      <dd className="col-sm-6 mb-0">{""}</dd>
      <dd className="col-sm-3 mb-0">{""}</dd>
      <dd className="col-sm-9 mb-0">
        <Button.Save className="mt-3" />
      </dd>
    </dl>
  </Form>
);

export default reduxForm({ form: CREATE_PERSONALINFO_FORM, validate })(CreatePersonalInfoForm);
