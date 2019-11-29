import React, { FunctionComponent } from "react";
import { Label, FormGroup, Form } from "reactstrap";
import { reduxForm } from "redux-form";
import Button from "components/Shared/Button";
import { DELETE_USER_ADDRESS_FORM } from "forms";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";
import { deleteUserAddress } from "store/root/user/addressBook/actions";
import { throwSubmissionError } from "utils/form/types";

async function submit(values, dispatch) {
  try {
    return await new Promise((resolve, reject) => {
      const meta: any = { resolve, reject };
      dispatch(deleteUserAddress(values, meta));
    });
  } catch (error) {
    throwSubmissionError(error);
  }
}

const DeleteUserAddressForm: FunctionComponent<any> = ({
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
    <Label>Are you sure you want to remove this address?</Label>
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

const enhance = compose<any, any>(
  reduxForm<any, { handleCancel: () => {} }, string>({
    form: DELETE_USER_ADDRESS_FORM
  })
);

export default enhance(DeleteUserAddressForm);
