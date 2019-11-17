import React, { FunctionComponent } from "react";
import { Label, FormGroup, Form } from "reactstrap";
import { reduxForm } from "redux-form";
import Button from "components/Shared/Button";
import { DELETE_STRIPE_PAYMENTMETHOD_FORM } from "forms";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";

const DeleteStripePaymentMethodForm: FunctionComponent<any> = ({ handleSubmit, handleCancel, deleteStripePaymentMethod, error }) => (
  <Form
    onSubmit={handleSubmit(() =>
      deleteStripePaymentMethod().then(() => {
        handleCancel();
      })
    )}
  >
    {error && <FormValidation error={error} />}
    <Label className="mb-3">Are you sure you want to delete this payment method?</Label>
    <FormGroup className="mb-0">
      <Button.Yes type="submit" className="mr-2" />
      <Button.No onClick={handleCancel} />
    </FormGroup>
  </Form>
);

const enhance = compose<any, any>(reduxForm<any, { handleCancel: () => any }, string>({ form: DELETE_STRIPE_PAYMENTMETHOD_FORM }));

export default enhance(DeleteStripePaymentMethodForm);
