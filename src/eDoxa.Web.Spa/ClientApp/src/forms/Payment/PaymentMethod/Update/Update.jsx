import React from "react";
import { FormGroup, Form, Label } from "reactstrap";
import { Field, reduxForm, FormSection } from "redux-form";
import Button from "components/Shared/Override/Button";
import Input from "components/Shared/Override/Input";
import { UPDATE_PAYMENTMETHOD_FORM } from "forms";
import validate from "./validate";
import { months } from "utils/helper";
import CardExpirationMonthOption from "components/Payment/Card/Expiration/Month/Option";
import CardExpirationYearOption from "components/Payment/Card/Expiration/Year/Option";
import CardBrandIcon from "components/Payment/Card/BrandIcon";

const expYearOptions = exp_year => {
  const array = [];
  for (let index = exp_year; index < exp_year + 20; index++) {
    array.push(index);
  }
  return array;
};

const UpdatePaymentMethodForm = ({
  handleSubmit,
  initialValues: {
    card: { brand, last4, exp_year }
  }
}) => (
  <Form onSubmit={handleSubmit} inline className="d-flex">
    <FormGroup>
      <div className="d-flex">
        <CardBrandIcon className="my-auto" brand={brand} size="2x" />
        <span className="my-auto ml-2">{`XXXX XXXX XXXX ${last4}`}</span>
      </div>
    </FormGroup>
    <FormSection className="mx-auto" name="card" component={FormGroup}>
      <Label className="ml-4 mr-2 text-muted">Expiration:</Label>
      <Field className="d-inline" type="select" name="exp_month" style={{ width: "55px" }} component={Input.Select}>
        {months.map((month, index) => (
          <CardExpirationMonthOption key={index} month={month} />
        ))}
      </Field>
      <span className="d-inline mx-2">/</span>
      <Field className="d-inline" type="select" name="exp_year" style={{ width: "55px" }} component={Input.Select}>
        {expYearOptions(exp_year).map((year, index) => (
          <CardExpirationYearOption key={index} year={year} />
        ))}
      </Field>
    </FormSection>
    <Button.Save />
  </Form>
);

export default reduxForm({ form: UPDATE_PAYMENTMETHOD_FORM, validate })(UpdatePaymentMethodForm);
