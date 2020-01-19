import React, { FunctionComponent } from "react";
import { Field } from "redux-form";
import Input from "components/Shared/Input";

const days = [];

for (let day = 1; day <= 31; day++) {
  days.push(day);
}

interface Props {
  name?: string;
  className?: string;
  width?: string;
  disabled?: boolean;
}

export const Day: FunctionComponent<Props> = ({
  className,
  name = "day",
  width,
  disabled = false
}) => (
  <Field
    className={className}
    name={name}
    type="select"
    style={width && { width }}
    component={Input.Select}
    disabled={disabled}
  >
    {days.map((day, index) => (
      <option key={index} value={day}>
        {day}
      </option>
    ))}
  </Field>
);
