import React, { FunctionComponent } from "react";
import { Label, FormGroup, Form } from "reactstrap";
import { reduxForm, InjectedFormProps } from "redux-form";
import Button from "components/Shared/Button";
import { DELETE_USER_ADDRESS_FORM } from "forms";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";
import { deleteUserAddress } from "store/actions/identity";
import { throwSubmissionError } from "utils/form/types";
import { AxiosActionCreatorMeta } from "utils/axios/types";
import { AddressId } from "types";

interface FormData {}

interface OutterProps {
  addressId: AddressId;
  handleCancel: () => void;
}

type InnerProps = InjectedFormProps<FormData, Props>;

type Props = InnerProps & OutterProps;

const ReduxForm: FunctionComponent<Props> = ({
  handleSubmit,
  error,
  handleCancel
}) => (
  <Form onSubmit={handleSubmit}>
    {error && <FormValidation error={error} />}
    <Label>Are you sure you want to remove this address?</Label>
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={() => handleCancel()} />
    </FormGroup>
  </Form>
);

const enhance = compose<InnerProps, OutterProps>(
  reduxForm<FormData, Props>({
    form: DELETE_USER_ADDRESS_FORM,
    onSubmit: async (_values, dispatch, { addressId }) => {
      try {
        return await new Promise((resolve, reject) => {
          const meta: AxiosActionCreatorMeta = { resolve, reject };
          dispatch(deleteUserAddress(addressId, meta));
        });
      } catch (error) {
        throwSubmissionError(error);
      }
    },
    onSubmitSuccess: (result, dispatch, { handleCancel }) => handleCancel()
  })
);

export default enhance(ReduxForm);
