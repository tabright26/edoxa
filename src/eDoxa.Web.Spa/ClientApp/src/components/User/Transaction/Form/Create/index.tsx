import React, { FunctionComponent } from "react";
import { Form, ModalBody, ModalFooter } from "reactstrap";
import { reduxForm, InjectedFormProps, FormErrors } from "redux-form";
import Button from "components/Shared/Button";
import { CREATE_USER_TRANSACTION_FORM } from "utils/form/constants";
import { compose } from "recompose";
import { ValidationSummary } from "components/Shared/ValidationSummary";
import { throwSubmissionError } from "utils/form/types";
import { createUserTransaction } from "store/actions/cashier";
import { Currency, TransactionType, TransactionBundleId } from "types";
import { AxiosActionCreatorMeta } from "utils/axios/types";
import FormField from "components/User/Transaction/Field";

interface FormData {
  bundleId: TransactionBundleId;
}

interface OutterProps {
  currency: Currency;
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
    <ModalBody>
      <ValidationSummary anyTouched={anyTouched} error={error} />
      <FormField.Bundle
        name="bundleId"
        transactionType={transactionType}
        currency={currency}
      />
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
          dispatch(createUserTransaction(values.bundleId, meta));
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
