import React, { FunctionComponent, useState } from "react";
import { Col, Row, Input, Label } from "reactstrap";
import { Field } from "redux-form";
import Format from "components/Shared/Format";
import {
  TransactionBundle,
  Currency,
  TransactionType,
  TransactionBundleId
} from "types";
import { connect, MapStateToProps } from "react-redux";
import { compose } from "recompose";
import { RootState } from "store/types";

interface StateProps {
  bundles: TransactionBundle[];
}

interface OwnProps {
  name: string;
  currency: Currency;
  transactionType: TransactionType;
}

type OutterProps = OwnProps;

type InnerProps = StateProps;

type Props = InnerProps & OutterProps;

const FormFieldTransactionBundle: FunctionComponent<Props> = ({
  name,
  bundles
}) => {
  const [bundleId, setBundleId] = useState<TransactionBundleId>(null);
  return (
    <Field
      name={name}
      type="radio"
      value={bundleId}
      parse={Number}
      component={({ input }) => (
        <Row>
          {bundles.map(
            ({ id, currency: { amount, type } }: TransactionBundle, index) => {
              const checked = id === input.value;
              return (
                <Col key={index} xs="2">
                  <Label
                    className={`btn btn-dark btn-block rounded py-3 px-4 m-0 ${checked &&
                      "active"}`}
                  >
                    <Input
                      type="radio"
                      className="d-none"
                      {...input}
                      value={id}
                      checked={checked}
                      onClick={() => setBundleId(id)}
                    />
                    <Format.Currency
                      currency={type}
                      amount={amount}
                      alignment="center"
                    />
                  </Label>
                </Col>
              );
            }
          )}
        </Row>
      )}
    />
  );
};

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (
  state,
  ownProps
) => {
  return {
    bundles: state.static.cashier.transaction.bundles.filter(
      bundle =>
        bundle.type.toLowerCase() === ownProps.transactionType.toLowerCase() &&
        bundle.currency.type.toLowerCase() ===
          ownProps.currency.toLowerCase() &&
        !bundle.disabled &&
        !bundle.deprecated
    )
  };
};

const enhance = compose<InnerProps, OutterProps>(connect(mapStateToProps));

export default enhance(FormFieldTransactionBundle);
