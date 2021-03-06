import React, { FunctionComponent } from "react";
import { Label, FormGroup, Form, Button } from "reactstrap";
import { reduxForm, InjectedFormProps } from "redux-form";
import { DELETE_STRIPE_PAYMENTMETHOD_FORM } from "utils/form/constants";
import { compose } from "recompose";
import { ValidationSummary } from "components/Shared/ValidationSummary";
import { detachStripePaymentMethod } from "store/actions/payment";
import { DETACH_STRIPE_PAYMENTMETHOD_FAIL } from "store/actions/payment/types";
import { throwSubmissionError } from "utils/form/types";
import { RootActions } from "store/types";

type FormData = {};

type OutterProps = {
  paymentMethodId: string;
  handleCancel: () => void;
};

type InnerProps = InjectedFormProps<FormData, Props>;

type Props = InnerProps & OutterProps;

const Delete: FunctionComponent<Props> = ({
  handleSubmit,
  error,
  handleCancel,
  anyTouched
}) => (
  <Form onSubmit={handleSubmit}>
    <ValidationSummary anyTouched={anyTouched} error={error} />
    <Label className="mb-3">Do you want to delete your payment method?</Label>
    <FormGroup className="mb-0">
      <Button type="submit" size="sm" color="primary" className="mr-2">
        Yes
      </Button>
      <Button onClick={() => handleCancel()} size="sm">
        No
      </Button>
    </FormGroup>
  </Form>
);

const enhance = compose<InnerProps, OutterProps>(
  reduxForm<FormData, Props>({
    form: DELETE_STRIPE_PAYMENTMETHOD_FORM,
    onSubmit: async (_values, dispatch: any, { paymentMethodId }) =>
      await dispatch(detachStripePaymentMethod(paymentMethodId)).then(
        (action: RootActions) => {
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

export default enhance(Delete);
