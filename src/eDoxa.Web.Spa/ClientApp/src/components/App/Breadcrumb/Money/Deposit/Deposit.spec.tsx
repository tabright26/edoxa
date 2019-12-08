import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Deposit from "./Deposit";
import { UserAccountDepositBundlesState } from "store/actions/cashier";
import { StripeCustomerState } from "store/root/payment/stripe/customer/types";
import { MONEY } from "types";

it("renders without crashing", () => {
  //Arrange
  const moneyBundles: UserAccountDepositBundlesState = {
    data: [
      { amount: 100, price: 100 },
      { amount: 200, price: 200 },
      { amount: 300, price: 300 }
    ],
    loading: false,
    error: null
  };

  const customer: StripeCustomerState = {
    data: {
      id: "testID",
      object: "customer",
      address: null,
      created: 111111111,
      currency: "CAD",
      default_source: null,
      delinquent: null,
      livemode: false,
      metadata: null,
      shipping: null,
      subscriptions: null,
      invoice_settings: { default_payment_method: null }
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
        <Deposit currency={MONEY} />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
