import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Buy from "./Buy";
import { UserAccountDepositBundlesState } from "store/actions/cashier";
import { StripeCustomerState } from "store/root/payment/stripe/customer/types";
import { TOKEN } from "types";

it("renders without crashing", () => {
  //Arrange
  const tokenBundles: UserAccountDepositBundlesState = {
    data: [
      { amount: 10, price: 10 },
      { amount: 30, price: 30 },
      { amount: 50, price: 50 }
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
                bundles: { token: tokenBundles }
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
        <Buy currency={TOKEN} />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
