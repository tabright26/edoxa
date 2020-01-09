import React, { FunctionComponent } from "react";
import { Form, FormGroup } from "reactstrap";
import {
  Field,
  FormSection,
  reduxForm,
  InjectedFormProps,
  FormErrors
} from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { CREATE_USER_PROFILE_FORM } from "utils/form/constants";
import { compose } from "recompose";
import FormField from "components/Shared/Form/Field";
import FormValidation from "components/Shared/Form/Validation";
import { createUserProfile } from "store/actions/identity";
import { throwSubmissionError } from "utils/form/types";
import {
  personalInfoNameRegex,
  PERSONALINFO_FIRSTNAME_REQUIRED,
  PERSONALINFO_FIRSTNAME_INVALID,
  PERSONALINFO_LASTNAME_REQUIRED,
  PERSONALINFO_LASTNAME_INVALID,
  PERSONALINFO_GENDER_REQUIRED
} from "validation";
import { AxiosActionCreatorMeta } from "utils/axios/types";

interface FormData {
  firstName: string;
  lastName: string;
  gender: string;
  dob: {
    month: string;
    year: string;
    day: string;
  };
}

interface OutterProps {
  handleCancel: () => void;
}

type InnerProps = InjectedFormProps<FormData, Props>;

type Props = InnerProps & OutterProps;

const CustomForm: FunctionComponent<Props> = ({ handleSubmit, error }) => (
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
            <Field name="lastName" label="Last Name" component={Input.Text} />
          </dd>
        </dl>
      </dd>
      <dd className="col-sm-3 text-muted mb-0">Date of birth</dd>
      <dd className="col-sm-9 mb-0">
        <FormGroup>
          <FormSection name="dob">
            <FormField.Year className="d-inline" width="75px" />
            <span className="d-inline mx-2">/</span>
            <FormField.Month className="d-inline" width="60px" />
            <span className="d-inline mx-2">/</span>
            <FormField.Day className="d-inline" width="60px" />
          </FormSection>
        </FormGroup>
      </dd>
      <dd className="col-sm-3 text-muted mb-0">Gender</dd>
      <dd className="col-sm-3 mb-0">
        <FormGroup>
          <Field name="gender" type="select" component={Input.Select}>
            <option value="">Gender</option>
            <option value="Male">Male</option>
            <option value="Female">Female</option>
            <option value="Other">Other</option>
          </Field>
        </FormGroup>
      </dd>
      <dd className="col-sm-9 mb-0">
        <Button.Save />
      </dd>
    </dl>
  </Form>
);

const enhance = compose<InnerProps, OutterProps>(
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
    onSubmitSuccess: (result, dispatch, { handleCancel }) => handleCancel(),
    validate: values => {
      const errors: FormErrors<FormData> = {};
      if (!values.firstName) {
        errors.firstName = PERSONALINFO_FIRSTNAME_REQUIRED;
      } else if (!personalInfoNameRegex.test(values.firstName)) {
        errors.firstName = PERSONALINFO_FIRSTNAME_INVALID;
      }
      if (!values.lastName) {
        errors.lastName = PERSONALINFO_LASTNAME_REQUIRED;
      } else if (!personalInfoNameRegex.test(values.lastName)) {
        errors.lastName = PERSONALINFO_LASTNAME_INVALID;
      }
      if (!values.gender) {
        errors.gender = PERSONALINFO_GENDER_REQUIRED;
      }
      // if (!values.dob.year) {
      //   errors.dob = PERSONALINFO_YEAR_REQUIRED;
      // } else if (!personalInfoYearRegex.test(values.dob.year)) {
      //   errors.dob = PERSONALINFO_YEAR_INVALID;
      // }
      // if (!values.dob.month) {
      //   errors.dob = PERSONALINFO_MONTH_REQUIRED;
      // } else if (!personalInfoMonthRegex.test(values.dob.month)) {
      //   errors.dob = PERSONALINFO_MONTH_INVALID;
      // }
      // if (!values.dob.day) {
      //   errors.dob = PERSONALINFO_DAY_REQUIRED;
      // } else if (!personalInfoDayRegex.test(values.dob.day)) {
      //   errors.dob = PERSONALINFO_DAY_INVALID;
      // }
      return errors;
    }
  })
);

export default enhance(CustomForm);
