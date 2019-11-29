import React, { FunctionComponent } from "react";
import { FormGroup, Col, Form } from "reactstrap";
import { Field, reduxForm } from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import FormField from "components/Shared/Form/Field";
import { CREATE_USER_ADDRESS_FORM } from "forms";
import { validate } from "./validate";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";
import { createUserAddress } from "store/root/user/addressBook/actions";
import { throwSubmissionError } from "utils/form/types";

async function submit(values, dispatch) {
  try {
    return await new Promise((resolve, reject) => {
      const meta: any = { resolve, reject };
      dispatch(createUserAddress(values, meta));
    });
  } catch (error) {
    throwSubmissionError(error);
  }
}

const CreateUserAddressForm: FunctionComponent<any> = ({
  handleSubmit,
  handleCancel,
  dispatch,
  error
}) => (
  <Form
    onSubmit={handleSubmit(data =>
      submit(data, dispatch).then(() => handleCancel())
    )}
  >
    {error && <FormValidation error={error} />}
    <FormGroup>
      <FormField.Country />
    </FormGroup>
    <Field
      type="text"
      name="line1"
      label="Address line 1"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <Field
      type="text"
      name="line2"
      label="Address line 2 (optional)"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <Field
      type="text"
      name="city"
      label="City"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <FormGroup row className="my-0">
      <Col xs="8">
        <Field
          type="text"
          name="state"
          label="State"
          formGroup={FormGroup}
          component={Input.Text}
        />
      </Col>
      <Col xs="4">
        <Field
          type="text"
          name="postalCode"
          label="Postal Code"
          formGroup={FormGroup}
          component={Input.Text}
        />
      </Col>
    </FormGroup>
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

const enhance = compose<any, any>(
  reduxForm<any, { handleCancel: () => {} }, string>({
    form: CREATE_USER_ADDRESS_FORM,
    validate
  })
);

export default enhance(CreateUserAddressForm);
