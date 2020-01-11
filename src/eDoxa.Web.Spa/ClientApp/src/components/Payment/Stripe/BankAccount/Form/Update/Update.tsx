import React, { FunctionComponent } from "react";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { Field, reduxForm, FormErrors, InjectedFormProps } from "redux-form";
import { FormGroup, Form } from "reactstrap";
import { UPDATE_STRIPE_BANKACCOUNT_FORM } from "utils/form/constants";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";
import { updateStripeBankAccount } from "store/actions/payment";
import { injectStripe } from "react-stripe-elements";
import {
  StripeBankAccountActions,
  UPDATE_STRIPE_BANKACCOUNT_FAIL
} from "store/actions/payment/types";
import { throwSubmissionError } from "utils/form/types";

interface FormData {
  currency: string;
  accountHolderName: string;
  routingNumber: string;
  accountNumber: string;
}

interface OutterProps {
  country: string;
  handleCancel: () => void;
}

type InnerProps = InjectedFormProps<FormData, Props> & {
  stripe: stripe.Stripe;
};

type Props = InnerProps & OutterProps;

const CustomForm: FunctionComponent<Props> = ({
  handleSubmit,
  error,
  handleCancel
}) => (
  <Form onSubmit={handleSubmit}>
    {error && <FormValidation error={error} />}
    <Field
      type="text"
      name="currency"
      label="Currency"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <Field
      type="text"
      name="accountHolderName"
      label="Account Holder Name"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <Field
      type="text"
      name="routingNumber"
      label="Routing Number"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <Field
      type="text"
      name="accountNumber"
      label="Account Number"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={() => handleCancel()} />
    </FormGroup>
  </Form>
);

const enhance = compose<InnerProps, OutterProps>(
  injectStripe,
  reduxForm<FormData, Props>({
    form: UPDATE_STRIPE_BANKACCOUNT_FORM,
    onSubmit: async (values, dispatch: any, { country, stripe }) => {
      const options: stripe.BankAccountTokenOptions = {
        country,
        account_holder_type: "individual",
        account_holder_name: values.accountHolderName,
        routing_number: values.routingNumber,
        account_number: values.accountNumber,
        currency: values.currency
      };
      return await stripe.createToken("bank_account", options).then(result => {
        if (result.token) {
          return dispatch(updateStripeBankAccount(result.token)).then(
            (action: StripeBankAccountActions) => {
              switch (action.type) {
                case UPDATE_STRIPE_BANKACCOUNT_FAIL: {
                  throwSubmissionError(action.error);
                  break;
                }
              }
            }
          );
        } else {
          return Promise.reject(result.error);
        }
      });
    },
    onSubmitSuccess: (result, dispatch, { handleCancel }) => handleCancel(),
    validate: () => {
      const errors: FormErrors<FormData> = {};
      return errors;
    }
  })
);

export default enhance(CustomForm);
