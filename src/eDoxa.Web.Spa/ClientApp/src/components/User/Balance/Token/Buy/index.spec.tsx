import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Buy from ".";
import { StripeCustomerState } from "store/root/payment/stripe/customer/types";
import { CURRENCY_TYPE_TOKEN } from "types";
import { TransactionBundlesState } from "store/static/transactionBundle/types";

it("renders without crashing", () => {
  //Arrange
  const tokenBundles: TransactionBundlesState = {
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
        <Buy />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
