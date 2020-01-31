import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Panel from ".";
import { UserTransactionState } from "store/root/user/transactionHistory/types";
import {
  TRANSACTION_STATUS_SUCCEEDED,
  TRANSACTION_TYPE_CHARGE,
  TRANSACTION_TYPE_DEPOSIT,
  TRANSACTION_TYPE_WITHDRAWAL,
  CURRENCY_TYPE_MONEY
} from "types";

it("renders without crashing", () => {
  //Arrange
  const transactionHistory: UserTransactionState = {
    data: [
      {
        id: "1",
        timestamp: 111111,
        currency: {
          type: CURRENCY_TYPE_MONEY,
          amount: 100
        },
        description: "test",
        type: TRANSACTION_TYPE_CHARGE,
        status: TRANSACTION_STATUS_SUCCEEDED
      },
      {
        id: "2",
        timestamp: 222222,
        currency: {
          type: CURRENCY_TYPE_MONEY,
          amount: 100
        },
        description: "test",
        type: TRANSACTION_TYPE_DEPOSIT,
        status: TRANSACTION_STATUS_SUCCEEDED
      },
      {
        id: "3",
        timestamp: 333333,
        currency: {
          type: CURRENCY_TYPE_MONEY,
          amount: 100
        },
        description: "test",
        type: TRANSACTION_TYPE_WITHDRAWAL,
        status: TRANSACTION_STATUS_SUCCEEDED
      }
    ],
    loading: false,
    error: null
  };

  const store: any = {
    getState: () => {
      return {
        root: {
          user: {
            transactionHistory
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
        <Panel />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
