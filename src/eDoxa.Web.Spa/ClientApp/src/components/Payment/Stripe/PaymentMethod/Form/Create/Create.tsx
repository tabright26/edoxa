import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm } from "redux-form";
import {
  CardNumberElement,
  CardExpiryElement,
  CardCvcElement
} from "react-stripe-elements";
import Button from "components/Shared/Button";
import { CREATE_STRIPE_PAYMENTMETHOD_FORM } from "forms";
import { validate } from "./validate";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";

const CreateStripePaymentMethodForm: FunctionComponent<any> = ({
  handleSubmit,
  handleCancel,
  createStripePaymentMethod,
  error
}) => {
  return (
    <Form
      onSubmit={handleSubmit(() =>
        createStripePaymentMethod().then(() => {
          handleCancel();
        })
      )}
    >
      {error && <FormValidation error={error} />}
      <label>
        Card number
        <CardNumberElement />
      </label>
      <label>
        Expiration date
        <CardExpiryElement />
      </label>
      <label>
        CVC
        <CardCvcElement />
      </label>
      <FormGroup className="mb-0">
        <Button.Save className="mr-2" />
        <Button.Cancel onClick={handleCancel} />
      </FormGroup>
    </Form>
  );
};

const enhance = compose<any, any>(
  reduxForm<any, { handleCancel: () => any }, string>({
    form: CREATE_STRIPE_PAYMENTMETHOD_FORM,
    validate
  })
);

export default enhance(CreateStripePaymentMethodForm);
