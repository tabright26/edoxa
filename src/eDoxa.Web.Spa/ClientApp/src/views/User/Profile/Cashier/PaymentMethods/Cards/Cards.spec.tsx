import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Cards from "./Cards";
import { StripePaymentMethodsState } from "store/root/payment/stripe/paymentMethods/types";

it("renders without crashing", () => {
  //Arrange
  const paymentMethods: StripePaymentMethodsState = {
    data: {
      object: "list",
      data: [
        {
          type: "card",
          object: "payment_method",
          metadata: {},
          livemode: false,
          id: "testID",
          customer: "testCustomer",
          card: {
            brand: "visa",
            checks: { address_line1_check: "pass", address_postal_code_check: "pass", cvc_check: "pass" },
            country: "CA",
            exp_month: 11,
            exp_year: 22,
            fingerprint: "test",
            funding: "credit",
            generated_from: null,
            last4: "4242",
            three_d_secure_usage: { supported: true },
            wallet: null
          },
          billing_details: {
            address: { line1: "test address" },
            email: "gabriel@edoxa.gg",
            name: "Gabriel Roy",
            phone: "123456789"
          },
          created: 11111111
        }
      ],
      has_more: false,
      url: "testURL"
    },
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
