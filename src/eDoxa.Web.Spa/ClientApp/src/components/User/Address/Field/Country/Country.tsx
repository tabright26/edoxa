import React, { FunctionComponent, SyntheticEvent } from "react";
import { Field } from "redux-form";
import Input from "components/Shared/Input";
import { compose } from "recompose";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { CountryOptions } from "types";
import { FormGroup, Label } from "reactstrap";

interface OwnProps {
  label?: string;
  placeholder: string;
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
  onChange = null
}) => (
  <FormGroup>
    {label && <Label>{label}</Label>}
    <Field
      name="country"
      type="select"
      placeholder={placeholder}
      component={Input.Select}
      disabled={disabled}
      onChange={onChange}
    >
      {countries.map((country, index) => (
        <option key={index} value={country.twoIso}>
          {country.name}
        </option>
      ))}
    </Field>
  </FormGroup>
);

const mapStateToProps: MapStateToProps<
  StateProps,
  OwnProps,
  RootState
> = state => {
  return {
    countries: state.static.identity.data.addressBook.countries
  };
};

const enhance = compose<InnerProps, OutterProps>(connect(mapStateToProps));

export default enhance(CountryField);
