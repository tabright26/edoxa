import React, { FunctionComponent } from "react";
import { FormGroup, Form, Label } from "reactstrap";
import {
  reduxForm,
  FormSection,
  FormErrors,
  InjectedFormProps
} from "redux-form";
import Button from "components/Shared/Button";
import { UPDATE_STRIPE_PAYMENTMETHOD_FORM } from "utils/form/constants";
import CardIcon from "components/Payment/Stripe/PaymentMethod/Card/Icon";
import { compose } from "recompose";
import FormField from "components/Shared/Form/Field";
import FormValidation from "components/Shared/Form/Validation";
import { updateStripePaymentMethod } from "store/actions/payment";
import {
  StripePaymentMethodsActions,
  UPDATE_STRIPE_PAYMENTMETHOD_FAIL
} from "store/actions/payment/types";
import { throwSubmissionError } from "utils/form/types";
import { RootState } from "store/types";
import { connect, MapStateToProps } from "react-redux";
import { BrandProp } from "../../Card/Icon/types";

interface StateProps {}

interface FormData {
  card: {
    brand: BrandProp;
    last4: string;
    expYear: number;
    expMonth: number;
  };
}

interface OutterProps {
  paymentMethodId: string;
  handleCancel: () => void;
}

type InnerProps = InjectedFormProps<FormData, Props> & StateProps;

type Props = InnerProps & OutterProps;

const CustomForm: FunctionComponent<Props> = ({
  handleSubmit,
  initialValues: {
    card: { brand, last4, expYear }
  },
  error
}) => (
  <Form onSubmit={handleSubmit} inline className="d-flex">
    {error && <FormValidation error={error} />}
    <FormGroup>
      <div className="d-flex">
        <CardIcon className="my-auto" brand={brand} size="2x" />
        <span className="my-auto ml-2">{`XXXX XXXX XXXX ${last4}`}</span>
      </div>
    </FormGroup>
    <FormSection className="mx-auto" name="card">
      <Label className="ml-4 mr-2 text-muted">Expiration:</Label>
      <FormField.Month className="d-inline" name="expMonth" width="55px" />
      <span className="d-inline mx-2">/</span>
      <FormField.Year
        className="d-inline"
        name="expYear"
        width="55px"
        min={expYear}
        max={expYear + 20}
        descending={false}
      />
    </FormSection>
    <Button.Save />
  </Form>
);

const mapStateToProps: MapStateToProps<StateProps, Props, RootState> = (
  state,
  ownProps
) => {
  const { data } = state.root.payment.stripe.paymentMethods;
  const paymentMethod = data.find(
    paymentMethod => paymentMethod.id === ownProps.paymentMethodId
  );
  return {
    initialValues: paymentMethod
  };
};

const enhance = compose<InnerProps, OutterProps>(
  connect(mapStateToProps),
  reduxForm<FormData, Props>({
    form: UPDATE_STRIPE_PAYMENTMETHOD_FORM,
    onSubmit: async (values, dispatch: any, { paymentMethodId }) =>
      await dispatch(
        updateStripePaymentMethod(
          paymentMethodId,
          values.card.expMonth,
          values.card.expYear
        )
      ).then((action: StripePaymentMethodsActions) => {
        switch (action.type) {
          case UPDATE_STRIPE_PAYMENTMETHOD_FAIL: {
            throwSubmissionError(action.error);
            break;
          }
        }
      }),
    onSubmitSuccess: (result, dispatch, { handleCancel }) => handleCancel(),
    validate: values => {
      const errors: FormErrors<FormData> = {};
      // if (!values.card.expMonth) {
      //   errors.card = CC_MONTH_REQUIRED;
      // } else if (!ccMonthRegex.test(values.card.expMonth.toString())) {
      //   errors.card = CC_MONTH_INVALID;
      // }
      // if (!values.card.expYear) {
      //   errors.card = CC_YEAR_REQUIRED;
      // } else if (!ccYearRegex.test(values.card.expYear.toString())) {
      //   errors.card = CC_YEAR_INVALID;
      // }
      return errors;
    }
  })
);

export default enhance(CustomForm);
