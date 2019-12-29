import React, { FunctionComponent, useState, useEffect } from "react";
import { Col, Row, Input } from "reactstrap";
import { Field } from "redux-form";
import Format from "components/Shared/Format";
import { TransactionBundle, Currency } from "types";

interface Props {
  currency: Currency;
  bundles: TransactionBundle[];
}

const FormFieldBundles: FunctionComponent<Props> = ({ currency, bundles }) => {
  const [value, setValue] = useState(null);
  useEffect(() => {
    setValue(bundles[0].amount);
  }, [bundles]);
  return (
    <Field
      name="bundle"
      type="radio"
      value={value}
      parse={Number}
      component={({ input }) => (
        <Row>
          {bundles.map(({ amount }, index) => (
            <Col key={index} xs="2">
              <label className={`btn btn-dark btn-block rounded py-3 px-4 m-0 ${amount === input.value && "active"} `}>
                <Input type="radio" className="d-none" {...input} value={amount} checked={amount === input.value} onClick={() => setValue(amount)} />
                <Format.Currency currency={currency} amount={amount} alignment="center" />
              </label>
            </Col>
          ))}
        </Row>
      )}
    />
  );
};

export default FormFieldBundles;
