import React from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm } from "redux-form";
import Button from "components/Shared/Override/Button";
import Input from "components/Shared/Override/Input";
import { CREATE_CANDIDATURE_FORM } from "forms";

const CreateCandidatureForm = ({ handleSubmit, initialValues: { userId, clanId } }) => (
  <Form onSubmit={handleSubmit} className="mt-3">
    <FormGroup className="mb-0">
      <Input.Text type="hidden" value={userId} name="userId" disabled />
      <Input.Text type="hidden" value={clanId} name="clanId" disabled />
      <Button.Submit width="150px" color="info">
        Click to send your candidature.
      </Button.Submit>
    </FormGroup>
  </Form>
);

export default reduxForm({ form: CREATE_CANDIDATURE_FORM })(CreateCandidatureForm);
