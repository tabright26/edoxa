import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { Field, reduxForm } from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { CREATE_CLAN_FORM } from "utils/form/constants";
import { compose } from "recompose";
import { ValidationSummary } from "components/Shared/ValidationSummary";

const CustomForm: FunctionComponent<any> = ({
  handleSubmit,
  handleCancel,
  error,
  submitting
}) => (
  <Form onSubmit={handleSubmit}>
    <ValidationSummary error={error} />
    <Field
      type="text"
      name="name"
      placeholder="Name"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <FormGroup className="mb-0">
      <Button.Submit loading={submitting} size="sm" className="mr-2">
        Save
      </Button.Submit>
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

const enhance = compose<any, any>(
  reduxForm<any, { handleCancel: () => any }, string>({
    form: CREATE_CLAN_FORM,
    validate: values => {
      var nameRegExp = new RegExp("^[a-zA-Z0-9- .,]{3,20}$");
      const errors: any = {};
      if (!values.name) {
        errors.name = "Name is required";
      } else if (!nameRegExp.test(values.name)) {
        errors.name =
          "Invalid format. Must between 3-20 characters and alphanumeric. Hyphens, spaces, dot and coma allowed.";
      }
      return errors;
    }
  })
);

export default enhance(CustomForm);
