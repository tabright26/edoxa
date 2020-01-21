import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { Field, reduxForm, InjectedFormProps } from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { REDEEM_PROMOTIONAL_CODE_FORM } from "utils/form/constants";
import { compose } from "recompose";
import { ValidationSummary } from "components/Shared/ValidationSummary";
import { throwSubmissionError } from "utils/form/types";
import { AxiosActionCreatorMeta } from "utils/axios/types";
import { redeemPromotionalCode } from "store/actions/cashier";

interface FormData {
  code: string;
}

type InnerProps = InjectedFormProps<FormData, Props>;

type OutterProps = {};

type Props = InnerProps & OutterProps;

const Redeem: FunctionComponent<Props> = ({ handleSubmit, error }) => (
  <Form onSubmit={handleSubmit}>
    <ValidationSummary error={error} />
    <Field
      type="text"
      name="code"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <FormGroup className="mb-0">
      <Button.Submit size="sm">Redeem</Button.Submit>
    </FormGroup>
  </Form>
);

const enhance = compose<InnerProps, OutterProps>(
  reduxForm<FormData, Props>({
    form: REDEEM_PROMOTIONAL_CODE_FORM,
    onSubmit: async (values, dispatch) => {
      try {
        return await new Promise((resolve, reject) => {
          const meta: AxiosActionCreatorMeta = { resolve, reject };
          dispatch(redeemPromotionalCode(values.code, meta));
        });
      } catch (error) {
        throwSubmissionError(error);
      }
    }
  })
);

export default enhance(Redeem);
