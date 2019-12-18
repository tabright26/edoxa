import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { Field, reduxForm, InjectedFormProps } from "redux-form";
import Input from "components/Shared/Input";
import Button from "components/Shared/Button";
import { UPDATE_USER_PHONE_FORM } from "forms";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";
import { updateUserPhone } from "store/actions/identity";
import { throwSubmissionError } from "utils/form/types";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { AxiosActionCreatorMeta } from "utils/axios/types";
import { PHONE_REQUIRED, PHONE_INVALID, phoneRegex } from "validation";

interface Props {}

interface FormData {
  number: number;
}

interface StateProps {}

const validate = values => {
  const errors: any = {};
  if (!values.number) {
    errors.number = PHONE_REQUIRED;
  } else if (!phoneRegex.test(values.number)) {
    errors.number = PHONE_INVALID;
  }
  return errors;
};

async function submit(values, dispatch) {
  try {
    return await new Promise((resolve, reject) => {
      const meta: AxiosActionCreatorMeta = { resolve, reject };
      dispatch(updateUserPhone(values, meta));
    });
  } catch (error) {
    throwSubmissionError(error);
  }
}

const UpdateUserPhoneForm: FunctionComponent<InjectedFormProps<FormData> &
  Props &
  any> = ({ handleSubmit, handleCancel, dispatch, error }) => (
  <Form
    onSubmit={handleSubmit(data =>
      submit(data, dispatch).then(() => handleCancel())
    )}
  >
    {error && <FormValidation error={error} />}
    <Field
      type="text"
      name="number"
      label="Phone Number"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

const mapStateToProps: MapStateToProps<StateProps, Props, RootState> = (
  state: RootState
) => {
  const { data } = state.root.user.phone;
  return {
    initialValues: data
  };
};

const enhance = compose<any, any>(
  reduxForm<FormData, Props>({
    form: UPDATE_USER_PHONE_FORM,
    validate
  }),
  connect(mapStateToProps)
);

export default enhance(UpdateUserPhoneForm);
