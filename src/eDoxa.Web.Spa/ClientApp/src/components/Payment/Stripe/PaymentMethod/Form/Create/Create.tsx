import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm, FormErrors, InjectedFormProps } from "redux-form";
import {
  injectStripe,
  CardNumberElement,
  CardExpiryElement,
  CardCvcElement,
  ReactStripeElements
} from "react-stripe-elements";
import Button from "components/Shared/Button";
import { CREATE_STRIPE_PAYMENTMETHOD_FORM } from "forms";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";
import { attachStripePaymentMethod } from "store/actions/payment";
import {
  StripePaymentMethodsActions,
  ATTACH_STRIPE_PAYMENTMETHOD_FAIL
} from "store/actions/payment/types";
import { throwSubmissionError } from "utils/form/types";

interface FormData {}

interface OutterProps {
  handleCancel: () => void;
}

type InnerProps = InjectedFormProps<FormData, Props> &
  ReactStripeElements.InjectedStripeProps;

type Props = InnerProps & OutterProps;

const CreateStripePaymentMethodForm: FunctionComponent<Props> = ({
  handleSubmit,
  error,
  handleCancel
}) => (
  <Form onSubmit={handleSubmit}>
    {error && <FormValidation error={error} />}
    <label>
      Card number
      <CardNumberElement />
    </label>
    <label>
      Expiration date
      <CardExpiryElement />
    </label>
    <label>
      CVC
      <CardCvcElement />
    </label>
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={() => handleCancel()} />
    </FormGroup>
  </Form>
);

const enhance = compose<InnerProps, OutterProps>(
  injectStripe,
  reduxForm<FormData, Props>({
    form: CREATE_STRIPE_PAYMENTMETHOD_FORM,
    onSubmit: async (values, dispatch: any, { stripe }) =>
      stripe.createPaymentMethod("card").then(result => {
        if (result.paymentMethod) {
          return dispatch(attachStripePaymentMethod(result.paymentMethod)).then(
            (action: StripePaymentMethodsActions) => {
              switch (action.type) {
                case ATTACH_STRIPE_PAYMENTMETHOD_FAIL: {
                  throwSubmissionError(action.error);
                  break;
                }
              }
            }
          );
        } else {
          return Promise.reject(result.error);
        }
      }),
    onSubmitSuccess: (result, dispatch, { handleCancel }) => handleCancel(),
    validate: () => {
      const errors: FormErrors<FormData> = {};
      return errors;
    }
  })
);

export default enhance(CreateStripePaymentMethodForm);
