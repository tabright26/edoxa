import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm } from "redux-form";
import Button from "components/Shared/Override/Button";
import { DEPOSIT_FORM } from "forms";
import { validate } from "./validate";
import Amounts from "../../../../components/Payment/Amounts";
import { compose } from "recompose";

const DepositForm: FunctionComponent<any> = ({ initialValues: { amounts }, handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <Amounts amounts={amounts} />
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

const enhance = compose<any, any>(reduxForm<any, { handleCancel: () => any }, string>({ form: DEPOSIT_FORM, validate }));

export default enhance(DepositForm);
