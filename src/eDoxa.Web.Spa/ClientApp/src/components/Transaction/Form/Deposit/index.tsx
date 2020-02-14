import React, { FunctionComponent } from "react";
import { Form, ModalBody, ModalFooter, FormGroup } from "reactstrap";
import { reduxForm, InjectedFormProps, FormErrors } from "redux-form";
import Button from "components/Shared/Button";
import { DEPOSIT_TRANSACTION_FORM } from "utils/form/constants";
import { compose } from "recompose";
import { ValidationSummary } from "components/Shared/ValidationSummary";
import { throwSubmissionError } from "utils/form/types";
import { depositTransaction } from "store/actions/cashier";
import {
  CurrencyType,
  TransactionBundleId,
  TRANSACTION_TYPE_DEPOSIT,
  StripePaymentMethodId
} from "types";
import { AxiosActionCreatorMeta } from "utils/axios/types";
import FormField from "components/Transaction/Field";
import { injectStripe, ReactStripeElements } from "react-stripe-elements";
import {
  DEPOSIT_TRANSACTION_SUCCESS,
  DEPOSIT_TRANSACTION_FAIL,
  DepositTransactionAction
} from "store/actions/cashier/types";
import TransactionField from "components/Transaction/Field";
import { RootState } from "store/types";
import { MapStateToProps, connect, MapDispatchToProps } from "react-redux";
import { getProfilePaymentMethodsPath } from "utils/coreui/constants";
import { push } from "connected-react-router";

type StateProps = {
  hasPaymentMethod: boolean;
};

type DispatchProps = { getProfilePaymentMethodsPath: () => void };

interface FormData {
  bundleId: TransactionBundleId;
  paymentMethodId: StripePaymentMethodId;
}

interface OutterProps {
  currencyType: CurrencyType;
  handleCancel: () => void;
}

type InnerProps = InjectedFormProps<FormData, Props> &
  ReactStripeElements.InjectedStripeProps &
  StateProps &
  DispatchProps;

type Props = InnerProps & OutterProps;

const Deposit: FunctionComponent<Props> = ({
  handleSubmit,
  error,
  handleCancel,
  currencyType,
  submitting,
  anyTouched,
  hasPaymentMethod,
  getProfilePaymentMethodsPath
}) => (
  <Form onSubmit={handleSubmit}>
    <ModalBody className="pb-0">
      <ValidationSummary anyTouched={anyTouched} error={error} />
      <FormField.Bundle
        name="bundleId"
        currencyType={currencyType}
        transactionType={TRANSACTION_TYPE_DEPOSIT}
      />
      <FormGroup>
        {hasPaymentMethod ? (
          <TransactionField.PaymentMethods />
        ) : (
          <Button.Link
            className="my-0"
            size="lg"
            onClick={() => {
              getProfilePaymentMethodsPath();
              handleCancel();
            }}
          >
            Add a credit card
          </Button.Link>
        )}
      </FormGroup>
    </ModalBody>
    <ModalFooter className="bg-gray-800">
      <Button.Submit
        loading={submitting}
        disabled={!hasPaymentMethod}
        className="mr-2"
      >
        Confirm
      </Button.Submit>
      <Button.Cancel onClick={handleCancel} />
    </ModalFooter>
  </Form>
);

const mapStateToProps: MapStateToProps<StateProps, any, RootState> = state => {
  return {
    hasPaymentMethod: !!state.root.payment.stripe.paymentMethods.data.length
  };
};

const mapDispatchToProps: MapDispatchToProps<DispatchProps, any> = dispatch => {
  return {
    getProfilePaymentMethodsPath: () =>
      dispatch(push(getProfilePaymentMethodsPath()))
  };
};

const enhance = compose<InnerProps, OutterProps>(
  connect(mapStateToProps, mapDispatchToProps),
  injectStripe,
  reduxForm<FormData, Props>({
    form: DEPOSIT_TRANSACTION_FORM,
    onSubmit: async (values, dispatch: any, { stripe }) => {
      try {
        return await new Promise((resolve, reject) => {
          const meta: AxiosActionCreatorMeta = { resolve, reject };
          dispatch(
            depositTransaction(values.bundleId, values.paymentMethodId, meta)
          ).then((action: DepositTransactionAction) => {
            if (action.type === DEPOSIT_TRANSACTION_SUCCESS) {
              stripe.confirmCardPayment(action.payload.data).then(result => {
                if (
                  result.paymentIntent &&
                  result.paymentIntent.status === "succeeded"
                ) {
                  resolve(action.payload);
                } else {
                  reject(result.error);
                }
              });
            }
            if (action.type === DEPOSIT_TRANSACTION_FAIL) {
              reject(action.error);
            }
          });
        });
      } catch (error) {
        throwSubmissionError(error);
      }
    },
    onSubmitSuccess: (_result, _dispatch, { handleCancel }) => handleCancel(),
    validate: values => {
      var errors: FormErrors<FormData> = {};
      if (!values.bundleId) {
        errors._error = "Select a bundle";
      } else if (!values.paymentMethodId) {
        errors._error = "Payment method is required";
      }
      return errors;
    }
  })
);

export default enhance(Deposit);
