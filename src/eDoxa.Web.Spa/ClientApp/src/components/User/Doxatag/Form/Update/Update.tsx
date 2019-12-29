import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm, Field, InjectedFormProps } from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { UPDATE_USER_DOXATAG_FORM } from "forms";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";
import { changeUserDoxatag } from "store/actions/identity";
import { throwSubmissionError } from "utils/form/types";
import {
  doxatagSpecialRegex,
  DOXATAG_MINIMUM_LENGTH,
  DOXATAG_MAXIMUM_LENGTH,
  DOXATAG_REQUIRED,
  DOXATAG_LENGTH_UNDER_INVALID,
  DOXATAG_LENGTH_OVER_INVALID,
  DOXATAG_INVALID
} from "validation";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { AxiosActionCreatorMeta } from "utils/axios/types";

interface Props {}

interface FormData {
  name: string;
}

interface StateProps {}

const validate = values => {
  const errors: any = {};
  if (!values.name) {
    errors.name = DOXATAG_REQUIRED;
  } else if (values.name.length < DOXATAG_MINIMUM_LENGTH) {
    errors.name = DOXATAG_LENGTH_UNDER_INVALID;
  } else if (values.name.length > DOXATAG_MAXIMUM_LENGTH) {
    errors.name = DOXATAG_LENGTH_OVER_INVALID;
  } else if (!doxatagSpecialRegex.test(values.name)) {
    errors.name = DOXATAG_INVALID;
  }
  return errors;
};

async function submit(values, dispatch) {
  try {
    return await new Promise(async (resolve, reject) => {
      const meta: AxiosActionCreatorMeta = { resolve, reject };
      await dispatch(changeUserDoxatag(values, meta));
    });
  } catch (error) {
    throwSubmissionError(error);
  }
}

const UpdateUserDoxatagForm: FunctionComponent<InjectedFormProps<FormData> &
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
      name="name"
      label="Name"
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
  const { data } = state.root.user.doxatagHistory;
  const doxatag =
    data
      .slice()
      .sort((left: any, right: any) =>
        left.timestamp < right.timestamp ? 1 : -1
      )[0] || null;
  return {
    initialValues: doxatag
  };
};

const enhance = compose<any, any>(
  connect(mapStateToProps),
  reduxForm<FormData, Props>({
    form: UPDATE_USER_DOXATAG_FORM,
    validate
  })
);

export default enhance(UpdateUserDoxatagForm);
