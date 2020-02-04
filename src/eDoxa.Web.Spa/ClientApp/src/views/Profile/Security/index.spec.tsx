import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Security from ".";
import { UserPhoneState } from "store/root/user/phone/types";

it("renders without crashing", () => {
  // Arrange
  const phone: UserPhoneState = {
    data: { number: "123456789", verified: true },
    loading: false,
    error: null
  };

  const store: any = {
    getState: () => {
      return {
        root: {
          user: {
            phone
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
        <Security />
      </Provider>
    )
    .toJSON();

  // Assert
  expect(tree).toMatchSnapshot();
});
