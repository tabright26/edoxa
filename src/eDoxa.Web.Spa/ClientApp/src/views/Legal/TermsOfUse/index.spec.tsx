import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import TermsOfUse from ".";
import { LocalizeProvider } from "react-localize-redux";

it("renders without crashing", () => {
  // Arrange
  const store: any = {
    getState: () => {},
    dispatch: action => {},
    subscribe: () => {}
  };

  // Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <LocalizeProvider store={store}>
          <TermsOfUse />
        </LocalizeProvider>
      </Provider>
    )
    .toJSON();

  // Assert
  expect(tree).toMatchSnapshot();
});
