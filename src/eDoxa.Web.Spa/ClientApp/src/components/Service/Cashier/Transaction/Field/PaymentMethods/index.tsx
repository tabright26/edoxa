import React, { FunctionComponent, SyntheticEvent } from "react";
import { Field } from "redux-form";
import Input from "components/Shared/Input";
import { compose } from "recompose";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { Label } from "reactstrap";
import { StripePaymentMethod } from "types/payment";

interface OwnProps {
  label?: string;
  size?: string;
  disabled?: boolean;
  onChange?: (event: SyntheticEvent) => void;
}

interface StateProps {
  paymentMethods: StripePaymentMethod[];
}

type InnerProps = StateProps;

type OutterProps = OwnProps;

type Props = InnerProps & OutterProps;

const PaymentMethodsField: FunctionComponent<Props> = ({
  paymentMethods,
  disabled = false,
  label = null,
  size = null,
  onChange = null
}) => (
  <>
    {label && <Label>{label}</Label>}
    <Field
      name="paymentMethodId"
      type="select"
      size={size}
      component={Input.Select}
      disabled={disabled}
      onChange={onChange}
    >
      <option key={0} value={""}>
        Select a payment method
      </option>
      {paymentMethods.map((paymentMethod, index) => (
        <option key={index + 1} value={paymentMethod.id}>
          {`XXXX XXXX XXXX ${paymentMethod.card.last4}`}
        </option>
      ))}
    </Field>
  </>
);

const mapStateToProps: MapStateToProps<
  StateProps,
  OwnProps,
  RootState
> = state => {
  return {
    paymentMethods: state.root.payment.stripe.paymentMethods.data
  };
};

const enhance = compose<InnerProps, OutterProps>(connect(mapStateToProps));

export default enhance(PaymentMethodsField);
