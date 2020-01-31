import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm } from "redux-form";
import Button from "components/Shared/Button";
import { UPLOAD_CLAN_LOGO_FORM } from "utils/form/constants";
import { compose } from "recompose";
import { ValidationSummary } from "components/Shared/ValidationSummary";

const UploadClanLogoForm: FunctionComponent<any> = ({
  handleSubmit,
  error,
  submitting,
  anyTouched
}) => (
  <Form onSubmit={handleSubmit}>
    <ValidationSummary anyTouched={anyTouched} error={error} />
    <FormGroup>
      <input type="file" name="Logo" accept="image/png, image/jpeg" />
    </FormGroup>
    <FormGroup className="mb-0">
      <Button.Submit loading={submitting} size="sm">
        Save
      </Button.Submit>
    </FormGroup>
  </Form>
);

const enhance = compose<any, any>(
  reduxForm<any, { handleCancel: () => any }, string>({
    form: UPLOAD_CLAN_LOGO_FORM
  })
);

export default enhance(UploadClanLogoForm);
