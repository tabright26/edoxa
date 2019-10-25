import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import PaymentMethods from "./PaymentMethods";
import { StripeBankAccountState } from "store/root/payment/stripe/bankAccount/types";
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

  const bankAccount: StripeBankAccountState = {
    data: {
      account: null,
      default_for_currency: true,
      metadata: {},
      object: "bank_account",
      account_holder_name: "Test Name",
      account_holder_type: "individual",
      bank_name: "RBC",
      country: "Canada",
      currency: "CAD",
      fingerprint: "Test",
      last4: "4242",
      routing_number: "1234567890",
      status: "verified",
      id: "accountID"
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
              bankAccount,
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
        <PaymentMethods />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
