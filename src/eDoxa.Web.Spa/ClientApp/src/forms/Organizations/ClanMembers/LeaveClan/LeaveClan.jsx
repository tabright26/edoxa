import React from "react";
import { Label, FormGroup, Form } from "reactstrap";
import { reduxForm } from "redux-form";
import Button from "components/Shared/Override/Button";
import { LEAVE_CLAN_FORM } from "forms";

import Input from "components/Shared/Override/Input";

const DeclineCandidatureForm = ({ handleSubmit, handleCancel, initialValues: { clanId } }) => (
  <Form onSubmit={handleSubmit} className="mt-3">
    <FormGroup className="mb-0">
      <Input.Text type="hidden" value={clanId} name="clanId" disabled />
      <Button.Submit width="50px" color="info">
        Leave Clan
      </Button.Submit>
    </FormGroup>
  </Form>
);

export default reduxForm({ form: LEAVE_CLAN_FORM })(DeclineCandidatureForm);
