import React from "react";
import Update from "./Update";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";

it("renders correctly", () => {
  const paymentMethod = {
    card: {
      brand: "visa",
      last4: "42w42",
      exp_year: 2030
    }
  };
  const tree = renderer
    .create(
      <Provider store={{ getState: () => {}, dispatch: action => {}, subscribe: () => {} }}>
        <Update initialValues={paymentMethod} />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
