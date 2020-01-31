import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm, Field } from "redux-form";
import Input from "components/Shared/Input";
import Button from "components/Shared/Button";
import { SEND_INVITATION_FORM } from "utils/form/constants";
import { compose } from "recompose";
import { ValidationSummary } from "components/Shared/ValidationSummary";

const Create: FunctionComponent<any> = ({
  handleSubmit,
  initialValues: { clanId },
  error,
  submitting,
  anyTouched
}) => (
  <Form onSubmit={handleSubmit}>
    <ValidationSummary anyTouched={anyTouched} error={error} />
    <Field
      type="text"
      name="userId"
      placeholder="DoxaTag"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <Field
      type="hidden"
      name="clanId"
      value={clanId}
      formGroup={FormGroup}
      component={Input.Text}
    />
    <FormGroup className="mb-0">
      <Button.Submit loading={submitting} size="sm">
        Save
      </Button.Submit>
    </FormGroup>
  </Form>
);

const enhance = compose<any, any>(
  reduxForm<any, { handleCancel: () => any }, string>({
    form: SEND_INVITATION_FORM
  })
);

export default enhance(Create);
