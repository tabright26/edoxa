import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { Field, reduxForm, InjectedFormProps, FormErrors } from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { REDEEM_PROMOTION_FORM } from "utils/form/constants";
import { compose } from "recompose";
import { ValidationSummary } from "components/Shared/ValidationSummary";
import { throwSubmissionError } from "utils/form/types";
import { AxiosActionCreatorMeta } from "utils/axios/types";
import {
  redeemPromotion,
  loadUserTransactionHistory,
  loadUserBalance
} from "store/actions/cashier";
import { toastr } from "react-redux-toastr";
import { CURRENCY_MONEY, CURRENCY_TOKEN, PromotionOptions } from "types";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";

interface FormData {
  promotionalCode: string;
}

type OwnProps = {};

type StateProps = { promotionOptions: PromotionOptions };

type InnerProps = InjectedFormProps<FormData, Props> & StateProps;

type OutterProps = OwnProps;

type Props = InnerProps & OutterProps;

const Redeem: FunctionComponent<Props> = ({ handleSubmit, error }) => (
  <Form onSubmit={handleSubmit}>
    <ValidationSummary error={error} />
    <Field
      type="text"
      name="promotionalCode"
      placeholder="Enter a promotional code"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <FormGroup className="mb-0">
      <Button.Submit size="sm">Redeem</Button.Submit>
    </FormGroup>
  </Form>
);

const mapStateToProps: MapStateToProps<
  StateProps,
  OwnProps,
  RootState
> = state => {
  return {
    promotionOptions: state.static.cashier.promotion
  };
};

const enhance = compose<InnerProps, OutterProps>(
  connect(mapStateToProps),
  reduxForm<FormData, Props>({
    form: REDEEM_PROMOTION_FORM,
    onSubmit: async (values, dispatch) => {
      try {
        return await new Promise((resolve, reject) => {
          const meta: AxiosActionCreatorMeta = { resolve, reject };
          dispatch(redeemPromotion(values.promotionalCode, meta));
        });
      } catch (error) {
        throwSubmissionError(error);
      }
    },
    onSubmitSuccess: (_result, dispatch, { reset }) => {
      reset();
      Promise.all([
        dispatch(loadUserTransactionHistory()),
        dispatch(loadUserBalance(CURRENCY_MONEY)),
        dispatch(loadUserBalance(CURRENCY_TOKEN))
      ]).then(() =>
        toastr.success(
          "Promotional code",
          "You have successfully redeemed your promotional code"
        )
      );
    },
    validate: (values, { promotionOptions }) => {
      const errors: FormErrors<FormData> = {};
      // for (let [key, value] of Object.entries(promotionOptions)) {
      //   if (!promotionOptions[key].excluded) {
      //     errors[key] = getFieldValidationRuleMessage(value, values[key]);
      //   }
      // }
      return errors;
    }
  })
);

export default enhance(Redeem);
