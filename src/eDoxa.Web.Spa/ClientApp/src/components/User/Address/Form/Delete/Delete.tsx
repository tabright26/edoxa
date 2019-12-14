import React, { FunctionComponent } from "react";
import { Label, FormGroup, Form } from "reactstrap";
import { reduxForm, InjectedFormProps } from "redux-form";
import Button from "components/Shared/Button";
import { DELETE_USER_ADDRESS_FORM } from "forms";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";
import { deleteUserAddress } from "store/actions/identity/actions";
import { throwSubmissionError } from "utils/form/types";
import { AxiosActionCreatorMeta } from "utils/axios/types";

interface Props {}

interface FormData {}

async function submit(values, dispatch) {
  try {
    return await new Promise((resolve, reject) => {
      const meta: AxiosActionCreatorMeta = { resolve, reject };
      dispatch(deleteUserAddress(values, meta));
    });
  } catch (error) {
    throwSubmissionError(error);
  }
}

const DeleteUserAddressForm: FunctionComponent<InjectedFormProps<FormData> &
  Props &
  any> = ({ handleSubmit, handleCancel, dispatch, error }) => (
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
  reduxForm<FormData, Props>({
    form: DELETE_USER_ADDRESS_FORM
  })
);

export default enhance(DeleteUserAddressForm);
