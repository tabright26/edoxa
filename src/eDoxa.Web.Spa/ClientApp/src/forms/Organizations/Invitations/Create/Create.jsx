import React from "react";
import { Label, FormGroup, Form } from "reactstrap";
import { reduxForm } from "redux-form";
import Button from "components/Shared/Override/Button";
import { CREATE_INVITATION_FORM } from "forms";

const CreateInvitationForm = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit} className="mt-3">
    <Label>Are you sure you want to this user to the clan?</Label>
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: CREATE_INVITATION_FORM })(CreateInvitationForm);
