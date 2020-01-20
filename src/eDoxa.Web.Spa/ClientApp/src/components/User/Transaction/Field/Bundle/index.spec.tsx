import React from "react";
import renderer from "react-test-renderer";
import { Form, reduxForm } from "redux-form";
import { Provider } from "react-redux";
import TransactionBundle from ".";
import {
  CURRENCY_TOKEN,
  CURRENCY_MONEY,
  TRANSACTION_TYPE_DEPOSIT
} from "types";

it("renders without crashing", () => {
  //Arrange
  const store: any = {
    getState: () => {
      return {
        static: {
          transactionBundle: {
            data: [
              {
                id: 0,
                type: TRANSACTION_TYPE_DEPOSIT,
                currency: {
                  amount: 10,
                  type: CURRENCY_MONEY
                },
                price: {
                  amount: 10,
                  type: CURRENCY_MONEY
                },
                description: null,
                notes: null,
                disabled: false,
                deprecated: false
              }
            ]
          }
        }
      };
    },
    dispatch: action => {},
    subscribe: () => {}
  };

  const FormWrapper = () => (
    <Form onSubmit={() => {}}>
      <TransactionBundle
        name="transactionBundleId"
        currency={CURRENCY_MONEY}
        transactionType={TRANSACTION_TYPE_DEPOSIT}
      />
    </Form>
  );

  const CustomForm = reduxForm({ form: "TEST_FORM" })(FormWrapper);

  // Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <CustomForm />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});

it("renders without crashing", () => {
  //Arrange
  const store: any = {
    getState: () => {
      return {
        static: {
          transactionBundle: {
            data: [
              {
                id: 0,
                type: TRANSACTION_TYPE_DEPOSIT,
                currency: {
                  amount: 10000,
                  type: CURRENCY_TOKEN
                },
                price: {
                  amount: 10000,
                  type: CURRENCY_TOKEN
                },
                description: null,
                notes: null,
                disabled: false,
                deprecated: false
              }
            ]
          }
        }
      };
    },
    dispatch: action => {},
    subscribe: () => {}
  };

  const FormWrapper = () => (
    <Form onSubmit={() => {}}>
      <TransactionBundle
        name="transactionBundleId"
        currency={CURRENCY_TOKEN}
        transactionType={TRANSACTION_TYPE_DEPOSIT}
      />
    </Form>
  );

  const CustomForm = reduxForm({ form: "TEST_FORM" })(FormWrapper);

  // Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <CustomForm />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
