import React from "react";
import { Field, FormSection, reduxForm } from "redux-form";
import { FormGroup, Form } from "reactstrap";
import Button from "../../../../components/Button";
import Input from "../../../../components/Input";
import validate from "./validate";
import { CREATE_PERSONALINFO_FORM } from "../forms";

const days = () => {
  const days = [];
  for (let day = 1; day <= 31; day++) {
    days.push(day);
  }
  return days;
};

const months = () => {
  const months = [];
  for (let month = 1; month <= 12; month++) {
    months.push(month);
  }
  return months;
};

const years = () => {
  const date = new Date(Date.now());
  const years = [];
  console.log(date.getFullYear());
  for (let year = date.getFullYear() - 100; year <= date.getFullYear(); year++) {
    years.push(year);
  }
  return years;
};

const CreatePersonalInfoForm = ({ handleSubmit }) => (
  <Form onSubmit={handleSubmit}>
    <dl class="row mb-0">
      <dd class="col-sm-3 text-muted mb-0">Name</dd>
      <dd class="col-sm-9 mb-0">
        <dl class="row">
          <dt class="col-sm-6 mb-0">
            <Field name="firstName" label="Enter your first name" component={props => <Input.Text {...props} />} />
          </dt>
          <dd class="col-sm-6 mb-0">
            <Field name="lastName" label="Enter your last name" component={props => <Input.Text {...props} />} />
          </dd>
        </dl>
      </dd>
      <dd class="col-sm-3 text-muted mb-0">Birth date</dd>
      <dd class="col-sm-9 mb-0">
        <FormSection name="birthDate" component={FormGroup}>
          <Field className="d-inline" name="year" type="select" style={{ width: "75px" }} component={props => <Input.Select {...props} />}>
            <option value="">yyyy</option>
            {years().map((year, index) => (
              <option key={index} value={year}>
                {year}
              </option>
            ))}
          </Field>
          <span className="d-inline mx-2">/</span>
          <Field className="d-inline" name="month" type="select" style={{ width: "60px" }} component={props => <Input.Select {...props} />}>
            <option value="">MM</option>
            {months().map((month, index) => (
              <option key={index} value={month}>
                {month}
              </option>
            ))}
          </Field>
          <span className="d-inline mx-2">/</span>
          <Field className="d-inline" name="day" type="select" style={{ width: "60px" }} component={props => <Input.Select {...props} />}>
            <option value="">dd</option>
            {days().map((day, index) => (
              <option key={index} value={day}>
                {day}
              </option>
            ))}
          </Field>
        </FormSection>
      </dd>
      <dd class="col-sm-3 text-muted mb">Gender</dd>
      <dd class="col-sm-5 mb-0">
        <Field name="gender" type="select" component={props => <Input.Select {...props} />}>
          <option value="">Select your gender...</option>
          <option value="Male">Male</option>
          <option value="Female">Female</option>
          <option value="Other">Other</option>
        </Field>
        <Button.Save className="mt-3" />
      </dd>
    </dl>
  </Form>
);

export default reduxForm({ form: CREATE_PERSONALINFO_FORM, validate })(CreatePersonalInfoForm);
