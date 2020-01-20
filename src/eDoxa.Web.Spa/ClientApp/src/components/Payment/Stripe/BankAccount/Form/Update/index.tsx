import React, { FunctionComponent } from "react";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { Field, reduxForm, FormErrors, InjectedFormProps } from "redux-form";
import { FormGroup, Form } from "reactstrap";
import { UPDATE_STRIPE_BANKACCOUNT_FORM } from "utils/form/constants";
import { compose } from "recompose";
import { ValidationSummary } from "components/Shared/ValidationSummary";
import { updateStripeBankAccount } from "store/actions/payment";
import { injectStripe } from "react-stripe-elements";
import {
  StripeBankAccountActions,
  UPDATE_STRIPE_BANKACCOUNT_FAIL
} from "store/actions/payment/types";
import { throwSubmissionError } from "utils/form/types";
import FormField from "components/Payment/Stripe/BankAccount/Field";
import {
  withUserProfileCountry,
  HocUserProfileCountryStateProps
} from "utils/oidc/containers";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";

interface FormData {
  currency: string;
  accountHolderName: string;
  routingNumber: string;
  accountNumber: string;
}

type StateProps = {
  initialValues: {
    currency: string;
  };
};

type OwnProps = HocUserProfileCountryStateProps;

type OutterProps = OwnProps & {
  handleCancel: () => void;
};

type InnerProps = InjectedFormProps<FormData, Props> &
  StateProps & {
    stripe: stripe.Stripe;
  };

type Props = InnerProps & OutterProps;

const Update: FunctionComponent<Props> = ({
  handleSubmit,
  error,
  handleCancel
}) => (
  <Form onSubmit={handleSubmit}>
    <ValidationSummary error={error} />
    <FormField.Currency />
    <Field
      type="text"
      name="accountHolderName"
      placeholder="Account Holder Name"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <Field
      type="text"
      name="routingNumber"
      placeholder="Routing Number"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <Field
      type="text"
      name="accountNumber"
      placeholder="Account Number"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={() => handleCancel()} />
    </FormGroup>
  </Form>
);

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (
  state,
  ownProps
) => {
  return {
    initialValues: {
      currency:
        state.static.payment.stripe.currencies[
          ownProps.country.toLowerCase()
        ][0]
    }
  };
};

const enhance = compose<InnerProps, OutterProps>(
  withUserProfileCountry,
  connect(mapStateToProps),
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

export default enhance(Update);
