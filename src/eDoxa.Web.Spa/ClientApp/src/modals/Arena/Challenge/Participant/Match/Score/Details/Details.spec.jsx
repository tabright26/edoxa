import React from "react";
import Details from "./Details";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";

it("renders correctly", () => {
  const tree = renderer
    .create(
      <Provider
        store={{
          getState: () => {
            return {
              modal: {
                name: ""
              }
            };
          },
          dispatch: action => {},
          subscribe: () => {}
        }}
      >
        <Details />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
