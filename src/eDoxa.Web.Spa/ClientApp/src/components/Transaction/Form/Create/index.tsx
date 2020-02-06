import React, { FunctionComponent } from "react";
import { Form, ModalBody, ModalFooter, FormGroup } from "reactstrap";
import { reduxForm, InjectedFormProps, FormErrors, Field } from "redux-form";
import Button from "components/Shared/Button";
import { CREATE_USER_TRANSACTION_FORM } from "utils/form/constants";
import { compose } from "recompose";
import { ValidationSummary } from "components/Shared/ValidationSummary";
import { throwSubmissionError } from "utils/form/types";
import { createUserTransaction } from "store/actions/cashier";
import {
  CurrencyType,
  TransactionType,
  TransactionBundleId,
  TRANSACTION_TYPE_WITHDRAWAL
} from "types";
import { AxiosActionCreatorMeta } from "utils/axios/types";
import FormField from "components/Transaction/Field";
import Input from "components/Shared/Input";

interface FormData {
  bundleId: TransactionBundleId;
  email?: string;
}

interface OutterProps {
  currency: CurrencyType;
  transactionType: TransactionType;
  handleCancel: () => void;
}

type InnerProps = InjectedFormProps<FormData, Props>;

type Props = InnerProps & OutterProps;

const Create: FunctionComponent<Props> = ({
  handleSubmit,
  error,
  handleCancel,
  currency,
  transactionType,
  submitting,
  anyTouched
}) => (
  <Form onSubmit={handleSubmit}>
    <ModalBody className="pb-0">
      <ValidationSummary anyTouched={anyTouched} error={error} />
      <FormField.Bundle
        name="bundleId"
        transactionType={transactionType}
        currency={currency}
      />
      {transactionType === TRANSACTION_TYPE_WITHDRAWAL && (
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
      )}
    </ModalBody>
    <ModalFooter className="bg-gray-800">
      <Button.Submit size={null} loading={submitting} className="mr-2">
        Confirm
      </Button.Submit>
      <Button.Cancel size={null} onClick={handleCancel} />
    </ModalFooter>
  </Form>
);

const enhance = compose<InnerProps, OutterProps>(
  reduxForm<FormData, Props>({
    form: CREATE_USER_TRANSACTION_FORM,
    onSubmit: async (values, dispatch) => {
      try {
        return await new Promise((resolve, reject) => {
          const meta: AxiosActionCreatorMeta = { resolve, reject };
          dispatch(createUserTransaction(values.bundleId, values.email, meta));
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

export default enhance(Create);
