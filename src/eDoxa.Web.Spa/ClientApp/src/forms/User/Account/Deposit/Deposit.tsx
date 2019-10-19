import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm } from "redux-form";
import Button from "components/Shared/Override/Button";
import { USER_ACCOUNT_DEPOSIT_FORM } from "forms";
import { validate } from "./validate";
import FormField from "components/Payment/Form/Field";
import { compose } from "recompose";
import FormValidation from "components/Shared/Override/Form/Validation";
import faker from "faker";

faker.seed(1000);

const DepositForm: FunctionComponent<any> = ({ bundles, accountDeposit, currency, handleSubmit, handleCancel, error }) => (
  <Form
    onSubmit={handleSubmit(data =>
      accountDeposit(data).then(() => {
        handleCancel();
      })
    )}
  >
    {error && <FormValidation error={error} />}
    <FormField.Bundles bundles={bundles} currency={currency} />
    <hr className="border-secondary" />
    {faker.lorem.lines(7)}
    <hr className="border-secondary" />
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

const enhance = compose<any, any>(reduxForm<any, { handleCancel: () => any }, string>({ form: USER_ACCOUNT_DEPOSIT_FORM, validate }));

export default enhance(DepositForm);
