import React, { FunctionComponent } from "react";
import { FormGroup, Form, Label } from "reactstrap";
import { reduxForm, FormSection } from "redux-form";
import Button from "components/Shared/Override/Button";
import { UPDATE_STRIPE_PAYMENTMETHOD_FORM } from "forms";
import { validate } from "./validate";
import CardBrandIcon from "components/Payment/Card/BrandIcon";
import { compose } from "recompose";
import MonthSelectField from "components/Shared/Override/Field/Month";
import YearSelectField from "components/Shared/Override/Field/Year";

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
    <FormSection className="mx-auto" name="card">
      <Label className="ml-4 mr-2 text-muted">Expiration:</Label>
      <MonthSelectField className="d-inline" name="exp_month" width="55px" />
      <span className="d-inline mx-2">/</span>
      <YearSelectField className="d-inline" name="exp_year" width="55px" min={exp_year} max={exp_year + 20} descending={false} />
      {/* <Field className="d-inline" type="select" name="exp_year" style={{ width: "55px" }} component={Input.Select}>
        {expYearOptions(exp_year).map((year, index) => (
          <CardExpirationYearOption key={index} year={year} />
        ))}
      </Field> */}
    </FormSection>
    <Button.Save />
  </Form>
);

const enhance = compose<any, any>(reduxForm<any, { handleCancel: () => any }, string>({ form: UPDATE_STRIPE_PAYMENTMETHOD_FORM, validate }));

export default enhance(UpdateStripePaymentMethodForm);
