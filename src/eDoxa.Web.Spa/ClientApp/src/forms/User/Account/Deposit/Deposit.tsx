import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm } from "redux-form";
import Button from "components/Shared/Button";
import { USER_ACCOUNT_DEPOSIT_FORM } from "forms";
import FormField from "components/Shared/Form/Field";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";
import { throwSubmissionError } from "utils/form/types";
import { accountDeposit } from "store/root/user/account/deposit/actions";

async function submit(values, currency, dispatch) {
  try {
    return await new Promise((resolve, reject) => {
      const meta: any = { resolve, reject };
      dispatch(accountDeposit(currency, values.amount, meta));
    });
  } catch (error) {
    throwSubmissionError(error);
  }
}

const DepositForm: FunctionComponent<any> = ({
  bundles,
  dispatch,
  currency,
  handleSubmit,
  handleCancel,
  error
}) => (
  <Form
    onSubmit={handleSubmit(data =>
      submit(data, currency, dispatch).then(() => {
        handleCancel();
      })
    )}
  >
    {error && <FormValidation error={error} />}
    <FormField.Bundles bundles={bundles} currency={currency} />
    <hr className="border-secondary" />
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

const enhance = compose<any, any>(
  reduxForm<any, { handleCancel: () => any }, string>({
    form: USER_ACCOUNT_DEPOSIT_FORM
  })
);

export default enhance(DepositForm);
