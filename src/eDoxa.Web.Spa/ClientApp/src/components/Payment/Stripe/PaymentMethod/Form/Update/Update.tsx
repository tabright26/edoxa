import React, { FunctionComponent } from "react";
import { FormGroup, Form, Label } from "reactstrap";
import { reduxForm, FormSection } from "redux-form";
import Button from "components/Shared/Button";
import { UPDATE_STRIPE_PAYMENTMETHOD_FORM } from "forms";
import CardIcon from "components/Payment/Stripe/PaymentMethod/Card/Icon";
import { compose } from "recompose";
import FormField from "components/Shared/Form/Field";
import FormValidation from "components/Shared/Form/Validation";
import {
  CC_MONTH_REQUIRED,
  CC_MONTH_INVALID,
  ccMonthRegex,
  CC_YEAR_REQUIRED,
  ccYearRegex,
  CC_YEAR_INVALID
} from "validation";

const validate = (values: any) => {
  const errors: any = {};
  if (!values.exp_month) {
    errors.exp_month = CC_MONTH_REQUIRED;
  } else if (!ccMonthRegex.test(values.exp_month)) {
    errors.exp_month = CC_MONTH_INVALID;
  }
  if (!values.exp_year) {
    errors.exp_year = CC_YEAR_REQUIRED;
  } else if (!ccYearRegex.test(values.exp_year)) {
    errors.exp_year = CC_YEAR_INVALID;
  }
  return errors;
};

const UpdateStripePaymentMethodForm: FunctionComponent<any> = ({
  handleSubmit,
  initialValues: {
    card: { brand, last4, expYear }
  },
  handleCancel,
  error,
  updateStripePaymentMethod
}) => (
  <Form
    onSubmit={handleSubmit((data: any) =>
      updateStripePaymentMethod(data).then(() => {
        handleCancel();
      })
    )}
    inline
    className="d-flex"
  >
    {error && <FormValidation error={error} />}
    <FormGroup>
      <div className="d-flex">
        <CardIcon className="my-auto" brand={brand} size="2x" />
        <span className="my-auto ml-2">{`XXXX XXXX XXXX ${last4}`}</span>
      </div>
    </FormGroup>
    <FormSection className="mx-auto" name="card">
      <Label className="ml-4 mr-2 text-muted">Expiration:</Label>
      <FormField.Month className="d-inline" name="exp_month" width="55px" />
      <span className="d-inline mx-2">/</span>
      <FormField.Year
        className="d-inline"
        name="exp_year"
        width="55px"
        min={expYear}
        max={expYear + 20}
        descending={false}
      />
    </FormSection>
    <Button.Save />
  </Form>
);

const enhance = compose<any, any>(
  reduxForm<any, { handleCancel: () => any }, string>({
    form: UPDATE_STRIPE_PAYMENTMETHOD_FORM,
    validate
  })
);

export default enhance(UpdateStripePaymentMethodForm);
