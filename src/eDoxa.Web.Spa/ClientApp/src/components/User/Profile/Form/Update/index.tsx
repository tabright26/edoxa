import React, { FunctionComponent } from "react";
import { Form } from "reactstrap";
import { Field, reduxForm, InjectedFormProps, FormErrors } from "redux-form";
import Input from "components/Shared/Input";
import Button from "components/Shared/Button";
import { UPDATE_USER_PROFILE_FORM } from "utils/form/constants";
import { compose } from "recompose";
import { ValidationSummary } from "components/Shared/ValidationSummary";
import { updateUserProfile } from "store/actions/identity";
import { throwSubmissionError } from "utils/form/types";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import {
  PROFILE_FIRST_NAME_REQUIRED,
  PROFILE_FIRST_NAME_INVALID,
  PROFILE_FIRST_NAME_REGEXP
} from "utils/form/validators";
import { AxiosActionCreatorMeta } from "utils/axios/types";

interface FormData {
  firstName: string;
}

type StateProps = {};

type InnerProps = InjectedFormProps<FormData, Props> & StateProps;

type OutterProps = {
  handleCancel: () => void;
};

type Props = InnerProps & OutterProps;

const CustomForm: FunctionComponent<Props> = ({
  handleSubmit,
  handleCancel,
  error,
  submitting
}) => (
  <Form onSubmit={handleSubmit}>
    <ValidationSummary error={error} />
    <dl className="row mb-0">
      <dd className="col-sm-3 mb-0 text-muted">Name</dd>
      <dd className="col-sm-9 mb-0">
        <dl className="row">
          <dd className="col-sm-4 mb-0">
            <Field
              name="firstName"
              placeholder="First Name"
              component={Input.Text}
            />
          </dd>
          <dd className="col-sm-4 mb-0">
            <Field
              name="lastName"
              placeholder="Last Name"
              component={Input.Text}
              disabled
            />
          </dd>
        </dl>
      </dd>
      <dd className="col-sm-3 mb-0 text-muted">Gender</dd>
      <dd className="col-sm-6 mb-0">
        <Field
          name="gender"
          placeholder="Gender"
          component={Input.Text}
          disabled
        />
      </dd>
      <dd className="col-sm-3 mb-0"></dd>
      <dd className="col-sm-3 mb-0"></dd>
      <dd className="col-sm-9 mb-0">
        <Button.Submit loading={submitting} className="mt-3 mr-2" size="sm">
          Save
        </Button.Submit>
        <Button.Cancel onClick={handleCancel} className="mt-3" />
      </dd>
    </dl>
  </Form>
);

const mapStateToProps: MapStateToProps<
  StateProps,
  Props,
  RootState
> = state => {
  const { data } = state.root.user.profile;
  return {
    initialValues: data
  };
};

const enhance = compose<InnerProps, OutterProps>(
  connect(mapStateToProps),
  reduxForm<FormData, Props>({
    form: UPDATE_USER_PROFILE_FORM,
    onSubmit: async (values, dispatch) => {
      try {
        return await new Promise((resolve, reject) => {
          const meta: AxiosActionCreatorMeta = { resolve, reject };
          dispatch(updateUserProfile(values, meta));
        });
      } catch (error) {
        throwSubmissionError(error);
      }
    },
    onSubmitSuccess: (result, dispatch, { handleCancel }) => handleCancel(),
    validate: values => {
      const errors: FormErrors<FormData> = {};
      if (!values.firstName) {
        errors.firstName = PROFILE_FIRST_NAME_REQUIRED;
      } else if (!PROFILE_FIRST_NAME_REGEXP.test(values.firstName)) {
        errors.firstName = PROFILE_FIRST_NAME_INVALID;
      }
      return errors;
    }
  })
);

export default enhance(CustomForm);
