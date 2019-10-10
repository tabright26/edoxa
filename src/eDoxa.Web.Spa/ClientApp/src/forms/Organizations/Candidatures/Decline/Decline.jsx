import React from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm } from "redux-form";
import Button from "components/Shared/Override/Button";
import Input from "components/Shared/Override/Input";

import { DECLINE_CANDIDATURE_FORM } from "forms";

const DeclineCandidatureForm = ({ handleSubmit, handleCancel, initialValues: { candidatureId } }) => (
  <Form onSubmit={handleSubmit} className="mt-3">
    <FormGroup className="mb-0">
      <Input.Text type="hidden" value={candidatureId} name="candidatureId" disabled />
      <Button.Submit width="50px" color="info">
        Decline
      </Button.Submit>
    </FormGroup>
  </Form>
);

export default reduxForm({ form: DECLINE_CANDIDATURE_FORM })(DeclineCandidatureForm);
