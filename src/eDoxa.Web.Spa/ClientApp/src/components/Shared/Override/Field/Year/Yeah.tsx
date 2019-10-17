import React, { FunctionComponent } from "react";
import { Field } from "redux-form";
import Input from "components/Shared/Override/Input";

const date = new Date(Date.now());

const fullYear = date.getFullYear();

const years = (max, min, descending) => {
  const years = new Array<number>();
  for (let year = descending ? max : min; descending ? year >= min : year <= max; descending ? year-- : year++) {
    years.push(year);
  }
  return years;
};

interface YearSelectFieldProps {
  name?: string;
  className?: string;
  width?: string;
  disabled?: boolean;
  max?: number;
  min?: number;
  descending?: boolean;
}

const YearSelectField: FunctionComponent<YearSelectFieldProps> = ({ className, name = "year", width, disabled = false, max = fullYear, min = fullYear - 100, descending = true }) => (
  <Field className={className} name={name} type="select" style={width && { width }} component={Input.Select} disabled={disabled}>
    {years(max, min, descending).map((year, index) => (
      <option key={index} value={year}>
        {year}
      </option>
    ))}
  </Field>
);

export default YearSelectField;
