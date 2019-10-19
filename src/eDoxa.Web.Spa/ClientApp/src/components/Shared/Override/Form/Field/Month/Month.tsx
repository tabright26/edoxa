import React, { FunctionComponent } from "react";
import { Field } from "redux-form";
import Input from "components/Shared/Override/Input";

const months = [];

for (let month = 1; month <= 12; month++) {
  months.push(month);
}

interface MonthSelectFieldProps {
  name?: string;
  className?: string;
  width?: string;
  disabled?: boolean;
}

const MonthSelectField: FunctionComponent<MonthSelectFieldProps> = ({ className, name = "month", width, disabled = false }) => (
  <Field className={className} name={name} type="select" style={width && { width }} component={Input.Select} disabled={disabled}>
    {months.map((month, index) => (
      <option key={index} value={month}>
        {month}
      </option>
    ))}
  </Field>
);

export default MonthSelectField;
