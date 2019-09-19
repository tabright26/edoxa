import React from "react";
import Create from "./Create";
import { Elements } from "react-stripe-elements";
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
        <Elements>
          <Create />
        </Elements>
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
