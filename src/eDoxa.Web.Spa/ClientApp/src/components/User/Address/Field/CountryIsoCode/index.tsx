import React, { FunctionComponent, SyntheticEvent } from "react";
import { Field } from "redux-form";
import Input from "components/Shared/Input";
import { compose } from "recompose";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { CountryOptions } from "types";
import { Label } from "reactstrap";

interface OwnProps {
  label?: string;
  placeholder: string;
  size?: string;
  disabled?: boolean;
  onChange?: (event: SyntheticEvent) => void;
}

interface StateProps {
  countries: CountryOptions[];
}

type InnerProps = StateProps;

type OutterProps = OwnProps;

type Props = InnerProps & OutterProps;

const CountryField: FunctionComponent<Props> = ({
  countries,
  disabled = false,
  label = null,
  placeholder,
  size = null,
  onChange = null
}) => (
  <>
    {label && <Label>{label}</Label>}
    <Field
      name="countryIsoCode"
      type="select"
      size={size}
      placeholder={placeholder}
      component={Input.Select}
      disabled={disabled}
      onChange={onChange}
    >
      {countries.map((country, index) => (
        <option key={index} value={country.isoCode}>
          {country.name}
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
    countries: state.static.identity.countries
  };
};

const enhance = compose<InnerProps, OutterProps>(connect(mapStateToProps));

export default enhance(CountryField);
