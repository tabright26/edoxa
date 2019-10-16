import React, { FunctionComponent } from "react";
import { Label, FormGroup, Form } from "reactstrap";
import { reduxForm } from "redux-form";
import Button from "components/Shared/Override/Button";
import { DELETE_USER_ADDRESS_FORM } from "forms";
import { compose } from "recompose";

const DeleteUserAddressForm: FunctionComponent<any> = ({ deleteUserAddress, handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit(() => deleteUserAddress().then(() => handleCancel()))} className="mt-3">
    <Label>Are you sure you want to remove this address?</Label>
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

const enhance = compose<any, any>(reduxForm<any, { handleCancel: () => {} }, string>({ form: DELETE_USER_ADDRESS_FORM }));

export default enhance(DeleteUserAddressForm);
