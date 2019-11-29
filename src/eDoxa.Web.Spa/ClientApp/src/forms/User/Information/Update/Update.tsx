import React, { FunctionComponent } from "react";
import { Form, FormGroup } from "reactstrap";
import { Field, reduxForm, FormSection } from "redux-form";
import Input from "components/Shared/Input";
import Button from "components/Shared/Button";
import { UPDATE_USER_INFORMATIONS_FORM } from "forms";
import { validate } from "./validate";
import { compose } from "recompose";
import FormField from "components/Shared/Form/Field";
import FormValidation from "components/Shared/Form/Validation";
import { updateUserInformations } from "store/root/user/information/actions";
import { throwSubmissionError } from "utils/form/types";

async function submit(values, dispatch) {
  try {
    return await new Promise((resolve, reject) => {
      const meta: any = { resolve, reject };
      dispatch(updateUserInformations(values, meta));
    });
  } catch (error) {
    throwSubmissionError(error);
  }
}

const UpdateUserInformationsForm: FunctionComponent<any> = ({
  handleSubmit,
  handleCancel,
  dispatch,
  error
}) => (
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
              label="Enter your first name"
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
      <dd className="col-sm-6 mb-0">{""}</dd>
      <dd className="col-sm-3 mb-0">{""}</dd>
      <dd className="col-sm-9 mb-0">
        <Button.Save className="mt-3 mr-2" />
        <Button.Cancel className="mt-3" onClick={handleCancel} />
      </dd>
    </dl>
  </Form>
);

const enhance = compose<any, any>(
  reduxForm<any, { handleCancel: () => any }, string>({
    form: UPDATE_USER_INFORMATIONS_FORM,
    validate
  })
);

export default enhance(UpdateUserInformationsForm);
