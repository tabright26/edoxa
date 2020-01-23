import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm, InjectedFormProps } from "redux-form";
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
  transactionBundleId: TransactionBundleId;
}

interface OutterProps {
  currency: Currency;
  transactionType: TransactionType;
  handleCancel: () => void;
}

type InnerProps = InjectedFormProps<FormData, Props>;

type Props = InnerProps & OutterProps;

const CustomForm: FunctionComponent<Props> = ({
  handleSubmit,
  error,
  handleCancel,
  currency,
  transactionType
}) => (
  <Form onSubmit={handleSubmit}>
    <ValidationSummary error={error} />
    <FormField.Bundle
      name="transactionBundleId"
      transactionType={transactionType}
      currency={currency}
    />
    <hr className="border-secondary" />
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={() => handleCancel()} />
    </FormGroup>
  </Form>
);

const enhance = compose<InnerProps, OutterProps>(
  reduxForm<FormData, Props>({
    form: CREATE_USER_TRANSACTION_FORM,
    onSubmit: async (values, dispatch) => {
      try {
        return await new Promise((resolve, reject) => {
          const meta: AxiosActionCreatorMeta = { resolve, reject };
          dispatch(createUserTransaction(values.transactionBundleId, meta));
        });
      } catch (error) {
        throwSubmissionError(error);
      }
    },
    onSubmitSuccess: (result, dispatch, { handleCancel }) => handleCancel()
  })
);

export default enhance(CustomForm);
