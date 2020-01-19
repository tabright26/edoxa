import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Deposit from "./Deposit";
import { StripeCustomerState } from "store/root/payment/stripe/customer/types";
import { CURRENCY_MONEY } from "types";
import { TransactionBundlesState } from "store/static/transactionBundle/types";

it("renders without crashing", () => {
  //Arrange
  const moneyBundles: TransactionBundlesState = {
    data: [],
    loading: false,
    error: null
  };

  const customer: StripeCustomerState = {
    data: {
      defaultPaymentMethodId: "defaultPaymentMethodId"
    },
    loading: false,
    error: null
  };

  const store: any = {
    getState: () => {
      return {
        root: {
          user: {
            account: {
              deposit: {
                bundles: { money: moneyBundles }
              }
            }
          },
          payment: { stripe: { customer } }
        }
      };
    },
    dispatch: action => {},
    subscribe: () => {}
  };

  //Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <Deposit currency={CURRENCY_MONEY} />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
