import React, { FunctionComponent } from "react";
import { Form } from "reactstrap";
import { reduxForm, InjectedFormProps } from "redux-form";
import {
  injectStripe,
  CardNumberElement,
  CardExpiryElement,
  CardCvcElement,
  ReactStripeElements
} from "react-stripe-elements";
import Button from "components/Shared/Button";
import { CREATE_STRIPE_PAYMENTMETHOD_FORM } from "utils/form/constants";
import { compose } from "recompose";
import { ValidationSummary } from "components/Shared/ValidationSummary";
import { attachStripePaymentMethod } from "store/actions/payment";
import {
  StripePaymentMethodsActions,
  ATTACH_STRIPE_PAYMENTMETHOD_FAIL
} from "store/actions/payment/types";
import { throwSubmissionError } from "utils/form/types";

interface FormData {}

type OutterProps = {
  handleCancel: () => void;
};

type InnerProps = ReactStripeElements.InjectedStripeProps &
  InjectedFormProps<FormData, Props>;

type Props = InnerProps & OutterProps;

const CreateStripePaymentMethodForm: FunctionComponent<Props> = ({
  handleSubmit,
  error,
  handleCancel
}) => (
  <Form onSubmit={handleSubmit}>
    <ValidationSummary error={error} />
    <dl className="row mb-0">
      <dt className="col-sm-4">Card number</dt>
      <dd className="col-sm-8">
        <CardNumberElement />
      </dd>
      <dt className="col-sm-4">Expiration date</dt>
      <dd className="col-sm-8">
        <CardExpiryElement />
      </dd>
      <dt className="col-sm-4">CVC</dt>
      <dd className="col-sm-8">
        <CardCvcElement />
      </dd>
      <dt className="col-sm-4 mb-0"></dt>
      <dd className="col-sm-8 mb-0">
        <Button.Save className="mr-2" />
        <Button.Cancel onClick={() => handleCancel()} />
      </dd>
    </dl>
  </Form>
);

const enhance = compose<InnerProps, OutterProps>(
  injectStripe,
  reduxForm<FormData, Props>({
    form: CREATE_STRIPE_PAYMENTMETHOD_FORM,
    onSubmit: (_values, dispatch: any, { stripe }) =>
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
    onSubmitSuccess: (_result, _dispatch, { handleCancel }) => handleCancel()
  })
);

export default enhance(CreateStripePaymentMethodForm);
