import React, { FunctionComponent } from "react";
import { FormGroup, Form, Label } from "reactstrap";
import { Field, reduxForm, FormSection } from "redux-form";
import Button from "components/Shared/Override/Button";
import Input from "components/Shared/Override/Input";
import { UPDATE_STRIPE_PAYMENTMETHOD_FORM } from "forms";
import { validate } from "./validate";
import { months } from "utils/helper";
import CardExpirationMonthOption from "components/Payment/Card/Expiration/Month/Option";
import CardExpirationYearOption from "components/Payment/Card/Expiration/Year/Option";
import CardBrandIcon from "components/Payment/Card/BrandIcon";
import { compose } from "recompose";

// FRANCIS: To refactor.
const expYearOptions = exp_year => {
  const array = [];
  for (let index = exp_year; index < exp_year + 20; index++) {
    array.push(index);
  }
  return array;
};

const UpdateStripePaymentMethodForm: FunctionComponent<any> = ({
  handleSubmit,
  initialValues: {
    card: { brand, last4, exp_year }
  },
  handleCancel,
  updateStripePaymentMethod
}) => (
  <Form onSubmit={handleSubmit((data: any) => updateStripePaymentMethod(data).then(() => handleCancel()))} inline className="d-flex">
    <FormGroup>
      <div className="d-flex">
        <CardBrandIcon className="my-auto" brand={brand} size="2x" />
        <span className="my-auto ml-2">{`XXXX XXXX XXXX ${last4}`}</span>
      </div>
    </FormGroup>
    <FormSection className="mx-auto" name="card" formGroup={FormGroup}>
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

const enhance = compose<any, any>(reduxForm<any, { handleCancel: () => any }, string>({ form: UPDATE_STRIPE_PAYMENTMETHOD_FORM, validate }));

export default enhance(UpdateStripePaymentMethodForm);
