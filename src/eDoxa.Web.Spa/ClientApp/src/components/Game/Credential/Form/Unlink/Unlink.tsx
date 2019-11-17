import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm } from "redux-form";
import Button from "components/Shared/Button";
import { UNLINK_GAME_CREDENTIAL_FORM } from "forms";
import { compose } from "recompose";

const UnlinkGameCredentialForm: FunctionComponent<any> = ({
  unlinkGameCredential,
  handleSubmit,
  handleCancel
}) => (
  <Form
    onSubmit={handleSubmit(() =>
      unlinkGameCredential().then(() => {
        handleCancel();
      })
    )}
  >
    <FormGroup className="mb-0">
      <Button.Yes type="submit" className="mr-2" />
      <Button.No onClick={handleCancel} />
    </FormGroup>
  </Form>
);

const enhance = compose<any, any>(
  reduxForm<any, { handleCancel: () => {} }, string>({
    form: UNLINK_GAME_CREDENTIAL_FORM
  })
);

export default enhance(UnlinkGameCredentialForm);
