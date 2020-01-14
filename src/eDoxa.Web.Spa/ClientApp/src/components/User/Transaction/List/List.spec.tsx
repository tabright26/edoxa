// import React from "react";
// import renderer from "react-test-renderer";
// import { Provider } from "react-redux";
// import List from "./List";
// import { UserAccountTransactionsState } from "store/root/user/account/transaction/types";

it("renders without crashing", () => {
  // //Arrange
  // const transactions: UserAccountTransactionsState = {
  //   data: [
  //     {
  //       timestamp: 111111,
  //       currency: "money",
  //       amount: 100,
  //       description: "test",
  //       type: "charge",
  //       status: "succeeded",
  //       id: "1"
  //     },
  //     {
  //       timestamp: 222222,
  //       currency: "money",
  //       amount: 100,
  //       description: "test",
  //       type: "deposit",
  //       status: "succeeded",
  //       id: "2"
  //     },
  //     {
  //       timestamp: 333333,
  //       currency: "money",
  //       amount: 100,
  //       description: "test",
  //       type: "withdrawal",
  //       status: "succeeded",
  //       id: "3"
  //     }
  //   ],
  //   loading: false,
  //   error: null
  // };

  // const store: any = {
  //   getState: () => {},
  //   dispatch: action => {},
  //   subscribe: () => {}
  // };

  // //Act
  // const tree = renderer
  //   .create(
  //     <Provider store={store}>
  //       <List transactions={transactions.data} />
  //     </Provider>
  //   )
  //   .toJSON();

  // //Assert
  // expect(tree).toMatchSnapshot();
});
