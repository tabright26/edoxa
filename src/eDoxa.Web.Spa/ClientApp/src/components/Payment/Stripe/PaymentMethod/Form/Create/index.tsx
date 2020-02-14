import React, { FunctionComponent } from "react";
import { Form, FormGroup } from "reactstrap";
import { reduxForm, InjectedFormProps } from "redux-form";
import {
  injectStripe,
  ReactStripeElements,
  CardElement
} from "react-stripe-elements";
import Button from "components/Shared/Button";
import { CREATE_STRIPE_PAYMENTMETHOD_FORM } from "utils/form/constants";
import { compose } from "recompose";
import { ValidationSummary } from "components/Shared/ValidationSummary";
import { attachStripePaymentMethod } from "store/actions/payment";
import { throwSubmissionError } from "utils/form/types";
import { AxiosActionCreatorMeta } from "utils/axios/types";

const style = {
  base: {
    color: "#FFF",
    fontFamily: '"Helvetica Neue", Helvetica, sans-serif',
    fontSmoothing: "antialiased",
    fontSize: "16px",
    "::placeholder": {
      color: "#aab7c4"
    }
  },
  invalid: {
    color: "#ff6f00",
    iconColor: "#ff6f00"
  }
};

interface FormData {}

type OutterProps = {
  handleCancel: () => void;
};

type InnerProps = ReactStripeElements.InjectedStripeProps &
  InjectedFormProps<FormData, Props>;

type Props = InnerProps & OutterProps;

const Create: FunctionComponent<Props> = ({
  handleSubmit,
  error,
  handleCancel,
  submitting,
  anyTouched
}) => (
  <Form onSubmit={handleSubmit}>
    <ValidationSummary anyTouched={anyTouched} error={error} />
    <FormGroup>
      <CardElement style={style} />
    </FormGroup>
    <Button.Submit loading={submitting} className="mr-2" size="sm">
      Save
    </Button.Submit>
    <Button.Cancel size="sm" onClick={handleCancel} />
  </Form>
);

const enhance = compose<InnerProps, OutterProps>(
  injectStripe,
  reduxForm<FormData, Props>({
    form: CREATE_STRIPE_PAYMENTMETHOD_FORM,
    onSubmit: async (_values, dispatch: any, { stripe }) => {
      try {
        return await new Promise((resolve, reject) => {
          const meta: AxiosActionCreatorMeta = { resolve, reject };
          stripe.createPaymentMethod("card").then(result => {
            if (result.paymentMethod) {
              dispatch(
                attachStripePaymentMethod(result.paymentMethod, false, meta)
              );
            } else {
              reject(result.error);
            }
          });
        });
      } catch (error) {
        throwSubmissionError(error);
      }
    },
    onSubmitSuccess: (_result, _dispatch, { handleCancel }) => handleCancel()
  })
);

export default enhance(Create);
