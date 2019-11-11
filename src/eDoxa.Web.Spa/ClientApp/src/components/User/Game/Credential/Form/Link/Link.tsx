import React, { FunctionComponent } from "react";
import { Form } from "reactstrap";
import { reduxForm } from "redux-form";
import Button from "components/Shared/Button";
import { LINK_GAME_CREDENTIAL_FORM } from "forms";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";

const LinkGameCredentialForm: FunctionComponent<any> = ({ handleSubmit, linkGameCredential, error }) => (
  <Form onSubmit={handleSubmit(() => linkGameCredential())}>
    {error && <FormValidation error={error} />}
    <Button.Save />
  </Form>
);

const enhance = compose<any, any>(reduxForm({ form: LINK_GAME_CREDENTIAL_FORM }));

export default enhance(LinkGameCredentialForm);
