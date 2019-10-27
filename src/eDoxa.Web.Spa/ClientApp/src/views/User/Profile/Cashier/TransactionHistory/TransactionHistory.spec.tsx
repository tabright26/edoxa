import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import TransactionHistory from "./TransactionHistory";
import { UserAccountTransactionsState } from "store/root/user/account/transactions/types";

it("renders without crashing", () => {
  //Arrange
  const transactions: UserAccountTransactionsState = {
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
              transactions
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