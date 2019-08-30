import React from "react";
import { Field, FormSection, reduxForm } from "redux-form";
import { FormGroup, Form } from "reactstrap";
import Button from "../../../../components/Button";
import Input from "../../../../components/Input";
import validate from "./validate";
import { CREATE_PERSONALINFO_FORM } from "../../../../forms";
import { months, days, years } from "../../../../utils/helper";

const CreatePersonalInfoForm = ({ handleSubmit }) => (
  <Form onSubmit={handleSubmit}>
    <dl className="row mb-0">
      <dd className="col-sm-3 text-muted mb-0">Name</dd>
      <dd className="col-sm-9 mb-0">
        <dl className="row">
          <dt className="col-sm-4 mb-0">
            <Field name="firstName" label="Enter your first name" component={props => <Input.Text {...props} />} />
          </dt>
          <dd className="col-sm-4 mb-0">
            <Field name="lastName" label="Enter your last name" component={props => <Input.Text {...props} />} />
          </dd>
        </dl>
      </dd>
      <dd className="col-sm-3 text-muted mb-0">Birth date</dd>
      <dd className="col-sm-9 mb-0">
        <FormSection name="birthDate" component={FormGroup}>
          <Field className="d-inline" name="year" type="select" style={{ width: "75px" }} component={props => <Input.Select {...props} />}>
            <option value="">yyyy</option>
            {years.map((year, index) => (
              <option key={index} value={year}>
                {year}
              </option>
            ))}
          </Field>
          <span className="d-inline mx-2">/</span>
          <Field className="d-inline" name="month" type="select" style={{ width: "60px" }} component={props => <Input.Select {...props} />}>
            <option value="">MM</option>
            {months.map((month, index) => (
              <option key={index} value={month}>
                {month}
              </option>
            ))}
          </Field>
          <span className="d-inline mx-2">/</span>
          <Field className="d-inline" name="day" type="select" style={{ width: "60px" }} component={props => <Input.Select {...props} />}>
            <option value="">dd</option>
            {days.map((day, index) => (
              <option key={index} value={day}>
                {day}
              </option>
            ))}
          </Field>
        </FormSection>
      </dd>
      <dd className="col-sm-3 text-muted mb">Gender</dd>
      <dd className="col-sm-3 mb-0">
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
