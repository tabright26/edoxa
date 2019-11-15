import React, { FunctionComponent } from "react";
import { Form } from "reactstrap";
import { reduxForm } from "redux-form";
import Button from "components/Shared/Button";
import { VALIDATE_GAME_AUTHENTICATION_FORM } from "forms";
import { compose } from "recompose";

const ValidateGameAuthenticationForm: FunctionComponent<any> = ({
  handleSubmit,
  validateGameAuthentication
}) => (
  <Form onSubmit={handleSubmit(() => validateGameAuthentication())}>
    <Button.Submit>Validate</Button.Submit>
  </Form>
);

const enhance = compose<any, any>(
  reduxForm({ form: VALIDATE_GAME_AUTHENTICATION_FORM })
);

export default enhance(ValidateGameAuthenticationForm);
