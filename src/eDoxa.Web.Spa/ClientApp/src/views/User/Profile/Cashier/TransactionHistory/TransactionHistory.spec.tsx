import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import TransactionHistory from "./TransactionHistory";
import { UserTransactionHistoryState } from "store/root/user/transactionHistory/types";

it("renders without crashing", () => {
  //Arrange
  const transactions: UserTransactionHistoryState = {
    data: [
      {
        timestamp: 111111,
        currency: "money",
        amount: 100,
        description: "test",
        type: "charge",
        status: "succeded",
        id: "1"
      },
      {
        timestamp: 222222,
        currency: "money",
        amount: 100,
        description: "test",
        type: "deposit",
        status: "succeded",
        id: "2"
      },
      {
        timestamp: 333333,
        currency: "money",
        amount: 100,
        description: "test",
        type: "withdrawal",
        status: "succeded",
        id: "3"
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
            account: {
              transaction: transactions
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
        <TransactionHistory />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
