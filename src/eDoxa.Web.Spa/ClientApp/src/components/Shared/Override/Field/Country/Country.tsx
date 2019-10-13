import React from "react";
import { Field } from "redux-form";
import Input from "components/Shared/Override/Input";
import { countries } from "components/Shared/Localization/countries";

const CountryField = () => (
  <Field name="country" type="select" component={Input.Select}>
    {countries.map(country => (
      <option value={country.twoDigitIso}>{country.name}</option>
    ))}
  </Field>
);

export default CountryField;
