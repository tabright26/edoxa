import React from "react";
import { Field } from "redux-form";
import Input from "components/Shared/Override/Input";
import { countries } from "utils/localize/countries";

const CountryField = ({ disabled = false }) => (
  <Field name="country" type="select" component={Input.Select} disabled={disabled}>
    {countries.map((country, index) => (
      <option key={index} value={country.twoDigitIso}>
        {country.name}
      </option>
    ))}
  </Field>
);

export default CountryField;
