import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { Field, reduxForm, InjectedFormProps, FormErrors } from "redux-form";
import Input from "components/Shared/Input";
import Button from "components/Shared/Button";
import { UPDATE_USER_PHONE_FORM } from "utils/form/constants";
import { compose } from "recompose";
import { ValidationSummary } from "components/Shared/ValidationSummary";
import { updateUserPhone } from "store/actions/identity";
import { throwSubmissionError } from "utils/form/types";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { AxiosActionCreatorMeta } from "utils/axios/types";
import {
  PHONE_REQUIRED,
  PHONE_INVALID,
  PHONE_REGEXP
} from "utils/form/validators";

interface StateProps {}

interface FormData {
  number: number;
}
interface OutterProps {
  handleCancel: () => void;
}

type InnerProps = InjectedFormProps<FormData, Props> & StateProps;

type Props = InnerProps & OutterProps;

const CustomForm: FunctionComponent<Props> = ({
  handleSubmit,
  error,
  handleCancel,
  submitting
}) => (
  <Form onSubmit={handleSubmit}>
    <ValidationSummary error={error} />
    <Field
      type="text"
      name="number"
      placeholder="Phone Number"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <FormGroup className="mb-0">
      <Button.Submit loading={submitting} className="mr-2" size="sm">
        Save
      </Button.Submit>
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

const mapStateToProps: MapStateToProps<
  StateProps,
  Props,
  RootState
> = state => {
  const { data } = state.root.user.phone;
  return {
    initialValues: data
  };
};

const enhance = compose<InnerProps, OutterProps>(
  connect(mapStateToProps),
  reduxForm<FormData, Props>({
    form: UPDATE_USER_PHONE_FORM,
    onSubmit: async (values, dispatch) => {
      try {
        return await new Promise((resolve, reject) => {
          const meta: AxiosActionCreatorMeta = { resolve, reject };
          dispatch(updateUserPhone(values, meta));
        });
      } catch (error) {
        throwSubmissionError(error);
      }
    },
    onSubmitSuccess: (result, dispatch, { handleCancel }) => handleCancel(),
    validate: values => {
      const errors: FormErrors<FormData> = {};
      if (!values.number) {
        errors.number = PHONE_REQUIRED;
      } else if (!PHONE_REGEXP.test(values.number.toString())) {
        errors.number = PHONE_INVALID;
      }
      return errors;
    }
  })
);

export default enhance(CustomForm);
