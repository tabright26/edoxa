import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Cards from ".";
import { StripePaymentMethodsState } from "store/root/payment/stripe/paymentMethod/types";

it("renders without crashing", () => {
  //Arrange
  const paymentMethods: StripePaymentMethodsState = {
    data: [
      {
        id: "0",
        card: {
          brand: "visa",
          country: "CA",
          expMonth: 6,
          expYear: 2015,
          last4: "4242"
        }
      }
    ],
    loading: false,
    error: null
  };

  const store: any = {
    getState: () => {
      return {
        root: {
          payment: {
            stripe: {
              paymentMethods
            }
          }
        }
      };
    },
    dispatch: action => {},
    subscribe: () => {}
  };

  //Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <Cards />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
