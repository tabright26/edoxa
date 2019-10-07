import React from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm } from "redux-form";
import Button from "components/Shared/Override/Button";
import Input from "components/Shared/Override/Input";
import { UPDATE_CLAN_LOGO_FORM } from "forms";

const UpdateClanLogoForm = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit} className="mt-3">
    <FormGroup className="mb-0">
      <input type="file" name="Logo" accept="image/png, image/jpeg" />
      <Button.Submit width="50px" color="info">
        Update
      </Button.Submit>
    </FormGroup>
  </Form>
);

export default reduxForm({ form: UPDATE_CLAN_LOGO_FORM })(UpdateClanLogoForm);
