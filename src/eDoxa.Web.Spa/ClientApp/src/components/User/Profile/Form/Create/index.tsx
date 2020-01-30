import React, { FunctionComponent } from "react";
import { Form, FormGroup } from "reactstrap";
import { Field, reduxForm, InjectedFormProps, FormErrors } from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { CREATE_USER_PROFILE_FORM } from "utils/form/constants";
import { compose } from "recompose";
import { ValidationSummary } from "components/Shared/ValidationSummary";
import { createUserProfile } from "store/actions/identity";
import { throwSubmissionError } from "utils/form/types";
import {
  PROFILE_FIRST_NAME_REGEXP,
  PROFILE_FIRST_NAME_REQUIRED,
  PROFILE_FIRST_NAME_INVALID,
  PROFILE_LAST_NAME_REQUIRED,
  PROFILE_LAST_NAME_INVALID,
  PROFILE_GENDER_REQUIRED
} from "utils/form/validators";
import { AxiosActionCreatorMeta } from "utils/axios/types";
import { connect, MapStateToProps } from "react-redux";
import { Gender, GENDER_MALE } from "types";
import { RootState } from "store/types";

interface FormData {
  firstName: string;
  lastName: string;
  gender: Gender;
}

type StateProps = {
  initialValues: {
    gender: Gender;
  };
};

type OwnProps = {};

type InnerProps = StateProps & InjectedFormProps<FormData, Props>;

type OutterProps = OwnProps & {
  handleCancel: () => void;
};

type Props = InnerProps & OutterProps;

const Create: FunctionComponent<Props> = ({
  handleSubmit,
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
            />
          </dd>
        </dl>
      </dd>
      <dd className="col-sm-3 mb-0 text-muted">Gender</dd>
      <dd className="col-sm-6 mb-0">
        <FormGroup>
          <Field name="gender" type="select" component={Input.Select}>
            <option value="Male">Male</option>
            <option value="Female">Female</option>
          </Field>
        </FormGroup>
      </dd>
      <dd className="col-sm-3 mb-0"></dd>
      <dd className="col-sm-3 mb-0"></dd>
      <dd className="col-sm-9 mb-0">
        <Button.Submit loading={submitting} className="mt-3" size="sm">
          Save
        </Button.Submit>
      </dd>
    </dl>
  </Form>
);

const mapStateToProps: MapStateToProps<
  StateProps,
  OwnProps,
  RootState
> = () => {
  return {
    initialValues: {
      gender: GENDER_MALE
    }
  };
};

const enhance = compose<InnerProps, OutterProps>(
  connect(mapStateToProps),
  reduxForm<FormData, Props>({
    form: CREATE_USER_PROFILE_FORM,
    onSubmit: async (values, dispatch) => {
      try {
        return await new Promise((resolve, reject) => {
          const meta: AxiosActionCreatorMeta = { resolve, reject };
          dispatch(createUserProfile(values, meta));
        });
      } catch (error) {
        throwSubmissionError(error);
      }
    },
    onSubmitSuccess: (_result, _dispatch, { handleCancel }) => handleCancel(),
    validate: values => {
      const errors: FormErrors<FormData> = {};
      if (!values.firstName) {
        errors.firstName = PROFILE_FIRST_NAME_REQUIRED;
      } else if (!PROFILE_FIRST_NAME_REGEXP.test(values.firstName)) {
        errors.firstName = PROFILE_FIRST_NAME_INVALID;
      }
      if (!values.lastName) {
        errors.lastName = PROFILE_LAST_NAME_REQUIRED;
      } else if (!PROFILE_FIRST_NAME_REGEXP.test(values.lastName)) {
        errors.lastName = PROFILE_LAST_NAME_INVALID;
      }
      if (!values.gender) {
        errors.gender = PROFILE_GENDER_REQUIRED;
      }

      return errors;
    }
  })
);

export default enhance(Create);
