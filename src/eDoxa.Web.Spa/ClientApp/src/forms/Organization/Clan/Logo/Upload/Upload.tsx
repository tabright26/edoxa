import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm } from "redux-form";
import Button from "components/Shared/Override/Button";
import { UPLOAD_CLAN_LOGO_FORM } from "forms";
import { compose } from "recompose";
import { validate } from "./validate";

const UploadClanLogoForm: FunctionComponent<any> = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <FormGroup>
      <input type="file" name="Logo" accept="image/png, image/jpeg" />
      <Button.Submit width="100px" color="info">
        Update
      </Button.Submit>
    </FormGroup>
  </Form>
);

const enhance = compose<any, any>(reduxForm<any, { handleCancel: () => any }, string>({ form: UPLOAD_CLAN_LOGO_FORM, validate }));

export default enhance(UploadClanLogoForm);
