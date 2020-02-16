import React from "react";
import renderer from "react-test-renderer";
import List from ".";
import { Provider } from "react-redux";

it("renders without crashing", () => {
  // Act
  const store: any = {
    getState: () => {
      return {
        root: {
          challenge: {
            data: [],

            loading: false
          }
        }
      };
    },
    dispatch: action => {},
    subscribe: () => {}
  };

  const tree = renderer
    .create(
      <Provider store={store}>
        <List />
      </Provider>
    )
    .toJSON();

  // Assert
  expect(tree).toMatchSnapshot();
});
