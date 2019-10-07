import React from "react";
import { Label, FormGroup, Form } from "reactstrap";
import { reduxForm } from "redux-form";
import Button from "components/Shared/Override/Button";
import { DECLINE_INVITATION_FORM } from "forms";

const DeclineInvitationForm = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit} className="mt-3">
    <Label>Are you sure you want to decline this invitation?</Label>
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: DECLINE_INVITATION_FORM })(DeclineInvitationForm);
