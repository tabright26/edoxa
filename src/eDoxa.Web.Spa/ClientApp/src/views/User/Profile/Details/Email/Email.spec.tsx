import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Email from "./Email";
import { UserEmailState } from "store/root/user/email/types";

it("renders without crashing", () => {
  //Arrange
  const email: UserEmailState = {
    data: { address: "gabriel@edoxa.gg", verified: true },
    loading: false,
    error: null
  };

  const store: any = {
    getState: () => {
      return {
        root: {
          user: {
            email
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
        <Email />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
