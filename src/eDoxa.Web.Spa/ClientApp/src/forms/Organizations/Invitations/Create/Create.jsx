import React from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm, Field } from "redux-form";
import Input from "components/Shared/Override/Input";
import Button from "components/Shared/Override/Button";
import { CREATE_INVITATION_FORM } from "forms";

const CreateInvitationForm = ({ handleSubmit, initialValues: { clanId } }) => (
  <Form onSubmit={handleSubmit} className="mt-3">
    <FormGroup className="mb-0">
      <Field type="text" name="userId" label="User Id" formGroup={FormGroup} component={Input.Text} />
      <Field type="hidden" name="clanId" value={clanId} formGroup={FormGroup} component={Input.Text} />
      <Button.Submit width="150px" color="info">
        Send
      </Button.Submit>
    </FormGroup>
  </Form>
);

export default reduxForm({ form: CREATE_INVITATION_FORM })(CreateInvitationForm);
