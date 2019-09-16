import "./stripeElements.css";
import React from "react";
import Button from "components/Shared/Override/Button";
import Input from "components/Shared/Override/Input";
import { Field, reduxForm } from "redux-form";
import { FormGroup, Form } from "reactstrap";
import { CREATE_BANK_FORM } from "forms";

/*handleSubmit = ev => {
    ev.preventDefault();

    var data = {};

    if (this.props.stripe) {
      this.props.stripe
        .createSource({
          type: "sepa_debit",
          currency: "usd",
          owner: {
            name: ev.target.name.value,
            email: ev.target.email.value
          },
          mandate: {
            notification_method: "email"
          }
        })
        .then(payload => {
          console.log(data);
          data = payload;
        });
    } else {
      console.log("Stripe.js hasn't loaded yet.");
    }

    console.log(data);

    if (data) {
      return data;
    }
  };*/

const CreateStripeBankForm = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <label>Bank details</label>
    <br />
    <Field type="text" name="country" label="Country" formGroup={FormGroup} component={Input.Text} />
    <br />
    <Field type="text" name="currency" label="Currency (usd or cad)" formGroup={FormGroup} component={Input.Text} />
    <br />
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: CREATE_BANK_FORM })(CreateStripeBankForm);
