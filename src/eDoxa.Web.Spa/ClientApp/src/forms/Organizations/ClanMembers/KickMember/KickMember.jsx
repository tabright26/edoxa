import React from "react";
import { Label, FormGroup, Form } from "reactstrap";
import { reduxForm } from "redux-form";
import Button from "components/Shared/Override/Button";
import { KICK_MEMBER_CLAN_FORM } from "forms";

import Input from "components/Shared/Override/Input";

const KickMemberForm = ({ handleSubmit, handleCancel, initialValues: { clanId, memberId } }) => (
  <Form onSubmit={handleSubmit}>
    <Input.Text type="text" value={clanId} name="clanId" disabled />
    <Input.Text type="text" value={memberId} name="memberId" disabled />
    <Button.Submit width="50px" color="info">
      Kick
    </Button.Submit>
  </Form>
);
export default reduxForm({ form: KICK_MEMBER_CLAN_FORM })(KickMemberForm);
