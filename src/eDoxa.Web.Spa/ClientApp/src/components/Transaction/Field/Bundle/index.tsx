import React, { FunctionComponent, useState } from "react";
import { Col, Row, Input, Label } from "reactstrap";
import { Field } from "redux-form";
import Format from "components/Shared/Format";
import {
  TransactionBundle,
  CurrencyType,
  TransactionType,
  TransactionBundleId
} from "types";
import { connect, MapStateToProps } from "react-redux";
import { compose } from "recompose";
import { RootState } from "store/types";
import produce, { Draft } from "immer";

interface StateProps {
  bundles: TransactionBundle[];
}

interface OwnProps {
  name: string;
  currencyType: CurrencyType;
  transactionType: TransactionType;
}

type OutterProps = OwnProps;

type InnerProps = StateProps;

type Props = InnerProps & OutterProps;

const Bundle: FunctionComponent<Props> = ({ name, bundles }) => {
  const [bundleId, setBundleId] = useState<TransactionBundleId>(null);
  return (
    <Field
      name={name}
      type="radio"
      value={bundleId}
      parse={Number}
      component={({ input }) => (
        <Row>
          {bundles.map(({ id, currency }: TransactionBundle, index) => {
            const checked = id === input.value;
            return (
              <Col key={index} xs="4">
                <Label
                  className={`btn btn-dark btn-block rounded py-3 px-4 mb-3 ${checked &&
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
                  <Format.Currency currency={currency} alignment="center" />
                </Label>
              </Col>
            );
          })}
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
    bundles: produce(
      state.static.cashier.transaction.bundles,
      (draft: Draft<TransactionBundle[]>) =>
        draft
          .filter(
            bundle =>
              bundle.type.toUpperCase() ===
                ownProps.transactionType.toUpperCase() &&
              bundle.currency.type.toUpperCase() ===
                ownProps.currencyType.toUpperCase() &&
              !bundle.disabled &&
              !bundle.deprecated
          )
          .sort((left, right) =>
            left.currency.amount < right.currency.amount ? -1 : 1
          )
    )
  };
};

const enhance = compose<InnerProps, OutterProps>(connect(mapStateToProps));

export default enhance(Bundle);
