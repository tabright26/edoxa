import React from "react";
import Create from "./Create";
import { Elements, StripeProvider } from "react-stripe-elements";
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
        <StripeProvider apiKey="" stripe={null}>
          <Elements>
            <Create />
          </Elements>
        </StripeProvider>
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
