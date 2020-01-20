import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Withdrawal from ".";
import { StripeAccountState } from "store/root/payment/stripe/account/types";
import { TransactionBundlesState } from "store/static/transactionBundle/types";

it("renders without crashing", () => {
  //Arrange
  const moneyBundles: TransactionBundlesState = {
    data: [],
    loading: false,
    error: null
  };

  const account: StripeAccountState = {
    data: {
      enabled: true
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
              withdrawal: {
                bundles: { money: moneyBundles }
              }
            }
          },
          payment: { stripe: { account } }
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
        <Withdrawal />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
