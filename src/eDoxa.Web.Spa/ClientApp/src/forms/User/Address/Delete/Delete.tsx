import React, { FunctionComponent } from "react";
import { Label, FormGroup, Form } from "reactstrap";
import { reduxForm } from "redux-form";
import Button from "components/Shared/Button";
import { DELETE_USER_ADDRESS_FORM } from "forms";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";

const DeleteUserAddressForm: FunctionComponent<any> = ({ deleteUserAddress, handleSubmit, handleCancel, error }) => (
  <Form
    onSubmit={handleSubmit(() =>
      deleteUserAddress().then(() => {
        handleCancel();
      })
    )}
    className="mt-3"
  >
    {error && <FormValidation error={error} />}
    <Label>Are you sure you want to remove this address?</Label>
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

const enhance = compose<any, any>(reduxForm<any, { handleCancel: () => {} }, string>({ form: DELETE_USER_ADDRESS_FORM }));

export default enhance(DeleteUserAddressForm);
