import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm } from "redux-form";
import Button from "components/Shared/Override/Button";
import { WITHDRAWAL_FORM } from "forms";
import validate from "./validate";
import Amounts from "../Amounts";

interface WithdrawalProps {
  initialValues: any;
  handleSubmit: any;
  handleCancel: any;
}

const WithdrawalForm: FunctionComponent<WithdrawalProps> = ({ initialValues: { amounts }, handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <Amounts amounts={amounts} />
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm<any, { handleCancel: () => any }, string>({ form: WITHDRAWAL_FORM, validate })(WithdrawalForm);
