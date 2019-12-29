import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Balance from "./Balance";
import { UserAccountBalanceState } from "store/root/user/account/balance/types";
import { CURRENCY_MONEY, CURRENCY_TOKEN } from "types";

it("renders without crashing", () => {
  //Arrange
  const balanceMoney: UserAccountBalanceState = {
    data: { available: 100, pending: 50 },
    loading: false,
    error: null
  };

  const store: any = {
    getState: () => {
      return {
        root: {
          user: {
            account: {
              balance: {
                money: balanceMoney
              }
            }
          }
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
        <Balance currency={CURRENCY_MONEY} attribute={"available"} />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});

it("renders without crashing", () => {
  //Arrange
  const balanceToken: UserAccountBalanceState = {
    data: { available: 10, pending: 25 },
    loading: false,
    error: null
  };

  const store: any = {
    getState: () => {
      return {
        root: {
          user: {
            account: {
              balance: {
                token: balanceToken
              }
            }
          }
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
        <Balance currency={CURRENCY_TOKEN} attribute={"available"} />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
