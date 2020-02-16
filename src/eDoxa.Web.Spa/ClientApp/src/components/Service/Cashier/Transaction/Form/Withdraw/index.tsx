import React, { FunctionComponent } from "react";
import { Form, ModalBody, ModalFooter, FormGroup } from "reactstrap";
import { reduxForm, InjectedFormProps, FormErrors, Field } from "redux-form";
import Button from "components/Shared/Button";
import { WITHDRAW_TRANSACTION_FORM } from "utils/form/constants";
import { compose } from "recompose";
import { ValidationSummary } from "components/Shared/ValidationSummary";
import { throwSubmissionError } from "utils/form/types";
import { withdrawTransaction } from "store/actions/cashier";
import {
  CurrencyType,
  TransactionBundleId,
  TRANSACTION_TYPE_WITHDRAWAL
} from "types/cashier";
import { AxiosActionCreatorMeta } from "utils/axios/types";
import TransactionField from "components/Service/Cashier/Transaction/Field";
import Input from "components/Shared/Input";

interface FormData {
  bundleId: TransactionBundleId;
  email: string;
}

interface OutterProps {
  currencyType: CurrencyType;
  handleCancel: () => void;
}

type InnerProps = InjectedFormProps<FormData, Props>;

type Props = InnerProps & OutterProps;

const Withdraw: FunctionComponent<Props> = ({
  handleSubmit,
  error,
  handleCancel,
  currencyType,
  submitting,
  anyTouched
}) => (
  <Form onSubmit={handleSubmit}>
    <ModalBody className="pb-0">
      <ValidationSummary anyTouched={anyTouched} error={error} />
      <TransactionField.Bundle
        name="bundleId"
        currencyType={currencyType}
        transactionType={TRANSACTION_TYPE_WITHDRAWAL}
      />
      <FormGroup>
        <dl className="row mb-0">
          <dd className="col-sm-3 mb-0 my-auto text-muted">PayPal email</dd>
          <dd className="col-sm-9 mb-0">
            <Field
              type="text"
              name="email"
              placeholder="Email"
              size="sm"
              autoComplete="email"
              component={Input.Text}
            />
          </dd>
        </dl>
      </FormGroup>
    </ModalBody>
    <ModalFooter className="bg-gray-800">
      <Button.Submit loading={submitting} className="mr-2">
        Confirm
      </Button.Submit>
      <Button.Cancel onClick={handleCancel} />
    </ModalFooter>
  </Form>
);

const enhance = compose<InnerProps, OutterProps>(
  reduxForm<FormData, Props>({
    form: WITHDRAW_TRANSACTION_FORM,
    onSubmit: async (values, dispatch) => {
      try {
        return await new Promise((resolve, reject) => {
          const meta: AxiosActionCreatorMeta = { resolve, reject };
          dispatch(withdrawTransaction(values.bundleId, values.email, meta));
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
      }
      return errors;
    }
  })
);

export default enhance(Withdraw);
