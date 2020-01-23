import React from "react";
import Create from ".";
import { Elements, StripeProvider } from "react-stripe-elements";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import { CREATE_STRIPE_PAYMENTMETHOD_MODAL } from "utils/modal/constants";

it("renders correctly", () => {
  const store: any = {
    getState: () => {
      return {
        modal: {
          name: CREATE_STRIPE_PAYMENTMETHOD_MODAL
        }
      };
    },
    dispatch: action => {},
    subscribe: () => {}
  };
  const tree = renderer
    .create(
      <Provider store={store}>
        <StripeProvider stripe={null}>
          <Elements>
            <Create />
          </Elements>
        </StripeProvider>
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
