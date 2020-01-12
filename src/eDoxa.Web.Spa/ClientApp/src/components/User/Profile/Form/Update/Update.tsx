import React, { FunctionComponent } from "react";
import { Form, FormGroup } from "reactstrap";
import {
  Field,
  reduxForm,
  FormSection,
  InjectedFormProps,
  FormErrors
} from "redux-form";
import Input from "components/Shared/Input";
import Button from "components/Shared/Button";
import { UPDATE_USER_PROFILE_FORM } from "utils/form/constants";
import { compose } from "recompose";
import FormField from "components/Shared/Form/Field";
import FormValidation from "components/Shared/Form/Validation";
import { updateUserProfile } from "store/actions/identity";
import { throwSubmissionError } from "utils/form/types";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import {
  PERSONALINFO_FIRSTNAME_REQUIRED,
  PERSONALINFO_FIRSTNAME_INVALID,
  personalInfoNameRegex
} from "validation";
import { AxiosActionCreatorMeta } from "utils/axios/types";

interface StateProps {}

interface FormData {
  firstName: string;
}

interface OutterProps {
  handleCancel: () => void;
}

type InnerProps = InjectedFormProps<FormData, Props> & StateProps;

type Props = InnerProps & OutterProps;

const CustomForm: FunctionComponent<Props> = ({
  handleSubmit,
  handleCancel,
  error
}) => (
  <Form onSubmit={handleSubmit}>
    {error && <FormValidation error={error} />}
    <dl className="row mb-0">
      <dd className="col-sm-3 text-muted mb-0">Name</dd>
      <dd className="col-sm-9 mb-0">
        <dl className="row">
          <dt className="col-sm-4 mb-0">
            <Field name="firstName" label="First Name" component={Input.Text} />
          </dt>
          <dd className="col-sm-4 mb-0">
            <Field
              name="lastName"
              label="Last Name"
              component={Input.Text}
              disabled
            />
          </dd>
        </dl>
      </dd>
      <dd className="col-sm-3 text-muted mb-0">Date of birth</dd>
      <dd className="col-sm-9 mb-0">
        <FormGroup>
          <FormSection name="dob">
            <FormField.Year className="d-inline" width="75px" disabled />
            <span className="d-inline mx-2">/</span>
            <FormField.Month className="d-inline" width="60px" disabled />
            <span className="d-inline mx-2">/</span>
            <FormField.Day className="d-inline" width="60px" disabled />
          </FormSection>
        </FormGroup>
      </dd>
      <dd className="col-sm-3 mb-0 text-muted">Gender</dd>
      <dd className="col-sm-3 mb-0">
        <Field name="gender" label="Gender" component={Input.Text} disabled />
      </dd>
      <dd className="col-sm-9 mb-0">
        <Button.Save className="mt-3 mr-2" />
        <Button.Cancel className="mt-3" onClick={() => handleCancel()} />
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
        errors.firstName = PERSONALINFO_FIRSTNAME_REQUIRED;
      } else if (!personalInfoNameRegex.test(values.firstName)) {
        errors.firstName = PERSONALINFO_FIRSTNAME_INVALID;
      }
      return errors;
    }
  })
);

export default enhance(CustomForm);
