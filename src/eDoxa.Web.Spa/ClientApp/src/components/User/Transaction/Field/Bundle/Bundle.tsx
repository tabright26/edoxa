import React, { FunctionComponent, useState, useEffect } from "react";
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
  transactionBundles: TransactionBundle[];
  initialValues: {
    transactionBundleId: number;
  };
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
  transactionBundles,
  initialValues
}) => {
  const [transactionBundleId, setTransactionBundleId] = useState<
    TransactionBundleId
  >(null);
  useEffect(() => {
    if (!transactionBundleId) {
      setTransactionBundleId(initialValues.transactionBundleId);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [initialValues.transactionBundleId]);
  return (
    <Field
      name="transactionBundleId"
      type="radio"
      value={transactionBundleId}
      parse={Number}
      component={({ input }) => (
        <Row>
          {transactionBundles.map(
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
                      onClick={() => setTransactionBundleId(id)}
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
  const bundles = state.static.cashier.transaction.bundles.filter(
    transactionBundle =>
      transactionBundle.type.toLowerCase() ===
        ownProps.transactionType.toLowerCase() &&
      transactionBundle.currency.type.toLowerCase() ===
        ownProps.currency.toLowerCase() &&
      !transactionBundle.disabled &&
      !transactionBundle.deprecated
  );
  return {
    initialValues: {
      transactionBundleId: bundles[0].id
    },
    transactionBundles: bundles
  };
};

const enhance = compose<InnerProps, OutterProps>(connect(mapStateToProps));

export default enhance(FormFieldTransactionBundle);
