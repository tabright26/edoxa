import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm } from "redux-form";
import Button from "components/Shared/Override/Button";
import { UPLOAD_CLAN_LOGO_FORM } from "forms";
import { compose } from "recompose";
import { validate } from "./validate";
import FormValidation from "components/Shared/Override/Form/Validation";

const UploadClanLogoForm: FunctionComponent<any> = ({ handleSubmit, handleCancel, error }) => (
  <Form onSubmit={handleSubmit}>
    {error && <FormValidation error={error} />}
    <FormGroup>
      <input type="file" name="Logo" accept="image/png, image/jpeg" />
    </FormGroup>
    <FormGroup className="mb-0">
      <Button.Save />
    </FormGroup>
  </Form>
);

const enhance = compose<any, any>(reduxForm<any, { handleCancel: () => any }, string>({ form: UPLOAD_CLAN_LOGO_FORM, validate }));

export default enhance(UploadClanLogoForm);
