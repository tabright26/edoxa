import React from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm } from "redux-form";
import { CardNumberElement, CardExpiryElement, CardCvcElement } from "react-stripe-elements";
import Button from "components/Shared/Override/Button";
import { CREATE_PAYMENTMETHOD_FORM } from "forms";
import validate from "./validate";

const CreatePaymentMethodFrom = ({ handleSubmit, handleCancel }) => {
  return (
    <Form onSubmit={handleSubmit}>
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

export default reduxForm({ form: CREATE_PAYMENTMETHOD_FORM, validate })(CreatePaymentMethodFrom);
