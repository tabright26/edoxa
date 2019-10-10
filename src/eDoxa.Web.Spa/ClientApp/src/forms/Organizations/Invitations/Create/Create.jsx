import React from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm } from "redux-form";
import Input from "components/Shared/Override/Input";
import Button from "components/Shared/Override/Button";
import { CREATE_INVITATION_FORM } from "forms";

const CreateInvitationForm = ({ handleSubmit, initialValues: { clanId, doxaTags } }) => (
  <Form onSubmit={handleSubmit} className="mt-3">
    <FormGroup className="mb-0">
      <Input.Text type="hidden" value={doxaTags} name="userId" disabled />
      <Input.Text type="hidden" value={clanId} name="clanId" disabled />
      <Button.Submit width="150px" color="info">
        Send
      </Button.Submit>
    </FormGroup>
  </Form>
);

export default reduxForm({ form: CREATE_INVITATION_FORM })(CreateInvitationForm);
