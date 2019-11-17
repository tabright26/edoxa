import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Overview from "./Overview";
import { UserDoxatagHistoryState } from "store/root/user/doxatagHistory/types";

it("renders without crashing", () => {
  //Arrange
  const doxatagHistory: UserDoxatagHistoryState = {
    data: [{ name: "League of legends", userId: "testId", code: 1234, timestamp: 111111 }],
    loading: false,
    error: null
  };

  const store: any = {
    getState: () => {
      return {
        root: {
          user: {
            doxatagHistory
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
        <Overview />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
