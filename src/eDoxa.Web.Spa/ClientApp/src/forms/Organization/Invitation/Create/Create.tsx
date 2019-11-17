import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm, Field } from "redux-form";
import Input from "components/Shared/Input";
import Button from "components/Shared/Button";
import { SEND_INVITATION_FORM } from "forms";
import { compose } from "recompose";
import { validate } from "./validate";
import FormValidation from "components/Shared/Form/Validation";

const CreateInvitationForm: FunctionComponent<any> = ({ handleSubmit, initialValues: { clanId }, error }) => (
  <Form onSubmit={handleSubmit}>
    {error && <FormValidation error={error} />}
    <Field type="text" name="userId" label="Doxatag" formGroup={FormGroup} component={Input.Text} />
    <Field type="hidden" name="clanId" value={clanId} formGroup={FormGroup} component={Input.Text} />
    <FormGroup className="mb-0">
      <Button.Save />
    </FormGroup>
  </Form>
);

const enhance = compose<any, any>(reduxForm<any, { handleCancel: () => any }, string>({ form: SEND_INVITATION_FORM, validate }));

export default enhance(CreateInvitationForm);
