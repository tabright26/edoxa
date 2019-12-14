import React, { FunctionComponent } from "react";
import { Form, FormGroup } from "reactstrap";
import { Field, FormSection, reduxForm, InjectedFormProps } from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { CREATE_USER_INFORMATIONS_FORM } from "forms";
import { compose } from "recompose";
import FormField from "components/Shared/Form/Field";
import FormValidation from "components/Shared/Form/Validation";
import { createUserInformations } from "store/actions/identity/actions";
import { throwSubmissionError } from "utils/form/types";
import {
  personalInfoNameRegex,
  personalInfoYearRegex,
  personalInfoMonthRegex,
  personalInfoDayRegex,
  PERSONALINFO_FIRSTNAME_REQUIRED,
  PERSONALINFO_FIRSTNAME_INVALID,
  PERSONALINFO_LASTNAME_REQUIRED,
  PERSONALINFO_LASTNAME_INVALID,
  PERSONALINFO_YEAR_REQUIRED,
  PERSONALINFO_YEAR_INVALID,
  PERSONALINFO_MONTH_REQUIRED,
  PERSONALINFO_MONTH_INVALID,
  PERSONALINFO_DAY_REQUIRED,
  PERSONALINFO_DAY_INVALID,
  PERSONALINFO_GENDER_REQUIRED
} from "validation";
import { AxiosActionCreatorMeta } from "utils/axios/types";

interface Props {}

interface FormData {
  firstName: string;
  lastName: string;
  year: number;
  month: number;
  day: number;
  gender: string;
}

const validate = values => {
  const errors: any = {};
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
  if (!values.year) {
    errors.year = PERSONALINFO_YEAR_REQUIRED;
  } else if (!personalInfoYearRegex.test(values.year)) {
    errors.year = PERSONALINFO_YEAR_INVALID;
  }
  if (!values.month) {
    errors.month = PERSONALINFO_MONTH_REQUIRED;
  } else if (!personalInfoMonthRegex.test(values.month)) {
    errors.month = PERSONALINFO_MONTH_INVALID;
  }
  if (!values.day) {
    errors.day = PERSONALINFO_DAY_REQUIRED;
  } else if (!personalInfoDayRegex.test(values.day)) {
    errors.day = PERSONALINFO_DAY_INVALID;
  }
  if (!values.gender) {
    errors.gender = PERSONALINFO_GENDER_REQUIRED;
  }
  return errors;
};

async function submit(values, dispatch) {
  try {
    return await new Promise((resolve, reject) => {
      const meta: AxiosActionCreatorMeta = { resolve, reject };
      dispatch(createUserInformations(values, meta));
    });
  } catch (error) {
    throwSubmissionError(error);
  }
}

const CreateUserInformationsForm: FunctionComponent<InjectedFormProps<
  FormData
> &
  Props &
  any> = ({ handleSubmit, handleCancel, dispatch, error }) => (
  <Form
    onSubmit={handleSubmit(data =>
      submit(data, dispatch).then(() => handleCancel())
    )}
  >
    {error && <FormValidation error={error} />}
    <dl className="row mb-0">
      <dd className="col-sm-3 text-muted mb-0">Name</dd>
      <dd className="col-sm-9 mb-0">
        <dl className="row">
          <dt className="col-sm-4 mb-0">
            <Field
              name="firstName"
              label="Enter your first name"
              component={Input.Text}
            />
          </dt>
          <dd className="col-sm-4 mb-0">
            <Field
              name="lastName"
              label="Enter your last name"
              component={Input.Text}
            />
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
            <option value="">Select your gender...</option>
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

const enhance = compose<any, any>(
  reduxForm<FormData, Props>({
    form: CREATE_USER_INFORMATIONS_FORM,
    validate
  })
);

export default enhance(CreateUserInformationsForm);
