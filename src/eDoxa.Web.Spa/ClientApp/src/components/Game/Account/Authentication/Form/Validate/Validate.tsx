import React, { FunctionComponent } from "react";
import { Form } from "reactstrap";
import { reduxForm } from "redux-form";
import Button from "components/Shared/Button";
import { LINK_GAME_CREDENTIAL_FORM } from "forms";
import { compose } from "recompose";

const ValidateGameAccountAuthenticationForm: FunctionComponent<any> = ({
  handleSubmit,
  linkGameCredential
}) => (
  <Form onSubmit={handleSubmit(() => linkGameCredential())}>
    <Button.Submit>Validate</Button.Submit>
  </Form>
);

const enhance = compose<any, any>(
  reduxForm({ form: LINK_GAME_CREDENTIAL_FORM })
);

export default enhance(ValidateGameAccountAuthenticationForm);
