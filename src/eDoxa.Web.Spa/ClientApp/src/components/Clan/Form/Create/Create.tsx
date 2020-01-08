import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { Field, reduxForm } from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { CREATE_CLAN_FORM } from "forms";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";

const validate = values => {
  var nameRegExp = new RegExp("^[a-zA-Z0-9- .,]{3,20}$");
  const errors: any = {};
  if (!values.name) {
    errors.name = "Name is required";
  } else if (!nameRegExp.test(values.name)) {
    errors.name =
      "Invalid format. Must between 3-20 characters and alphanumeric. Hyphens, spaces, dot and coma allowed.";
  }
  return errors;
};

const CustomForm: FunctionComponent<any> = ({
  handleSubmit,
  handleCancel,
  error
}) => (
  <Form onSubmit={handleSubmit}>
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

const enhance = compose<any, any>(
  reduxForm<any, { handleCancel: () => any }, string>({
    form: CREATE_CLAN_FORM,
    validate
  })
);

export default enhance(CustomForm);
