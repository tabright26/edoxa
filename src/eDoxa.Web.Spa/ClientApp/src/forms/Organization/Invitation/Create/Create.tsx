import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm, Field } from "redux-form";
import Input from "components/Shared/Override/Input";
import Button from "components/Shared/Override/Button";
import { CREATE_INVITATION_FORM } from "forms";
import { compose } from "recompose";
import { validate } from "./validate";

const CreateInvitationForm: FunctionComponent<any> = ({ handleSubmit, initialValues: { clanId } }) => (
  <Form onSubmit={handleSubmit}>
    <Field type="text" name="userId" label="DoxaTag" formGroup={FormGroup} component={Input.Text} />
    <Field type="hidden" name="clanId" value={clanId} formGroup={FormGroup} component={Input.Text} />
    <FormGroup>
      <Button.Submit width="50px" color="info">
        Send
      </Button.Submit>
    </FormGroup>
  </Form>
);

const enhance = compose<any, any>(reduxForm<any, { handleCancel: () => any }, string>({ form: CREATE_INVITATION_FORM, validate }));

export default enhance(CreateInvitationForm);
