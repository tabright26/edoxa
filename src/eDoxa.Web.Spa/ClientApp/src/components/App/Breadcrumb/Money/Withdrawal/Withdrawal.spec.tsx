import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Withdrawal from "./Withdrawal";
import { UserAccountDepositBundlesState } from "store/actions/cashier";
import { StripeAccountState } from "store/root/payment/stripe/account/types";
import { MONEY } from "types";

it("renders without crashing", () => {
  //Arrange
  const moneyBundles: UserAccountDepositBundlesState = {
    data: [
      { amount: 20, price: 20 },
      { amount: 50, price: 50 },
      { amount: 100, price: 100 }
    ],
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
        <Withdrawal currency={MONEY} />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
