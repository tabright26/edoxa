import React, { FunctionComponent } from "react";
import { Label, FormGroup, Form } from "reactstrap";
import { reduxForm } from "redux-form";
import Button from "components/Shared/Button";
import { UNLINK_GAME_CREDENTIAL_FORM } from "forms";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";

const UnlinkGameCredentialForm: FunctionComponent<any> = ({
  unlinkGameCredential,
  handleSubmit,
  handleCancel,
  error
}) => (
  <Form
    onSubmit={handleSubmit(() =>
      unlinkGameCredential().then(() => {
        handleCancel();
      })
    )}
    className="mt-3"
  >
    {error && <FormValidation error={error} />}
    <Label>Are you sure you want to unlink this game account?</Label>
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

const enhance = compose<any, any>(
  reduxForm<any, { handleCancel: () => {} }, string>({
    form: UNLINK_GAME_CREDENTIAL_FORM
  })
);

export default enhance(UnlinkGameCredentialForm);
