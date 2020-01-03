import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm, InjectedFormProps } from "redux-form";
import Button from "components/Shared/Button";
import { USER_ACCOUNT_WITHDRAWAL_FORM } from "forms";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";
import { throwSubmissionError } from "utils/form/types";
import { accountWithdrawal } from "store/actions/cashier";
import { connect, MapStateToProps } from "react-redux";
import { Currency, TransactionBundle } from "types";
import { RootState } from "store/types";
import { AxiosActionCreatorMeta } from "utils/axios/types";
import FormField from "components/Shared/Form/Field";

interface FormData {
  bundle: number;
}

interface StateProps {
  initialValues: {
    bundle: number;
  };
}

interface OutterProps {
  currency: Currency;
  bundles: TransactionBundle[];
  handleCancel: () => void;
}

type InnerProps = InjectedFormProps<FormData, Props> & StateProps;

type Props = InnerProps & OutterProps;

const ReduxForm: FunctionComponent<Props> = ({
  handleSubmit,
  error,
  handleCancel,
  bundles,
  currency
}) => (
  <Form onSubmit={handleSubmit}>
    {error && <FormValidation error={error} />}
    <FormField.Bundles bundles={bundles} currency={currency} />
    <hr className="border-secondary" />
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={() => handleCancel()} />
    </FormGroup>
  </Form>
);

const mapStateToProps: MapStateToProps<StateProps, Props, RootState> = (
  _state,
  ownProps
) => {
  return {
    initialValues: {
      bundle: ownProps.bundles[0].amount
    }
  };
};

const enhance = compose<InnerProps, OutterProps>(
  connect(mapStateToProps),
  reduxForm<FormData, Props>({
    form: USER_ACCOUNT_WITHDRAWAL_FORM,
    onSubmit: async (values, dispatch, { currency }) => {
      try {
        return await new Promise((resolve, reject) => {
          const meta: AxiosActionCreatorMeta = { resolve, reject };
          dispatch(accountWithdrawal(currency, values.bundle, meta));
        });
      } catch (error) {
        throwSubmissionError(error);
      }
    },
    onSubmitSuccess: (result, dispatch, { handleCancel }) => handleCancel()
  })
);

export default enhance(ReduxForm);
