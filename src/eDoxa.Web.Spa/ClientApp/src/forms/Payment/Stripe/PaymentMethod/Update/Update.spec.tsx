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
  const store: any = {
    getState: () => {},
    dispatch: action => {},
    subscribe: () => {}
  };
  const tree = renderer
    .create(
      <Provider store={store}>
        <Update initialValues={paymentMethod} />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
