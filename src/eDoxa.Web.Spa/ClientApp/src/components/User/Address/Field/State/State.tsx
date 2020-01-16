import React, { FunctionComponent } from "react";
import { Field } from "redux-form";
import Input from "components/Shared/Input";
import { CountryRegionOptions } from "types";
import { FormGroup, Label } from "reactstrap";

interface Props {
  label?: string;
  placeholder: string;
  regions: CountryRegionOptions[];
}

const CountryField: FunctionComponent<Props> = ({
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

export default CountryField;
