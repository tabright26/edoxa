import React, { FunctionComponent, useState } from "react";
import { FormGroup, Col, Row, Input } from "reactstrap";
import { Field } from "redux-form";
import Format from "components/Shared/Format";

interface AmountsProps {
  amounts: any;
}

// FRANCIS: To refactor as component
const Amounts: FunctionComponent<AmountsProps> = ({ amounts }) => {
  const [selected, setSelected] = useState(0);
  return (
    <Row>
      {amounts.map(({ type, amount }, index) => (
        <Col key={index} xs="4">
          <FormGroup>
            <label className={`btn btn-dark btn-block rounded p-4 ${amount === selected ? "active" : null} `} onClick={() => setSelected(amount)}>
              <Field
                name="amount"
                type="radio"
                value={amount}
                component={({ value, input, ...custom }) => <Input type="radio" className="d-none" checked={value === input.value} {...input} {...custom} />}
              />
              <Format.Currency currency={type} amount={amount} alignment="center" />
            </label>
          </FormGroup>
        </Col>
      ))}
    </Row>
  );
};

export default Amounts;
