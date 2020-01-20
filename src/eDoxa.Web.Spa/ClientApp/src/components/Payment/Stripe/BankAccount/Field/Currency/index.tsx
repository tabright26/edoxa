import React, { FunctionComponent, SyntheticEvent } from "react";
import { Field } from "redux-form";
import Input from "components/Shared/Input";
import { compose } from "recompose";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { FormGroup } from "reactstrap";
import {
  withUserProfileCountry,
  HocUserProfileCountryStateProps
} from "utils/oidc/containers";

type OwnProps = HocUserProfileCountryStateProps;

type StateProps = {
  currencies: string[];
};

type InnerProps = StateProps;

type OutterProps = {
  disabled?: boolean;
  onChange?: (event: SyntheticEvent) => void;
};

type Props = InnerProps & OutterProps;

const Currency: FunctionComponent<Props> = ({
  currencies,
  disabled = false,
  onChange = null
}) => (
  <FormGroup>
    <Field
      name="currency"
      type="select"
      component={Input.Select}
      disabled={disabled}
      onChange={onChange}
    >
      {currencies.map((currency, index) => (
        <option key={index} value={currency}>
          {currency}
        </option>
      ))}
    </Field>
  </FormGroup>
);

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (
  state,
  ownProps
) => {
  return {
    currencies:
      state.static.payment.stripe.currencies[ownProps.country.toLowerCase()]
  };
};

const enhance = compose<InnerProps, OutterProps>(
  withUserProfileCountry,
  connect(mapStateToProps)
);

export default enhance(Currency);
