import React, { FunctionComponent } from "react";
import { Label, FormGroup, Form } from "reactstrap";
import { reduxForm, InjectedFormProps } from "redux-form";
import Button from "components/Shared/Button";
import { DELETE_STRIPE_PAYMENTMETHOD_FORM } from "forms";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";
import { detachStripePaymentMethod } from "store/actions/payment";
import {
  StripePaymentMethodsActions,
  DETACH_STRIPE_PAYMENTMETHOD_FAIL
} from "store/actions/payment/types";
import { throwSubmissionError } from "utils/form/types";

interface FormData {}

interface OutterProps {
  paymentMethodId: string;
  handleCancel: () => void;
}

type InnerProps = InjectedFormProps<FormData, Props>;

type Props = InnerProps & OutterProps;

const ReduxForm: FunctionComponent<Props> = ({
  handleSubmit,
  error,
  handleCancel
}) => (
  <Form onSubmit={handleSubmit}>
    {error && <FormValidation error={error} />}
    <Label className="mb-3">
      Are you sure you want to delete this payment method?
    </Label>
    <FormGroup className="mb-0">
      <Button.Yes type="submit" className="mr-2" />
      <Button.No onClick={() => handleCancel()} />
    </FormGroup>
  </Form>
);

const enhance = compose<InnerProps, OutterProps>(
  reduxForm<FormData, Props>({
    form: DELETE_STRIPE_PAYMENTMETHOD_FORM,
    onSubmit: async (_values, dispatch: any, { paymentMethodId }) =>
      await dispatch(detachStripePaymentMethod(paymentMethodId)).then(
        (action: StripePaymentMethodsActions) => {
          switch (action.type) {
            case DETACH_STRIPE_PAYMENTMETHOD_FAIL: {
              throwSubmissionError(action.error);
              break;
            }
          }
        }
      ),
    onSubmitSuccess: (result, dispatch, { handleCancel }) => handleCancel()
  })
);

export default enhance(ReduxForm);
