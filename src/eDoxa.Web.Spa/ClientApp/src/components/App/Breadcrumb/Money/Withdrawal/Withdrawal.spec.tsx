import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Withdrawal from "./Withdrawal";
import { UserAccountWithdrawalBundlesState } from "store/root/user/account/withdrawal/bundles/types";
import { StripeAccountState } from "store/root/payment/stripe/account/types";
import { MONEY } from "types";

it("renders without crashing", () => {
  //Arrange
  const moneyBundles: UserAccountWithdrawalBundlesState = {
    data: [{ amount: 20, price: 20 }, { amount: 50, price: 50 }, { amount: 100, price: 100 }],
    loading: false,
    error: null
  };

  const account: StripeAccountState = {
    data: {
      id: "testID",
      object: "account",
      created: 111111111,
      charges_enabled: false,
      country: "Canada",
      details_submitted: false,
      display_name: "Test Name",
      payouts_enabled: true,
      type: "standard",
      requirements: { currently_due: [], current_deadline: 0 }
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
