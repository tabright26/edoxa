import React from "react";
import renderer from "react-test-renderer";
import Forgot from "./Forgot";
import { Provider } from "react-redux";

it("renders correctly", () => {
  const tree = renderer
    .create(
      <Provider
        store={{
          getState: () => {
            return {
              oidc: {
                user: null
              }
            };
          },
          dispatch: action => {},
          subscribe: () => {}
        }}
      >
        <Forgot />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
