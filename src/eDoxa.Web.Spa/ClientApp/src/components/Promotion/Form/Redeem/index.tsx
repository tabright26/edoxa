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
import { redeemPromotion } from "store/actions/cashier";
import { toastr } from "react-redux-toastr";
import { PromotionOptions } from "types";
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

const Redeem: FunctionComponent<Props> = ({
  handleSubmit,
  error,
  submitting,
  anyTouched
}) => (
  <Form onSubmit={handleSubmit}>
    <ValidationSummary anyTouched={anyTouched} error={error} />
    <Field
      type="text"
      name="promotionalCode"
      placeholder="Enter a promotional code"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <FormGroup className="mb-0">
      <Button.Submit loading={submitting} size="sm">
        Redeem
      </Button.Submit>
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
    onSubmitSuccess: (_result, _dispatch, { reset }) => {
      reset();
      toastr.success(
        "Promotional code",
        "You have successfully redeemed your promotional code"
      );
    },
    validate: () => {
      const errors: FormErrors<FormData> = {};
      return errors;
    }
  })
);

export default enhance(Redeem);
