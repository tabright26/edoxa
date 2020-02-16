import React, { FunctionComponent } from "react";
import { Field } from "redux-form";
import Input from "components/Shared/Input";
import { CountryRegionOptions } from "types";
import { FormGroup, Label } from "reactstrap";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { Country } from "types/identity";

type OwnProps = {
  label?: string;
  placeholder: string;
  countryIsoCode: Country;
};

type StateProps = {
  regions: CountryRegionOptions[];
};

type Props = OwnProps & StateProps;

const State: FunctionComponent<Props> = ({
  regions,
  label = null,
  placeholder
}) => (
  <FormGroup>
    {label && <Label>{label}</Label>}
    <Field
      name="state"
      type="select"
      placeholder={placeholder}
      component={Input.Select}
    >
      {regions.map((region, index) => (
        <option key={index} value={region.code}>
          {region.name}
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
    regions: state.static.identity.countries.find(
      country => country.isoCode === ownProps.countryIsoCode
    ).regions
  };
};

export default connect(mapStateToProps)(State);
