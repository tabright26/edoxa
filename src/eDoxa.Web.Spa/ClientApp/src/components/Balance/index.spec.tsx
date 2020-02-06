import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Balance from ".";
import { UserBalanceState } from "store/root/user/balance/types";
import { CURRENCY_TYPE_MONEY, CURRENCY_TYPE_TOKEN } from "types";

it("renders without crashing", () => {
  // Arrange
  const balanceMoney: UserBalanceState = {
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

  // Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <Balance type={CURRENCY_TYPE_MONEY} attribute={"available"} />
      </Provider>
    )
    .toJSON();

  // Assert
  expect(tree).toMatchSnapshot();
});

it("renders without crashing", () => {
  // Arrange
  const balanceToken: UserBalanceState = {
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

  // Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <Balance type={CURRENCY_TYPE_TOKEN} attribute={"available"} />
      </Provider>
    )
    .toJSON();

  // Assert
  expect(tree).toMatchSnapshot();
});