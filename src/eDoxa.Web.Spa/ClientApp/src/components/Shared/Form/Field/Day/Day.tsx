import React, { FunctionComponent } from "react";
import { Field } from "redux-form";
import Input from "components/Shared/Input";

const days = [];

for (let day = 1; day <= 31; day++) {
  days.push(day);
}

interface DayhSelectFieldProps {
  name?: string;
  className?: string;
  width?: string;
  disabled?: boolean;
}

const DaySelectField: FunctionComponent<DayhSelectFieldProps> = ({ className, name = "day", width, disabled = false }) => (
  <Field className={className} name={name} type="select" style={width && { width }} component={Input.Select} disabled={disabled}>
    {days.map((day, index) => (
      <option key={index} value={day}>
        {day}
      </option>
    ))}
  </Field>
);

export default DaySelectField;
