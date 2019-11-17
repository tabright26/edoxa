import React from "react";
import renderer from "react-test-renderer";
import List from "./List";
import { Provider } from "react-redux";
import { reducer as rootReducer } from "store/reducer";

it("renders without crashing", () => {
  // Act
  const store: any = {
    getState: () => {
      return {
        root: {
          challenge: {
            data: [],
            error: null,
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
